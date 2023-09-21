using Ticket.Model;
using Ticket.Repository.Utils;

namespace Ticket.Repository.Dao;

public interface IShowDao: ICommand<Show>, IQuery<Show> 
{

    Show FindByName(string Name);
    Category FindByCategoryName(string? Name);
}
