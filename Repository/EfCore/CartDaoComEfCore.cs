using Microsoft.EntityFrameworkCore;
using Ticket.Data;
using Ticket.Model;
using Ticket.Repository.Dao;

namespace Ticket.Repository.EfCore;

public class CartDaoComEfCore : ICartDao
{
    private readonly TicketContext _ticketContext;
    public CartDaoComEfCore(TicketContext ticketContext)
    {
        _ticketContext = ticketContext;
    }

    public void Add(Cart cart)
    {
        var existingCart = _ticketContext.Carts.FirstOrDefault(c => c.Id == cart.Id);

        if (existingCart == null)
        {
            _ticketContext.Carts.Add(cart);
        }    

       _ticketContext.SaveChanges();
    }

    public Cart FindCartUser(string Id) 
    {
        return _ticketContext.Carts.FirstOrDefault(cart => cart.Users.Id == Id)!;
    }

    public List<Cart> FindAll()
    {
        throw new NotImplementedException();
    }

    public Cart FindId(string id)
    {
        return _ticketContext.Carts.FirstOrDefault(cart => cart.Users.Id == id);
    }

    public void Remove(Cart category)
    {
        throw new NotImplementedException();
    }

    public void SaveChanges()
    {
        _ticketContext.SaveChanges();
    }
}
