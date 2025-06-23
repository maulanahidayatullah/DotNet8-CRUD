using System.ComponentModel.DataAnnotations;

namespace Dotnet_AnimeCRUD.Models.DTO.Request
{
    public class CreateAnimeRequest
    {
        [Required(ErrorMessage = "Title is required")]
        public string Tittle { get; set; } = "";

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; } = "" ;

        // nnti akan mengirim array atau list CategoryId
        [Required(ErrorMessage = "Category is required")]
        public List<int> CategoryIds { get; set; } = new();
    }
}
