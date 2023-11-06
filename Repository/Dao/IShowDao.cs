using Ticket.DTO.Show;
using Ticket.Model;
using Ticket.Repository.Utils;

namespace Ticket.Repository.Dao;

public interface IShowDao : ICommand<Show>, ObjectHandler<Show, ShowUpdateDto>
{
    Category FindByCategoryName(string Name);
    Show FindByName(string Name);
    Task<List<Show>> FindByShowNameList(string nome);
}
