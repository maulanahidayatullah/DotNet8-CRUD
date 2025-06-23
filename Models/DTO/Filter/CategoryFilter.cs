using Dotnet_AnimeCRUD.Model.DTO.Filter.BaseFilter;

namespace Dotnet_AnimeCRUD.Model.DTO.Filter
{
    public class CategoryFilter : PaginatedFilter
    {
        public string? Search { get; set; }
    }
}
