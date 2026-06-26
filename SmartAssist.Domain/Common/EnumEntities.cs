using System;
using System.Collections.Generic;
using System.Text;

namespace SmartAssist.Domain.Common
{
    public class EnumEntities
    {
        public enum TicketCategory
        {
            Technical = 1,
            Operational = 2,
            Quality = 3
        }

        public enum TicketGroup
        {
            All = 0,
            New = 1,
            Open = 2,
            Closed = 3
        }

        public enum TicketPriority
        {
            Low = 1,
            Medium = 2,
            High = 3
        }

        public enum TicketSubCategory
        {
            Incident = 1,
            BugDefect = 2,
            ServiceRequest = 3,
            ChangeRequest = 4,
            AccessPermission = 5,
            DataCorrection = 6,
            BillingIssue = 7,
            EnhancementRequest = 8,
            UsabilityIssue = 9,
            PerformanceIssue = 10
        }
        public enum UserRole
        {
            END_USER = 1,
            SUPPORT_ENGINEER = 2,
            SUPERVISOR = 3
        }

        public enum TicketStatus
        {
            New = 1,
            Assigned = 2,
            Input_Requested = 3,
            Resolved = 4,
            Feedback_Received = 5,
            Closed = 6,
        }
    }
}
