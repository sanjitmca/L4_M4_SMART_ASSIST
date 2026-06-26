using SmartAssist.Domain.Common;
using static SmartAssist.Domain.Common.EnumEntities;

namespace SmartAssist.Domain.UserManagement
{
    public class User : BaseEntity
    {
        public string UserId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public UserRole Role { get; set; }
        public string Email { get; set; } = string.Empty;

        // Nullable: password may not be stored when using external auth (SSO / OAuth)
        public string? Password { get; set; }
    }
}
