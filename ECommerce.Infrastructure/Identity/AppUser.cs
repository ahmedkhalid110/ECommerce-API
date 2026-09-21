using Microsoft.AspNetCore.Identity;

namespace ECommerce.Infrastructure.Identity
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; } = string.Empty;
    }
}