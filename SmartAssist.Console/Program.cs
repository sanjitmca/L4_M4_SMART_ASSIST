using SmartAssist.Domain.Common.Exceptions;
using SmartAssist.Domain.DTO;
using SmartAssist.Repository.Interfaces;
using SmartAssist.Repository.Repositories;
using SmartAssist.Repository;
using SmartAssist.Services;
using SmartAssist.Services.Interfaces;
using static SmartAssist.Domain.Common.EnumEntities;

namespace SmartAssist.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("=== SmartAssist Console Application ===\n");

            IUserRepository   userRepository   = new UserRepository();
            ITicketRepository ticketRepository = new TicketRepository();

            IUserService   userService   = new UserService(userRepository);
            ITicketService ticketService = new TicketService(ticketRepository);

            try
            {
                System.Console.WriteLine("--- Users in System ---");
                var users = userService.GetAllUsers();
                foreach (var user in users)
                {
                    System.Console.WriteLine($"  {user.UserId} - {user.Name} ({user.Role}) - {user.Email}");
                }
                System.Console.WriteLine();

                var endUser = userService.GetUserById("U001");
                var engineer = userService.GetUserById("U003");

                if (endUser is null || engineer is null)
                {
                    System.Console.WriteLine("ERROR: Required test users not found.");
                    return;
                }

                System.Console.WriteLine("--- Creating New Ticket ---");

                var createRequest = new CreateTicketRequest
                {
                    Title           = "Application crashes on startup",
                    Description     = "The app fails to launch after the latest Windows update.",
                    CreatedByUserId = endUser.UserId,
                    Category        = TicketCategory.Technical,
                    SubCategory     = TicketSubCategory.BugDefect,
                    Priority        = TicketPriority.High
                };

                var createdTicket = ticketService.CreateTicket(createRequest);
                System.Console.WriteLine($"Ticket #{createdTicket.TicketId} created: {createdTicket.Title}");
                System.Console.WriteLine($"Status: {createdTicket.Status}, Priority: {createdTicket.Priority}\n");

                System.Console.WriteLine("--- Ticket Lifecycle ---");

                ticketService.AssignTicket(createdTicket.TicketId, engineer.UserId);
                System.Console.WriteLine($"icket #{createdTicket.TicketId} assigned to {engineer.Name}");

                ticketService.ResolveTicket(createdTicket.TicketId);
                System.Console.WriteLine($"Ticket #{createdTicket.TicketId} resolved");

                ticketService.CloseTicket(createdTicket.TicketId, rating: 5, feedback: "Great support!");
                System.Console.WriteLine($"Ticket #{createdTicket.TicketId} closed with rating 5\n");

                System.Console.WriteLine("--- All Tickets ---");
                var allTickets = ticketService.GetTickets();
                foreach (var ticket in allTickets)
                {
                    System.Console.WriteLine($"  #{ticket.TicketId} - {ticket.Title} [{ticket.Status}]");
                }
                System.Console.WriteLine();

                System.Console.WriteLine("--- Testing Business Rule Exception ---");
                try
                {
                    ticketService.AssignTicket(createdTicket.TicketId, engineer.UserId);
                }
                catch (BusinessRuleException ex)
                {
                    System.Console.WriteLine($"Business Rule Violated: {ex.Message}");
                }
                System.Console.WriteLine();

                System.Console.WriteLine("--- Testing Validation Exception ---");
                try
                {
                    var invalidRequest = new CreateTicketRequest
                    {
                        Title           = "",
                        CreatedByUserId = endUser.UserId,
                        Category        = TicketCategory.Operational,
                        SubCategory     = TicketSubCategory.ServiceRequest,
                        Priority        = TicketPriority.Low
                    };
                    ticketService.CreateTicket(invalidRequest);
                }
                catch (ValidationException ex)
                {
                    System.Console.WriteLine($"Validation Failed: {ex.Message}");
                    foreach (var error in ex.Errors)
                    {
                        System.Console.WriteLine($"    - {error}");
                    }
                }
                System.Console.WriteLine();

                System.Console.WriteLine("--- Testing NotFoundException ---");
                try
                {
                    ticketService.AssignTicket(99999, engineer.UserId);
                }
                catch (NotFoundException ex)
                {
                    System.Console.WriteLine($"Not Found: {ex.Message}");
                }
                System.Console.WriteLine();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"FATAL ERROR: {ex.GetType().Name} - {ex.Message}");
                System.Console.WriteLine(ex.StackTrace);
            }

            System.Console.WriteLine("=== End of SmartAssist Demo ===");
            System.Console.WriteLine("Press any key to exit...");
            System.Console.ReadKey();
        }
    }
}
