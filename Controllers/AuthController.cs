using Dotnet_AnimeCRUD.Model.DTO.Filter;
using Dotnet_AnimeCRUD.Model.DTO.Response.BaseResponse;
using Dotnet_AnimeCRUD.Model.DTO.Response;
using Dotnet_AnimeCRUD.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dotnet_AnimeCRUD.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Dotnet_AnimeCRUD.Models.DTO.Request.Auth;
using Dotnet_AnimeCRUD.Models.DTO.Response.Auth;
using Microsoft.AspNetCore.Authorization;

namespace Dotnet_AnimeCRUD.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService authService;

        public AuthController(AuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("login")]
        public async Task<DetailResponse<LoginResponse>> Login(LoginRequest request)
        {
            return await authService.Login(request);
        }

        [Authorize]
        [HttpPost("me")]
        public async Task<DetailResponse<MeResponse>> Me()
        {
            return await authService.Me();
        }
    }
}
