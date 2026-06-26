using SmartAssist.Domain.UserManagement;
using SmartAssist.Repository.Interfaces;
using SmartAssist.Services.Interfaces;

namespace SmartAssist.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<User> GetAllUsers() => _userRepository.GetAllUsers();

        public User? GetUserById(string userId) => _userRepository.GetById(userId);
    }
}
