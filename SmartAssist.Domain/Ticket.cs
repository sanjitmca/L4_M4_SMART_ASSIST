using System;
using System.Collections.Generic;
using System.Text;

namespace SmartAssist.Domain
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CreatedByUserId { get; set; }
        public int Priority { get; set; }
        public TicketStatus Status { get; set; }
        public TicketCategory Category { get; set; }
        public TicketSubCategory SubCategory { get; set; }
        public DateTime CreatedAt { get; set; }
        public string AssignedToUserId { get; set; }
        public int Rating { get; set; }
        public string Feedback { get; set; }

    }
    public enum TicketStatus
    {
        New,
        Assigned,
        Resolved,
        Closed
    }
    public enum TicketCategory
    {
        Technical,
        Functional,
        Other
    }
    public enum TicketSubCategory
    {
        Incident,
        BugDefect,
        ServiceRequest
    }
}
