using SmartAssist.Domain.UserManagement;
using SmartAssist.Repository.Interfaces;
using static SmartAssist.Domain.Common.EnumEntities;

namespace SmartAssist.Repository
{
    public class UserRepository : IUserRepository
    {
        // In-memory data store — simulates a real database for demonstration
        private readonly List<User> _users =
        [
            new User { UserId = "U001", Name = "Amit Sharma",  Role = UserRole.END_USER,          Email = "amit.sharma@smartassist.com" },
            new User { UserId = "U002", Name = "Priya Verma",  Role = UserRole.END_USER,          Email = "priya.verma@smartassist.com" },
            new User { UserId = "U003", Name = "Ravi Kumar",   Role = UserRole.SUPPORT_ENGINEER,  Email = "ravi.kumar@smartassist.com"  },
            new User { UserId = "U004", Name = "Neha Singh",   Role = UserRole.SUPERVISOR,        Email = "neha.singh@smartassist.com"  }
        ];

        public IEnumerable<User> GetAllUsers() => _users;

        // Nullable return: returns null when no user matches — caller decides how to handle it
        public User? GetById(string userId) =>
            _users.FirstOrDefault(u => u.UserId == userId);
    }
}

