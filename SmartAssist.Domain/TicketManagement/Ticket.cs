using static SmartAssist.Domain.Common.EnumEntities;

namespace SmartAssist.Domain.TicketManagement
{
    public class Ticket
    {
        public int TicketId { get; private set; }
        public string Title { get; private set; } = string.Empty;
        public string? Description { get; private set; }
        public string CreatedByUserId { get; private set; } = string.Empty;
        public TicketPriority Priority { get; private set; }
        public TicketStatus Status { get; private set; }
        public TicketCategory Category { get; private set; }
        public TicketSubCategory SubCategory { get; private set; }
        public string? AssignedToUserId { get; private set; }
        public int? Rating { get; private set; }
        public string? Feedback { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; private set; }

        // Private constructor: forces use of the factory method for creation
        private Ticket() { }

        // Factory method (Polymorphism / Encapsulation): the only valid way to create a Ticket.
        public static Ticket Create(
            int ticketId,
            string title,
            string? description,
            string createdByUserId,
            TicketCategory category,
            TicketSubCategory subCategory,
            TicketPriority priority)
        {
            return new Ticket
            {
                TicketId = ticketId,
                Title = title,
                Description = description,
                CreatedByUserId = createdByUserId,
                Category = category,
                SubCategory = subCategory,
                Priority = priority,
                CreatedAt = DateTime.UtcNow
            };
        }

        // Behaviour methods: state transitions are the ticket's own responsibility (Encapsulation)

        public void Assign(string engineerUserId)
        {
            AssignedToUserId = engineerUserId;
            Status = TicketStatus.Assigned;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Resolve()
        {
            Status = TicketStatus.Resolved;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Close(int rating, string? feedback)
        {
            Status = TicketStatus.Closed;
            Rating = rating;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
