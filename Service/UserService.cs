using Dotnet_AnimeCRUD.Model;
using Dotnet_AnimeCRUD.Model.DTO.Filter;
using Dotnet_AnimeCRUD.Model.DTO.Response.BaseResponse;
using Microsoft.EntityFrameworkCore;
using Dotnet_AnimeCRUD.Models.DTO.Response.User;
using Dotnet_AnimeCRUD.Model.Entity;
using Dotnet_AnimeCRUD.Models.DTO.Request.Anime;
using Dotnet_AnimeCRUD.Models.DTO.Request;
using Dotnet_AnimeCRUD.Models.DTO.Response.Anime;
using Dotnet_AnimeCRUD.Models.DTO.Request.User;
using Dotnet_AnimeCRUD.Model.DTO.User.Request;

namespace Dotnet_AnimeCRUD.Service
{
    public class UserService
    {
        private readonly AnimeDBContext _dbContext; // untuk memanggil db nya
        private readonly ILogger<UserService> _logger; // untuk logging dari .net

        // Diharuskan pakai constructor
        // Agar di inject atau class lain bisa di pakai di class ini
        public UserService(ILogger<UserService> _logger, AnimeDBContext _dbContext)
        {
            this._logger = _logger;
            this._dbContext = _dbContext;
        }

        public async Task<PaginatedResponse<ListUserResponse>> GetListUser(UserFilter filter)
        {
            int page = filter.Page;
            int pagesize = filter.PageSize;
            int offset = (page - 1) * pagesize;
 
            var query = _dbContext.Users.AsQueryable();
            if (!string.IsNullOrEmpty(filter.Search))
            {
                query = query.Where(a => EF.Functions.ILike(a.Username, $"%{filter.Search}%"));
            }

            // Buat Pagination kemudian dimasukan ke ListAsync
            var result = await query
                .Skip(offset)
                .Take(pagesize)
                .ToListAsync();

            var totalData = await query.CountAsync();
            var maxPage = (int)Math.Ceiling((double)totalData / pagesize);

            var dataMapping = result.Select(data => new ListUserResponse
            {
                Id = data.Id,
                Username = data.Username,
            }).ToList();

            PaginatedResponse<ListUserResponse> response = new PaginatedResponse<ListUserResponse>
            {
                Status = true,
                StatusCode = 200,
                Message = "Data Retrieved Successfully!",
                Page = page,
                PageSize = pagesize,
                TotalData = totalData,
                MaxPage = maxPage,
                Data = dataMapping
            };

            return response;
        }
        public async Task<DetailResponse<DetailUserResponse>> GetDetailUser(int id)
        {
            var result = await _dbContext.Users.FindAsync(id);

            if (result is null)
            {
                return new DetailResponse<DetailUserResponse>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "Data Not Found!"
                };
            }

            var dataMapping = new DetailUserResponse
            {
                Id = result.Id,
                Username = result.Username
            };

            var response = new DetailResponse<DetailUserResponse>
            {
                Status = true,
                StatusCode = 200,
                Message = "Data Retrieved Successfully!",
                Data = dataMapping
            };

            return response;
        }
        public async Task<BaseResponse> CreateUser(CreateUserRequest request)
        {
            var dataMapping = new User
            {
                Username = request.Username,
                Password = request.Password,
                RoleId = request.RoleId
            };

            _dbContext.Users.Add(dataMapping);
            await _dbContext.SaveChangesAsync();

            return new BaseResponse()
            {
                Status = true,
                StatusCode = 200,
                Message = "Successfully Created!",
            };
        }
        public async Task<BaseResponse> UpdateUser(int id, UpdateUserRequest request)
        {
            var result = await _dbContext.Users.FindAsync(id);
            if (result is null)
            {
                return new BaseResponse()
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "Data Not Found!",
                };
            }

            result.Username = request.Username;
            result.Password = request.Password;
            result.RoleId = request.RoleId;

            // Mengupdate datanya
            await _dbContext.SaveChangesAsync();

            return new BaseResponse()
            {
                Status = true,
                StatusCode = 200,
                Message = "Successfully Updated!",
            };
        }
        public async Task<BaseResponse> DeleteUser(int id)
        {
            var result = await _dbContext.Users.FindAsync(id);
            if (result is null)
            {
                // Kalau datanya kosong akan return BaseResponse "Not Found"
                return new BaseResponse()
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "Data Not Found!",
                };
            }

            _dbContext.Users.Remove(result);
            await _dbContext.SaveChangesAsync();

            return new BaseResponse()
            {
                Status = true,
                StatusCode = 200,
                Message = "Successfully Deleted!",
            };
        }
    }
}
