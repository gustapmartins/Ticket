using Ticket.Repository.Dao;
using Ticket.DTO.Ticket;
using Ticket.Data;
using Ticket.Model;

namespace Ticket.Repository.EfCore;

public class TicketDaoComEfCore: ITicketDao
{
    private readonly TicketContext _ticketContext;

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

    public Show FindByShowName(string name)
    {
        return _ticketContext.Shows.FirstOrDefault(show => show.Name == name)!;
    }

    public Users FindByUserEmail(string email)
    {
       return _ticketContext.Users.FirstOrDefault(user => user.Email == email)!;
    }

    public Tickets TicketIdExist(Users findUser, int findTicketId)
    {
        return findUser.Tickets.Find(ticketId => ticketId.Id == findTicketId)!;
    }

    public void Add(Tickets show)
    {
        _ticketContext.Tickets.Add(show);
        _ticketContext.SaveChanges();
    }

    public void Remove(Tickets show)
    {
        _ticketContext.Tickets.Remove(show);
        _ticketContext.SaveChanges();
    }

    public void Update(Tickets tickets, TicketUpdateDto ticketUpdateDto)
    {
        if (ticketUpdateDto.Price > 0)
        {
            tickets.Price = ticketUpdateDto.Price;
        }
        if(ticketUpdateDto.QuantityTickets > 0)
        {
            tickets.QuantityTickets = ticketUpdateDto.QuantityTickets;
        }
        if (ticketUpdateDto.ShowName != null)
        {
            Show show = FindByShowName(ticketUpdateDto.ShowName);
            tickets.Show = show;
        }

        _ticketContext.SaveChanges();
    }

    public void SaveChanges()
    {
        _ticketContext.SaveChanges();
    }

}
