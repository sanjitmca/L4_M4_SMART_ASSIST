using System;
using SmartAssist.Services;
using SmartAssist.Repository;

namespace SmartAssist.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("=== SmartAssist Console ===");

            // Use DI later)
            var userRepository = new UserRepository();
            var userService = new UserService(userRepository);

            var users = userService.GetAllUsers();
            foreach (var user in users)
            {
                System.Console.WriteLine($"{user.UserId} - {user.Name} ({user.Email})");
            }

            System.Console.WriteLine("=== End of Demo ===");
        }
    }
}