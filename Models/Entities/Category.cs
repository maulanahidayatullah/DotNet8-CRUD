namespace Dotnet_AnimeCRUD.Model.Entity
{
    public class Category
    {
        public int Id { get; set; }
        public required string Categoryname { get; set; }

        // Relasi one to many ke tabel relasi AnimeCategory
        // untuk relasi many to many tabel Anime ke Category
        // Karena one to many dia di buat ke collection (list/array)
        public ICollection<AnimeCategory> AnimeCategories { get; set; } = [];
    }
}
