using static SmartAssist.Domain.Common.EnumEntities;

namespace SmartAssist.Domain.DTO
{
    public class CreateTicketRequest
    {
        public string Title { get; set; } = string.Empty;

        // Nullable: description is optional
        public string? Description { get; set; }

        public string CreatedByUserId { get; set; } = string.Empty;
        public TicketCategory Category { get; set; }
        public TicketSubCategory SubCategory { get; set; }
        public TicketPriority Priority { get; set; }
    }
}
