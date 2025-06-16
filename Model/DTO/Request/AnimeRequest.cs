using System.ComponentModel.DataAnnotations;

namespace Dotnet_AnimeCRUD.Model.DTO.Request
{
    public class AnimeRequest
    {
        [Required(ErrorMessage = "Title is required")]
        public string Tittle { get; set; } = "";

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; } = "" ;
    }
}
