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
        return _ticketContext.Tickets.OrderByDescending(ticket => ticket.Id).ToList();
    }

    public Tickets FindId(int Id)
    {
        return _ticketContext.Tickets.FirstOrDefault(ticket => ticket.Id == Id)!;
    }

    public List<Show> FindByShowName(string name)
    {
        return _ticketContext.Shows.Where(show => show.Name!.Equals(name)).ToList();
    }

    public Show FindByShowId(int Id)
    {
        return _ticketContext.Shows.FirstOrDefault(show => show.Id == Id)!;
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

}
