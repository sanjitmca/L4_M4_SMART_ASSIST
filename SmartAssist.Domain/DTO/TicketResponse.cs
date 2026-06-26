using SmartAssist.Domain.TicketManagement;

namespace SmartAssist.Domain.DTO
{
    public class TicketResponse
    {
        public int TicketId { get; init; }
        public string Title { get; init; } = string.Empty;
        public string? Description { get; init; }           
        public string Status { get; init; } = string.Empty;
        public string Priority { get; init; } = string.Empty;
        public string Category { get; init; } = string.Empty;
        public string CreatedByUserId { get; init; } = string.Empty;
        public string? AssignedToUserId { get; init; }      
        public DateTime CreatedAt { get; init; }

        public static TicketResponse FromDomain(Ticket ticket) => new()
        {
            TicketId       = ticket.TicketId,
            Title          = ticket.Title,
            Description    = ticket.Description,
            Status         = ticket.Status.ToString(),
            Priority       = ticket.Priority.ToString(),
            Category       = ticket.Category.ToString(),
            CreatedByUserId = ticket.CreatedByUserId,
            AssignedToUserId = ticket.AssignedToUserId,
            CreatedAt      = ticket.CreatedAt
        };
    }
}
