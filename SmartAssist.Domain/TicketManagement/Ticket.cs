using SmartAssist.Domain.Common;
using static SmartAssist.Domain.Common.EnumEntities;

namespace SmartAssist.Domain.TicketManagement
{
    public class Ticket : BaseEntity
    {
        // Data types: int, string, enums — all non-nullable by default
        public int TicketId { get; private set; }
        public string Title { get; private set; } = string.Empty;

        // Nullable reference type: description is optional
        public string? Description { get; private set; }

        public string CreatedByUserId { get; private set; } = string.Empty;

        // Enum instead of int: self-documenting, type-safe
        public TicketPriority Priority { get; private set; }
        public TicketStatus Status { get; private set; }
        public TicketCategory Category { get; private set; }
        public TicketSubCategory SubCategory { get; private set; }

        // Nullable: these fields are populated only after specific lifecycle events
        public string? AssignedToUserId { get; private set; }
        public int? Rating { get; private set; }
        public string? Feedback { get; private set; }

        // Private constructor: forces use of the factory method for creation
        private Ticket() { }

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
                Status = TicketStatus.New           
            };
        }

        public void Assign(string engineerUserId)
        {
            AssignedToUserId = engineerUserId;
            Status = TicketStatus.Assigned;
            MarkUpdated();                         
        }

        public void Resolve()
        {
            Status = TicketStatus.Resolved;
            MarkUpdated();
        }

        public void Close(int rating, string? feedback)
        {
            Status = TicketStatus.Closed;
            Rating = rating;
            Feedback = feedback;                    
            MarkUpdated();
        }
    }
}
