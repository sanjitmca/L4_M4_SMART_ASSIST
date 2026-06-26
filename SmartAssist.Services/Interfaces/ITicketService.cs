using SmartAssist.Domain.DTO;

namespace SmartAssist.Services.Interfaces
{
    public interface ITicketService
    {
        // Task<TicketResponse>: async, returns a value — ticket was created
        Task<TicketResponse> CreateTicketAsync(CreateTicketRequest request);

        // Task<TicketResponse?>: nullable — ticket with that id may not exist
        Task<TicketResponse?> GetTicketByIdAsync(int id);

        // Task<List<TicketResponse>>: async, returns a collection of DTOs
        Task<List<TicketResponse>> GetTicketsAsync();

        // Task (no return): async write operations — result communicated via exception on failure
        Task AssignTicketAsync(int ticketId, string engineerUserId);
        Task ResolveTicketAsync(int id);
        Task CloseTicketAsync(int id, int rating, string? feedback);
    }
}
