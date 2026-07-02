using SmartAssist.Domain.TicketManagement;

namespace SmartAssist.Repository.Interfaces
{
    public interface ITicketRepository
    {
        void Add(Ticket ticket);
        Ticket? Get(int id);
        List<Ticket> GetAll();
        void Update(Ticket ticket);
    }
}
