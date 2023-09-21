using Ticket.Data;
using Ticket.Model;
using Ticket.Repository.Dao;

namespace Ticket.Repository.EfCore;

public class ShowDaoComEfCore: IShowDao
{
    public readonly TicketContext _ticketContext;

    public ShowDaoComEfCore(TicketContext ticketContext)
    {
        _ticketContext = ticketContext;
    }

    public List<Show> FindAll()
    {
        return _ticketContext.Shows.ToList();
    }

    public Show FindId(int Id)
    {
        return _ticketContext.Shows.FirstOrDefault(show => show.Id == Id);
    }

    public Category FindByCategoryName(string Name)
    {
        return _ticketContext.Categorys.First(category => category.Name == Name);
    }

    public Show FindByName(string Name)
    {
        return _ticketContext.Shows.FirstOrDefault(show => show.Name == Name);
    }

    public void Add(Show show)
    {
        _ticketContext.Shows.Add(show);
        _ticketContext.SaveChanges();
    }

    public void Remove(Show show)
    {
        _ticketContext.Remove(show);
        _ticketContext.SaveChanges();
    }

    public void SaveChanges()
    {
        _ticketContext.SaveChanges();
    }
}
