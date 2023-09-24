using Ticket.Data;
using Ticket.Model;
using Ticket.Repository.Dao;

namespace Ticket.Repository.EfCore;

public class TicketDaoComEfCore: ITicketDao
{
    public readonly TicketContext _ticketContext;

    public TicketDaoComEfCore(TicketContext ticketContext)
    {
        _ticketContext = ticketContext;
    }

    public List<Tickets> FindAll()
    {
        return _ticketContext.Tickets.ToList();
    }

    public Tickets FindId(int Id)
    {
        return _ticketContext.Tickets.FirstOrDefault(ticket => ticket.Id == Id);
    }

    public Show FindByShowId(int Id)
    {
        return _ticketContext.Shows.FirstOrDefault(show => show.Id == Id);
    }

    public void Add(Tickets show)
    {
        _ticketContext.Tickets.Add(show);
        _ticketContext.SaveChanges();
    }

    public void Remove(Tickets show)
    {
        _ticketContext.Remove(show);
        _ticketContext.SaveChanges();
    }

    public void SaveChanges()
    {
        _ticketContext.SaveChanges();
    }

    public virtual Tickets FindByName(string name) 
    {
        return null;
    }
}
