using Ticket.DTO.Ticket;
using Ticket.Model;
using Ticket.Repository.Utils;

namespace Ticket.Repository.Dao;

public interface ITicketDao : ICommand<Tickets>, ObjectHandler<Tickets, TicketUpdateDto>
{
    Show FindByShowName(string showName);

    Task<List<Show>> FindByShowNameList(string nome);
}
