namespace Dotnet_AnimeCRUD.Model.Entity
{
    public class User
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Password{ get; set; }

        // Relasi ke tabel Role
        public required int RoleId { get; set; }
        public Role Role { get; set; } = default!;
    }
}
