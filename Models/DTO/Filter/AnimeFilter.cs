using Dotnet_AnimeCRUD.Model.DTO.Filter.BaseFilter;

namespace Dotnet_AnimeCRUD.Model.DTO.Filter
{
    // Turunan dari PaginatedFilter
    // Jadi apa yang ada di PaginatedFilter nnti ada juga di AnimeFilter
    public class AnimeFilter : PaginatedFilter
    {
        public string? Search { get; set; }
    }
}
