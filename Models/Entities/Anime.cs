using Dotnet_AnimeCRUD.Models.DTO.Response.Anime;

namespace Dotnet_AnimeCRUD.Model.Entity
{
    // Tabel yang ada pada DB
    // Yang akan di inisialisasikan pada AnimeDBContext
    public class Anime
    {
        public int Id { get; set; }
        public required string Tittle { get; set; }
        public required string Description { get; set; }

        // Relasi one to many ke tabel realasi AnimeCategory
        // untuk relasi many to many tabel Anime ke Category
        // Karena one to many dia di buat ke collection (list/array)
        public ICollection<AnimeCategory> AnimeCategories { get; set; } = new List<AnimeCategory>();

        internal DetailAnimeResponse Select(Func<object, object> value)
        {
            throw new NotImplementedException();
        }
    }
}
