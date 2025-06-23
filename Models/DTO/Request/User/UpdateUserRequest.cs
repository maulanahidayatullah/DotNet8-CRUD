using System.ComponentModel.DataAnnotations;

namespace Dotnet_AnimeCRUD.Model.DTO.User.Request
{
    public class UpdateUserRequest
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; } = "";

        [Required(ErrorMessage = "Password is required")]
        public string Password{ get; set; } = "";

        [Required(ErrorMessage = "RoleId is required")]
        public int RoleId { get; set; } = 0;
    }
}
