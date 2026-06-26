using SmartAssist.Domain.UserManagement;

namespace SmartAssist.Services.Interfaces
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();

        // Nullable return type: signals that the user may not exist — no exception needed
        User? GetUserById(string userId);
    }
}
