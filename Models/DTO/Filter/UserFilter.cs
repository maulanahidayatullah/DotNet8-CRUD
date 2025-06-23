using Dotnet_AnimeCRUD.Model.DTO.Filter.BaseFilter;

namespace Dotnet_AnimeCRUD.Model.DTO.Filter
{
    public class UserFilter : PaginatedFilter
    {
        public string? Search { get; set; }
    }
}
