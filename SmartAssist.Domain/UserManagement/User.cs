using static SmartAssist.Domain.Common.EnumEntities;

namespace SmartAssist.Domain.UserManagement
{
    public class User
    {
        public string UserId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public UserRole Role { get; set; }
        public string Email { get; set; } = string.Empty;
        public string? Password { get; set; }
    }
}
