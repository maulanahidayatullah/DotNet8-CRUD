namespace Dotnet_AnimeCRUD.Model.Entity
{
    public class User
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Password{ get; set; }

        // Relasi one to one ke tabel Role
        // Karena one to one dia memanggil entity Role menjadi Object
        public required int RoleId { get; set; }
        public Role Role { get; set; } = default!;
    }
}
