using BCrypt.Net;
namespace Dotnet_AnimeCRUD.Helpers
{
    public class PasswordHelper
    {
        // Hash password
        public static string Generate(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        // Verifikasi password
        public static bool Verify(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
