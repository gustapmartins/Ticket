using Ticket.Repository.Dao;
using Ticket.Model;
using Ticket.Data;
using Ticket.Enum;

namespace Ticket.Repository.EfCore;

public class CartDaoComEfCore : ICartDao
{
    private readonly TicketContext _ticketContext;
    public CartDaoComEfCore(TicketContext ticketContext)
    {
        _ticketContext = ticketContext;
    }

    public void Add(Carts cart)
    {
        var existingCart = _ticketContext.Carts.FirstOrDefault(c => c.Id == cart.Id);

        if (existingCart == null)
        {
            _ticketContext.Carts.Add(cart);
        }    

       _ticketContext.SaveChanges();
    }

    public Carts FindCartPedding(string Id, StatusPayment statusPayment) 
    {
        return _ticketContext.Carts.FirstOrDefault(cart => cart.Users.Id == Id 
            && cart.CartList.Any(item => item.statusPayment == statusPayment))!;
    }

    public List<Carts> FindAll()
    {
        throw new NotImplementedException();
    }

    public Carts FindId(string id)
    {
        return _ticketContext.Carts.FirstOrDefault(cart => cart.Users.Id == id)!;
    }


    public void Remove(Carts category)
    {
        throw new NotImplementedException();
    }

    public void SaveChanges()
    {
        _ticketContext.SaveChanges();
    }
}
