using System;
using System.Collections.Generic;
using System.Text;

namespace SmartAssist.Domain
{
    public class RoleMenuMapping
    {
        public int Role { get; set; }
        public List<int> MenuIds { get; set; }
    }
}
