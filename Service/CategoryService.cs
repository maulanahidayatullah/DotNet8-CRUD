using Dotnet_AnimeCRUD.Model;
using Dotnet_AnimeCRUD.Model.DTO.Filter;
using Dotnet_AnimeCRUD.Model.DTO.Response.BaseResponse;
using Microsoft.EntityFrameworkCore;
using Dotnet_AnimeCRUD.Model.Entity;
using Dotnet_AnimeCRUD.Models.DTO.Response.Category;
using Dotnet_AnimeCRUD.Models.DTO.Request.Category;

namespace Dotnet_AnimeCRUD.Service
{
    public class CategoryService
    {
        private readonly AnimeDBContext _dbContext; // untuk memanggil db nya
        private readonly ILogger<CategoryService> _logger; // untuk logging dari .net

        // Diharuskan pakai constructor
        // Agar di inject atau class lain bisa di pakai di class ini
        public CategoryService(ILogger<CategoryService> _logger, AnimeDBContext _dbContext)
        {
            this._logger = _logger;
            this._dbContext = _dbContext;
        }

        public async Task<PaginatedResponse<ListCategoryResponse>> GetListCategory(CategoryFilter filter)
        {
            int page = filter.Page;
            int pagesize = filter.PageSize;
            int offset = (page - 1) * pagesize;
 
            var query = _dbContext.Categories.AsQueryable();
            if (!string.IsNullOrEmpty(filter.Search))
            {
                query = query.Where(a => EF.Functions.ILike(a.Categoryname, $"%{filter.Search}%"));
            }

            // Buat Pagination kemudian dimasukan ke ListAsync
            var result = await query
                .Skip(offset)
                .Take(pagesize)
                .ToListAsync();

            var totalData = await query.CountAsync();
            var maxPage = (int)Math.Ceiling((double)totalData / pagesize);

            var dataMapping = result.Select(data => new ListCategoryResponse
            {
                Id = data.Id,
                Categoryname = data.Categoryname,
            }).ToList();

            PaginatedResponse<ListCategoryResponse> response = new PaginatedResponse<ListCategoryResponse>
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
        public async Task<DetailResponse<DetailCategoryResponse>> GetDetailaCategory(int id)
        {
            var result = await _dbContext.Categories.FindAsync(id);

            if (result is null)
            {
                return new DetailResponse<DetailCategoryResponse>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "Data Not Found!"
                };
            }

            var dataMapping = new DetailCategoryResponse
            {
                Id = result.Id,
                Categoryname = result.Categoryname
            };

            var response = new DetailResponse<DetailCategoryResponse>
            {
                Status = true,
                StatusCode = 200,
                Message = "Data Retrieved Successfully!",
                Data = dataMapping
            };

            return response;
        }
        public async Task<BaseResponse> CreateCategory(CreateCategoryRequest request)
        {
            var dataMapping = new Category
            {
                Categoryname = request.Categoryname,
            };

            _dbContext.Categories.Add(dataMapping);
            await _dbContext.SaveChangesAsync();

            return new BaseResponse()
            {
                Status = true,
                StatusCode = 200,
                Message = "Successfully Created!",
            };
        }
        public async Task<BaseResponse> UpdateCategory(int id, UpdateCategoryRequest request)
        {
            var result = await _dbContext.Categories.FindAsync(id);
            if (result is null)
            {
                return new BaseResponse()
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "Data Not Found!",
                };
            }

            result.Categoryname = request.Categoryname;

            // Mengupdate datanya
            await _dbContext.SaveChangesAsync();

            return new BaseResponse()
            {
                Status = true,
                StatusCode = 200,
                Message = "Successfully Updated!",
            };
        }
        public async Task<BaseResponse> DeleteCategory(int id)
        {
            var result = await _dbContext.Categories.FindAsync(id);
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

            _dbContext.Categories.Remove(result);
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
