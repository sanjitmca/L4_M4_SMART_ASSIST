using SmartAssist.Domain.TicketManagement;

namespace SmartAssist.Repository.Interfaces
{
    public interface ITicketRepository
    {
        Task AddAsync(Ticket ticket);

        // Nullable return: Task<Ticket?> — caller must handle the case where ticket is not found
        Task<Ticket?> GetAsync(int id);

        Task<List<Ticket>> GetAllAsync();

        Task UpdateAsync(Ticket ticket);
    }
}
