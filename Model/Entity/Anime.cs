namespace Dotnet_AnimeCRUD.Model.Entities
{
    public class Anime
    {
        public int Id { get; set; }
        public required string Tittle { get; set; }
        public required string Description { get; set; }
    }
}
