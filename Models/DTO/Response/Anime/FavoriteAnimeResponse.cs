using Dotnet_AnimeCRUD.Model.DTO.Response.BaseResponse;
using Dotnet_AnimeCRUD.Models.DTO.Response.Auth;

namespace Dotnet_AnimeCRUD.Models.DTO.Response.Anime
{
    public class FavoriteAnimeResponse : BaseResponse
    {
        public List<AnimesDTO> Animes { get; set; } = [];

        // DTO untuk isi object Array category
        public class AnimesDTO
        {
            public int Id { get; set; }
            public string Tittle { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;

            public List<CategoriesDTO> Categories { get; set; } = new();

        }
        public class CategoriesDTO
        {
            public int Id { get; set; }
            public string Categoryname { get; set; } = string.Empty;
        }
    } 
}
