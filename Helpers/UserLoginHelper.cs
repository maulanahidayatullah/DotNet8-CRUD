using System.Security.Claims;

namespace Dotnet_AnimeCRUD.Helpers
{
    // Helper ini bertujuan untuk mendapatkan UserId yang login
    public class UserLoginHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserLoginHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetUserId()
        {
            var userIdStr = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (int.TryParse(userIdStr, out var userId))
            {
                return userId;
            }

            return 0;
        }
    }
}
