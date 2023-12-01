using Ticket.Model;
using Ticket.Repository.Utils;

namespace Ticket.Repository.Dao;

public interface ICartDao : ICommand<Cart>
{
    Cart FindCartUser(string Id);

    Tickets TicketIdExist(Cart cart, string findTicketId);
}
