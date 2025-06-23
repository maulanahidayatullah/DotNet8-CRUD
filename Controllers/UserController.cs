using Dotnet_AnimeCRUD.Model;
using Dotnet_AnimeCRUD.Model.DTO.Filter;
using Dotnet_AnimeCRUD.Model.DTO.Response.BaseResponse;
using Dotnet_AnimeCRUD.Service;
using Microsoft.AspNetCore.Mvc;
using Dotnet_AnimeCRUD.Models.DTO.Response.User;
using Dotnet_AnimeCRUD.Models.DTO.Request;
using Microsoft.AspNetCore.Authorization;
using Dotnet_AnimeCRUD.Models.DTO.Request.User;
using Dotnet_AnimeCRUD.Model.DTO.User.Request;


namespace Dotnet_AnimeCRUD.Controllers
{
    // Bisa diganti buat nama api nya
    [Authorize(Roles = "Admin")]
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService userService;

        public UserController(UserService userService)
        {
            this.userService = userService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<PaginatedResponse<ListUserResponse>> GetListUser([FromQuery] UserFilter filter)
        {
            return await userService.GetListUser(filter);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("detail/{id}")]
        public async Task<DetailResponse<DetailUserResponse>> GetDetailUser(int id)
        {
            return await userService.GetDetailUser(id);
        }

        [HttpPost("create")]
        public async Task<BaseResponse> CreateUser([FromBody] CreateUserRequest request)
        {
            return await userService.CreateUser(request);
        }

        [HttpPut("update/{id}")]
        public async Task<BaseResponse> UpdateUser(int id, [FromBody] UpdateUserRequest request)
        {
            return await userService.UpdateUser(id, request);
        }

        [HttpDelete("delete/{id}")]
        public async Task<BaseResponse> DeleteUser(int id)
        {
            return await userService.DeleteUser(id);
        }
    }
}
