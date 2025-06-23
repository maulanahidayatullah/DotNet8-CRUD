namespace Dotnet_AnimeCRUD.Models.DTO.Response.Auth
{
    public class MeResponse
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Rolename { get; set; } = string.Empty;
        public MeRoleResponse Role { get; set; } = new();

    }

    // Pakai Nested
    public class MeRoleResponse
    {
        public int Id { get; set; }
        public string Rolename { get; set; } = string.Empty;
    }
}
