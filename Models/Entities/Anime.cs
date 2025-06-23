namespace Dotnet_AnimeCRUD.Model.Entity
{
    // Tabel yang ada pada DB
    // Yang akan di inisialisasikan pada AnimeDBContext
    public class Anime
    {
        public int Id { get; set; }
        public required string Tittle { get; set; }
        public required string Description { get; set; }
    }
}
