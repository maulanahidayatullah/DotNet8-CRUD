using System.ComponentModel.DataAnnotations;

namespace Dotnet_AnimeCRUD.Model.DTO.Filter.BaseFilter
{
    public class PaginatedFilter
    {
        [Required(ErrorMessage = "Page is required")]
        public int Page { get; set; }

        [Required(ErrorMessage = "PageSize is required")]
        public int PageSize { get; set; }
    }
}