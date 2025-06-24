namespace Dotnet_AnimeCRUD.Model.Entity
{
    // Tabel yang ada pada DB
    // Yang akan di inisialisasikan pada AnimeDBContext
    public class UserAnimeFavorite
    {
        public int Id { get; set; }
        public required int UserId { get; set; }
        public required int AnimeId { get; set; }
        public Anime Anime { get; set; } = default!;

        // Relasi one to many ke tabel realasi AnimeCategory
        // untuk relasi many to many tabel Anime ke Category
        // Karena one to many dia di buat ke collection (list/array)
        //public ICollection<AnimeCategory> AnimeCategories { get; set; } = new List<AnimeCategory>();
    }
}
