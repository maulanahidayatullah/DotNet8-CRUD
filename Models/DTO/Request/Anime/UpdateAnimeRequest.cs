using System.ComponentModel.DataAnnotations;

namespace Dotnet_AnimeCRUD.Models.DTO.Request.Anime
{
    public class UpdateAnimeRequest
    {
        [Required(ErrorMessage = "Title is required")]
        public string Tittle { get; set; } = "";

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; } = "";
    }
}
