using System.ComponentModel.DataAnnotations;

namespace Dotnet_AnimeCRUD.Models.DTO.Request.User
{
    public class CreateUserRequest
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; } = "";

        [Required(ErrorMessage = "Password is required")]
        public string Password{ get; set; } = "";

        [Required(ErrorMessage = "RoleId is required")]
        public int RoleId { get; set; } = 0;
    }
}
