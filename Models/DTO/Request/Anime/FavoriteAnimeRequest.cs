using System.ComponentModel.DataAnnotations;

namespace Dotnet_AnimeCRUD.Models.DTO.Request.Anime
{
    public class FavoriteAnimeRequest
    {

        [Required(ErrorMessage = "AnimeId is required")]
        public int AnimeId { get; set; }
    }
}
