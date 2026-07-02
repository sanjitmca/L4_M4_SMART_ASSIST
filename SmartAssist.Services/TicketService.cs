using SmartAssist.Domain.Common.Exceptions;
using SmartAssist.Domain.DTO;
using SmartAssist.Domain.TicketManagement;
using SmartAssist.Repository.Interfaces;
using SmartAssist.Services.Interfaces;
using static SmartAssist.Domain.Common.EnumEntities;

namespace SmartAssist.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _repository;
        public TicketService(ITicketRepository repository)
        {
            _repository = repository;
        }
        public TicketResponse CreateTicket(CreateTicketRequest request)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(request.Title))
                errors.Add("Title is required.");

            if (request.Title?.Length > 200)
                errors.Add("Title must not exceed 200 characters.");

            if (string.IsNullOrWhiteSpace(request.CreatedByUserId))
                errors.Add("CreatedByUserId is required.");

            if (errors.Count > 0)
                throw new ValidationException(errors);

            var ticket = Ticket.Create(
                ticketId:        Random.Shared.Next(1000, 9999),
                title:           request.Title,
                description:     request.Description,
                createdByUserId: request.CreatedByUserId,
                category:        request.Category,
                subCategory:     request.SubCategory,
                priority:        request.Priority);

            _repository.Add(ticket);

            return TicketResponse.FromDomain(ticket);
        }
        public TicketResponse? GetTicketById(int id)
        {
            var ticket = _repository.Get(id);

            return ticket is null ? null : TicketResponse.FromDomain(ticket);
        }
        public void AssignTicket(int ticketId, string engineerUserId)
        {
            var ticket = _repository.Get(ticketId);

            if (ticket is null)
                throw new NotFoundException(nameof(Ticket), ticketId);

            if (ticket.Status != TicketStatus.New)
                throw new BusinessRuleException(
                    $"Only a NEW ticket can be assigned. Current status: {ticket.Status}.");
            ticket.Assign(engineerUserId);
            _repository.Update(ticket);
        }
        public void ResolveTicket(int id)
        {
            var ticket = _repository.Get(id);

            if (ticket is null)
                throw new NotFoundException(nameof(Ticket), id);

            if (ticket.Status != TicketStatus.Assigned)
                throw new BusinessRuleException(
                    $"Only an ASSIGNED ticket can be resolved. Current status: {ticket.Status}.");

            ticket.Resolve();
            _repository.Update(ticket);
        }

        public void CloseTicket(int id, int rating, string? feedback)
        {
            var ticket = _repository.Get(id);

            if (ticket is null)
                throw new NotFoundException(nameof(Ticket), id);

            if (ticket.Status != TicketStatus.Resolved)
                throw new BusinessRuleException(
                    $"Only a RESOLVED ticket can be closed. Current status: {ticket.Status}.");

            ticket.Close(rating, feedback);
            _repository.Update(ticket);
        }
        public List<TicketResponse> GetTickets()
        {
            var tickets = _repository.GetAll();
            return tickets.Select(TicketResponse.FromDomain).ToList();
        }
    }
}
