using SmartAssist.Domain.UserManagement;

namespace SmartAssist.Repository.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();

        // Nullable return: the user may not exist; null signals "not found" without an exception
        User? GetById(string userId);
    }
}
