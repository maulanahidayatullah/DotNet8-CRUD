namespace Dotnet_AnimeCRUD.Model.Entity
{
    // Tabel yang ada pada DB
    // Yang akan di inisialisasikan pada AnimeDBContext
    public class Role
    {
        public int Id { get; set; }
        public required string Rolename { get; set; }
    }
}
