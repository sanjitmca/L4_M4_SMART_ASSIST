using SmartAssist.Domain.DTO;

namespace SmartAssist.Services.Interfaces
{
    public interface ITicketService
    {
        TicketResponse CreateTicket(CreateTicketRequest request);
        TicketResponse? GetTicketById(int id);
        List<TicketResponse> GetTickets();
        void AssignTicket(int ticketId, string engineerUserId);
        void ResolveTicket(int id);
        void CloseTicket(int id, int rating, string? feedback);
    }
}
