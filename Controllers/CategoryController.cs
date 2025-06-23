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
using Dotnet_AnimeCRUD.Models.DTO.Response.Category;
using Dotnet_AnimeCRUD.Models.DTO.Request.Category;


namespace Dotnet_AnimeCRUD.Controllers
{
    // Bisa diganti buat nama api nya
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService categoryService;

        public CategoryController(CategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<PaginatedResponse<ListCategoryResponse>> GetListCategory([FromQuery] CategoryFilter filter)
        {
            return await categoryService.GetListCategory(filter);
        }

        [HttpGet("detail/{id}")]
        public async Task<DetailResponse<DetailCategoryResponse>> GetDetailCategory(int id)
        {
            return await categoryService.GetDetailaCategory(id);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost("create")]
        public async Task<BaseResponse> Create([FromBody] CreateCategoryRequest request)
        {
            return await categoryService.CreateCategory(request);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update/{id}")]
        public async Task<BaseResponse> UpdateUser(int id, [FromBody] UpdateCategoryRequest request)
        {
            return await categoryService.UpdateCategory(id, request);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete/{id}")]
        public async Task<BaseResponse> DeleteCategory(int id)
        {
            return await categoryService.DeleteCategory(id);
        }
    }
}
