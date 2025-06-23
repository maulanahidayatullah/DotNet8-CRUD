namespace Dotnet_AnimeCRUD.Models.DTO.Response.Auth
{
    public class MeResponse
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Rolename { get; set; } = string.Empty;
        public RolesDTO Role { get; set; } = new();
        // Pakai Nested
        public class RolesDTO
        {
            public int Id { get; set; }
            public string Rolename { get; set; } = string.Empty;
        }

    }

    
}
