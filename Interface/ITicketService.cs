using Ticket.Model;

namespace Ticket.Interface;

public interface ITicketService
{
    IEnumerable<Tickets> FindAllTicket();
}
