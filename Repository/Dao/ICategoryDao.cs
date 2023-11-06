using Ticket.DTO.Category;
using Ticket.Model;
using Ticket.Repository.Utils;

namespace Ticket.Repository.Dao;

public interface ICategoryDao : ICommand<Category>, ObjectHandler<Category, CategoryUpdateDto>
{
    Category FindByName(string Name);
}
