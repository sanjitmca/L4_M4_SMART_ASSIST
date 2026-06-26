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
        static async Task Main(string[] args)
        {
            System.Console.WriteLine("=== SmartAssist Console Application ===\n");

            // 1. Create repository instances (bottom of the dependency tree)
            IUserRepository   userRepository   = new UserRepository();
            ITicketRepository ticketRepository = new TicketRepository();

            // 2. Inject repositories into services (middle layer)
            IUserService   userService   = new UserService(userRepository);
            ITicketService ticketService = new TicketService(ticketRepository);

            // 3. ConsoleApp consumes services — never touches repositories directly.
            // -----------------------------------------------------------------------

            try
            {
                // =================================================================
                // DEMO 1: User Management
                // =================================================================
                System.Console.WriteLine("--- Users in System ---");
                var users = userService.GetAllUsers();
                foreach (var user in users)
                {
                    System.Console.WriteLine($"  {user.UserId} - {user.Name} ({user.Role}) - {user.Email}");
                }
                System.Console.WriteLine();

                // Nullable handling: GetUserById returns User? — may be null
                var endUser = userService.GetUserById("U001");
                var engineer = userService.GetUserById("U003");

                // Control flow: guard clauses — verify data exists before using
                if (endUser is null || engineer is null)
                {
                    System.Console.WriteLine("ERROR: Required test users not found.");
                    return;
                }

                // =================================================================
                // DEMO 2: Create a Ticket (DTO pattern)
                // =================================================================
                System.Console.WriteLine("--- Creating New Ticket ---");

                // DTO vs Domain Model:
                //   - CreateTicketRequest = input DTO — dumb data bag, no behaviour.
                //   - Ticket (domain)     = private setters, factory method, lifecycle methods.
                var createRequest = new CreateTicketRequest
                {
                    Title           = "Application crashes on startup",
                    Description     = "The app fails to launch after the latest Windows update.",
                    CreatedByUserId = endUser.UserId,
                    Category        = TicketCategory.Technical,
                    SubCategory     = TicketSubCategory.BugDefect,
                    Priority        = TicketPriority.High
                };

                // Async/await: CreateTicketAsync is I/O-bound — await frees the thread while waiting.
                // Task<TicketResponse>: returns a value — the newly created ticket as a DTO.
                var createdTicket = await ticketService.CreateTicketAsync(createRequest);
                System.Console.WriteLine($"  ✓ Ticket #{createdTicket.TicketId} created: {createdTicket.Title}");
                System.Console.WriteLine($"    Status: {createdTicket.Status}, Priority: {createdTicket.Priority}\n");

                // =================================================================
                // DEMO 3: Ticket Lifecycle (Encapsulation via domain methods)
                // =================================================================
                System.Console.WriteLine("--- Ticket Lifecycle ---");

                // Assign ticket to a support engineer
                // Task (no return): async operation — result communicated via exception on failure.
                await ticketService.AssignTicketAsync(createdTicket.TicketId, engineer.UserId);
                System.Console.WriteLine($"  ✓ Ticket #{createdTicket.TicketId} assigned to {engineer.Name}");

                // Resolve ticket
                await ticketService.ResolveTicketAsync(createdTicket.TicketId);
                System.Console.WriteLine($"  ✓ Ticket #{createdTicket.TicketId} resolved");

                // Close ticket with rating and feedback (nullable parameter)
                await ticketService.CloseTicketAsync(createdTicket.TicketId, rating: 5, feedback: "Great support!");
                System.Console.WriteLine($"  ✓ Ticket #{createdTicket.TicketId} closed with rating 5\n");

                // =================================================================
                // DEMO 4: Retrieve all tickets
                // =================================================================
                System.Console.WriteLine("--- All Tickets ---");
                var allTickets = await ticketService.GetTicketsAsync();
                foreach (var ticket in allTickets)
                {
                    System.Console.WriteLine($"  #{ticket.TicketId} - {ticket.Title} [{ticket.Status}]");
                }
                System.Console.WriteLine();

                // =================================================================
                // DEMO 5: Exception Handling - Business Rule Violation
                // =================================================================
                System.Console.WriteLine("--- Testing Business Rule Exception ---");
                try
                {
                    // Try to assign an already-closed ticket (violates business rule)
                    await ticketService.AssignTicketAsync(createdTicket.TicketId, engineer.UserId);
                }
                catch (BusinessRuleException ex)
                {
                    // Exception handling: catch typed domain exceptions for specific errors
                    System.Console.WriteLine($"  ✗ Business Rule Violated: {ex.Message}");
                }
                System.Console.WriteLine();

                // =================================================================
                // DEMO 6: Exception Handling - Validation Failure
                // =================================================================
                System.Console.WriteLine("--- Testing Validation Exception ---");
                try
                {
                    var invalidRequest = new CreateTicketRequest
                    {
                        Title           = "",  // Invalid: empty title
                        CreatedByUserId = endUser.UserId,
                        Category        = TicketCategory.Operational,
                        SubCategory     = TicketSubCategory.ServiceRequest,
                        Priority        = TicketPriority.Low
                    };
                    await ticketService.CreateTicketAsync(invalidRequest);
                }
                catch (ValidationException ex)
                {
                    // ValidationException carries a collection of errors
                    System.Console.WriteLine($"  ✗ Validation Failed: {ex.Message}");
                    foreach (var error in ex.Errors)
                    {
                        System.Console.WriteLine($"    - {error}");
                    }
                }
                System.Console.WriteLine();

                // =================================================================
                // DEMO 7: Exception Handling - Not Found
                // =================================================================
                System.Console.WriteLine("--- Testing NotFoundException ---");
                try
                {
                    // Try to assign a non-existent ticket
                    await ticketService.AssignTicketAsync(99999, engineer.UserId);
                }
                catch (NotFoundException ex)
                {
                    System.Console.WriteLine($"  ✗ Not Found: {ex.Message}");
                }
                System.Console.WriteLine();
            }
            catch (Exception ex)
            {
                // Catch-all: handle any unexpected exceptions gracefully
                System.Console.WriteLine($"FATAL ERROR: {ex.GetType().Name} - {ex.Message}");
                System.Console.WriteLine(ex.StackTrace);
            }

            System.Console.WriteLine("=== End of SmartAssist Demo ===");
            System.Console.WriteLine("Press any key to exit...");
            System.Console.ReadKey();
        }
    }
}
