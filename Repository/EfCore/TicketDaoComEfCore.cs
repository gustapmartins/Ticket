using Microsoft.EntityFrameworkCore;
using Ticket.Data;
using Ticket.DTO.Ticket;
using Ticket.ExceptionFilter;
using Ticket.Model;
using Ticket.Repository.Dao;

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

    public async Task<Tickets> FindId(int Id)
    {
        return await _ticketContext.Tickets.FirstAsync(ticket => ticket.Id == Id);
    }

    public Show FindByShowName(string name)
    {
        var response = _ticketContext.Shows.FirstOrDefault(show => show.Name == name);

        if (response == null) throw new StudentNotFoundException("this value does not exist");

        return response;
    }

    public Users FindByUserEmail(string email)
    {
        var response = _ticketContext.Users.FirstOrDefault(user => user.Email == email);

        if (response == null) throw new StudentNotFoundException("this value does not exist");

        return response;
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
        _ticketContext.Remove(show);
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
