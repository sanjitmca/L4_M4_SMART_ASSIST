using SmartAssist.Domain.TicketManagement;
using SmartAssist.Repository.Interfaces;

namespace SmartAssist.Repository.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly List<Ticket> _tickets = [];

        // Task (not Task<T>): AddAsync performs a write and returns nothing
        public async Task AddAsync(Ticket ticket)
        {
            _tickets.Add(ticket);
            await Task.CompletedTask;   // Placeholder for real async I/O
        }

        // Task<List<Ticket>>: returns a value — the full list of tickets
        public async Task<List<Ticket>> GetAllAsync()
        {
            return await Task.FromResult(_tickets);
        }

        // Task<Ticket?>: nullable — returns null when no ticket with that id exists
        public async Task<Ticket?> GetAsync(int id)
        {
            return await Task.FromResult(
                _tickets.FirstOrDefault(x => x.TicketId == id));
        }

        public async Task UpdateAsync(Ticket ticket)
        {
            await Task.CompletedTask;   // In-memory list is already mutated via domain methods
        }
    }
}
