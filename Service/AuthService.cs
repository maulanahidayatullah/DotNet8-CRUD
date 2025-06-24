using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Dotnet_AnimeCRUD.Config;
using Dotnet_AnimeCRUD.Helpers;
using Dotnet_AnimeCRUD.Model;
using Dotnet_AnimeCRUD.Model.DTO.Response.BaseResponse;
using Dotnet_AnimeCRUD.Model.Entity;
using Dotnet_AnimeCRUD.Models.DTO.Request.Auth;
using Dotnet_AnimeCRUD.Models.DTO.Response.Anime;
using Dotnet_AnimeCRUD.Models.DTO.Response.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dotnet_AnimeCRUD.Service
{
    public class AuthService
    {
        private readonly AnimeDBContext _dbContext; // untuk memanggil db nya
        private readonly ILogger<AuthService> _logger; // untuk logging dari .net
        private readonly JwtConfig _jwtGen; // untuk generate jwt yang ada di helper
        private readonly IHttpContextAccessor _httpContextAccessor;

        // Diharuskan pakai constructor
        // Agar di inject atau class lain bisa di pakai di class ini
        public AuthService(ILogger<AuthService> _logger, AnimeDBContext _dbContext, JwtConfig _jwtGen, IHttpContextAccessor _httpContextAccessor)
        {
            this._jwtGen = _jwtGen;
            this._logger = _logger;
            this._dbContext = _dbContext;
            this._httpContextAccessor = _httpContextAccessor;
        }

        public async Task<DetailResponse<LoginResponse>> Login(LoginRequest request)
        {
            // Ambil data user dengan relasi (include) ketabel Role
            var result = await _dbContext.Users
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.Username == request.Username);

            if (result is null || !PasswordHelper.Verify(request.Password, result.Password))
            {
                return new DetailResponse<LoginResponse>
                {
                    Status = false,
                    StatusCode = 401,
                    Message = "Invalid username or password"
                    //Data = new LoginResponse()
                };
            }

            //_logger.LogInformation("Login Result: {@result}", result);
            //_logger.LogInformation("Role: {RoleName}", result?.Role?.Name);

            // Akan memanggil generate Token yang ada di Helpers -> JwtTokenGenerator
            // dan mengirim data user yang login dengan relasi ke Role
            string token = _jwtGen.Generate(result);

            var dataMapping = new LoginResponse
            {
                Token = token,
            };

            return new DetailResponse<LoginResponse>
            {
                Status = true,
                StatusCode = 200,
                Message = "Success Login!",
                Data = dataMapping
            };
        }
        public async Task<DetailResponse<MeResponse>> Me()
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";

            if (string.IsNullOrEmpty(userId))
            {
                return new DetailResponse<MeResponse>
                {
                    Status = false,
                    StatusCode = 403,
                    Message = "Unauthorized!"
                };
            }

            var result = await _dbContext.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == int.Parse(userId));

            if (result is null)
            {
                return new DetailResponse<MeResponse>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "Data Not Found!"
                };
            }

            var dataMapping = new MeResponse
            {
                Id = result.Id,
                Username = result.Username,
                Rolename = result.Role.Rolename,
                // Buat percobaan kalau nnti ada response object
                // MeRoleResponse ada di dalam file MeResponse dengan pakai Nested
                Role = new MeResponse.RolesDTO
                {
                    Id = result.Role.Id,
                    Rolename = result.Role.Rolename
                }
            };

            return new DetailResponse<MeResponse>
            {
                Status = true,
                StatusCode = 200,
                Message = "Data User!",
                Data = dataMapping
            };

            //if (result is null)
            //{
            //    return new DetailResponse<LoginResponse>
            //    {
            //        Status = false,
            //        StatusCode = 401,
            //        Message = "Invalid username or password"
            //        //Data = new LoginResponse()
            //    };
            //}

            //_logger.LogInformation("Login Result: {@result}", result);
            //_logger.LogInformation("Role: {RoleName}", result?.Role?.Name);

            // Akan memanggil generate Token yang ada di Helpers -> JwtTokenGenerator
            // dan mengirim data user yang login dengan relasi ke Role
            //string token = _jwtGen.Generate(result);

            //var dataMapping = new LoginResponse
            //{
            //    Token = token,
            //};

            //return new DetailResponse<LoginResponse>
            //{
            //    Status = true,
            //    StatusCode = 200,
            //    Message = "Success Login!",
            //    Data = dataMapping
            //};
        }
    }
}
