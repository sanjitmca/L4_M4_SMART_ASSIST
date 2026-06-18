using System;
using System.Collections.Generic;
using System.Text;

namespace SmartAssist.Domain
{
    public class User
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public int Role { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
