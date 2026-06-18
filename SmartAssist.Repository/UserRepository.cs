using SmartAssist.Domain;

namespace SmartAssist.Repository
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();
    }
    public class UserRepository : IUserRepository
    {
        public IEnumerable<User> GetAllUsers()
        {
            // Implementation to retrieve all users from the data source
            // Mock data or DB call
            return new List<User>
            {
                new User { UserId = "U001", Name = "Amit Sharma", Role = 1, Email = "amit.sharma@smartassist.com" },
                new User { UserId = "U002", Name = "Priya Verma", Role = 1, Email = "priya.verma@smartassist.com" }
            };
        }
    }
}
