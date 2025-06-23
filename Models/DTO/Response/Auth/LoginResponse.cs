using System.ComponentModel.DataAnnotations;
using Dotnet_AnimeCRUD.Model.DTO.Response.BaseResponse;

namespace Dotnet_AnimeCRUD.Models.DTO.Response.Auth
{
    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
    }
    
}
