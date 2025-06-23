using System.ComponentModel.DataAnnotations;

namespace Dotnet_AnimeCRUD.Models.DTO.Request.Category
{
    public class UpdateCategoryRequest
    {
        [Required(ErrorMessage = "Categoryname is required")]
        public string Categoryname{ get; set; } = "";
    }
}
