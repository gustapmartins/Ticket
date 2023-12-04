using Ticket.Model;
using Ticket.Repository.Utils;

namespace Ticket.Repository.Dao;

public interface ICartDao : ICommand<Cart>
{
    Cart FindCartUser(string Id);
}
