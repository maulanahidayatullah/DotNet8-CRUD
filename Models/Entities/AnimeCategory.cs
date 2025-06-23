namespace Dotnet_AnimeCRUD.Model.Entity
{
    // Tabel yang ada pada DB
    // Yang akan di inisialisasikan pada AnimeDBContext
    public class AnimeCategory
    {
        public int AnimeId { get; set; }
        public Anime Anime { get; set; } = default!;

        // Relasi ke tabel category
        // Jadi dia akan mengambil data object di category
        public int CategoryId { get; set; }
        public Category Category { get; set; } = default!;
    }
}
