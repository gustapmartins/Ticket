using Ticket.Model;
using Ticket.Repository.Utils;

namespace Ticket.Repository.Dao;

public interface IShowDao: ICommand<Show>, IQuery<Show> 
{
    Category FindByCategoryName(string? Name);

    Show FindByName(string Name);
}
