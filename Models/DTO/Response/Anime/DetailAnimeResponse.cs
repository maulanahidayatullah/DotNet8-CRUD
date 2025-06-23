using Dotnet_AnimeCRUD.Model.DTO.Response.BaseResponse;

namespace Dotnet_AnimeCRUD.Models.DTO.Response.Anime
{
    public class DetailAnimeResponse
    {
        public int Id { get; set; }
        public string Tittle { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
