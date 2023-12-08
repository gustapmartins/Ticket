using Ticket.Model;
using Ticket.Repository.Utils;

namespace Ticket.Repository.Dao;

public interface ICartDao : ICommand<Carts>
{
    Carts FindCartUser(string Id);
}
