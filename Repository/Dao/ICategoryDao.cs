using Ticket.Model;

namespace Ticket.Repository.Dao;

public interface ICategoryDao: ICommand<Category>, IQuery<Category>
{
}
