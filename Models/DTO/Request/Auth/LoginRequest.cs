using System.ComponentModel.DataAnnotations;

namespace Dotnet_AnimeCRUD.Models.DTO.Request.Auth
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; } = "";

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = "";
    }
}
