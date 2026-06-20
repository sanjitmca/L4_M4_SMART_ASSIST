using System;
using System.Collections.Generic;
using System.Text;

namespace SmartAssist.Domain
{
    public class User
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public UserRole Role { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }

    public enum UserRole
    {
        END_USER = 1,
        SUPPORT_ENGINEER = 2,
        SUPERVISOR = 3
    }
}
