using Ticket.Repository.Utils;
using Ticket.Model;
using Ticket.Enum;

namespace Ticket.Repository.Dao;

public interface ICartDao : ICommand<Carts>
{
    Carts FindCartPedding(string Id, StatusPayment statusPayment);
}
