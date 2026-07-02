using SmartAssist.Domain.TicketManagement;
using SmartAssist.Repository.Interfaces;

namespace SmartAssist.Repository.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly List<Ticket> _tickets = [];

        public void Add(Ticket ticket)
        {
            _tickets.Add(ticket);
        }
        public List<Ticket> GetAll() => _tickets;
        public Ticket? Get(int id) => _tickets.FirstOrDefault(x => x.TicketId == id);
        public void Update(Ticket ticket)
        {
        }
    }
}
