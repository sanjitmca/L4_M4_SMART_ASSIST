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

        public async Task<TicketResponse> CreateTicketAsync(CreateTicketRequest request)
        {
            // Validation: inline business rules — check input before creating domain entity
            var errors = new List<string>();

            // Control flow: if/else guards — check each rule independently
            if (string.IsNullOrWhiteSpace(request.Title))
                errors.Add("Title is required.");

            // Nullable-aware: Title?.Length uses null-conditional operator
            if (request.Title?.Length > 200)
                errors.Add("Title must not exceed 200 characters.");

            if (string.IsNullOrWhiteSpace(request.CreatedByUserId))
                errors.Add("CreatedByUserId is required.");

            // Exception handling: throw a typed domain exception, not a generic Exception.
            // ValidationException carries the full error list so the caller can display them.
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

            await _repository.AddAsync(ticket);

            return TicketResponse.FromDomain(ticket);
        }

        public async Task<TicketResponse?> GetTicketByIdAsync(int id)
        {
            var ticket = await _repository.GetAsync(id);

            // Nullable handling: null-conditional returns null instead of throwing
            return ticket is null ? null : TicketResponse.FromDomain(ticket);
        }

        public async Task AssignTicketAsync(int ticketId, string engineerUserId)
        {
            var ticket = await _repository.GetAsync(ticketId);

            if (ticket is null)
                throw new NotFoundException(nameof(Ticket), ticketId);

            // Control flow: guard clause — verify the business rule before proceeding
            if (ticket.Status != TicketStatus.New)
                throw new BusinessRuleException(
                    $"Only a NEW ticket can be assigned. Current status: {ticket.Status}.");

            ticket.Assign(engineerUserId);

            await _repository.UpdateAsync(ticket);
        }

        public async Task ResolveTicketAsync(int id)
        {
            var ticket = await _repository.GetAsync(id);

            if (ticket is null)
                throw new NotFoundException(nameof(Ticket), id);

            if (ticket.Status != TicketStatus.Assigned)
                throw new BusinessRuleException(
                    $"Only an ASSIGNED ticket can be resolved. Current status: {ticket.Status}.");

            ticket.Resolve();
            await _repository.UpdateAsync(ticket);
        }

        public async Task CloseTicketAsync(int id, int rating, string? feedback)
        {
            var ticket = await _repository.GetAsync(id);

            if (ticket is null)
                throw new NotFoundException(nameof(Ticket), id);

            if (ticket.Status != TicketStatus.Resolved)
                throw new BusinessRuleException(
                    $"Only a RESOLVED ticket can be closed. Current status: {ticket.Status}.");

            ticket.Close(rating, feedback);
            await _repository.UpdateAsync(ticket);
        }

        public async Task<List<TicketResponse>> GetTicketsAsync()
        {
            var tickets = await _repository.GetAllAsync();
            return tickets.Select(TicketResponse.FromDomain).ToList();
        }
    }
}

