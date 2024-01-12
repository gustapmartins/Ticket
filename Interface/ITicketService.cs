using Ticket.DTO.Ticket;
using Ticket.Model;

namespace Ticket.Interface;

public interface ITicketService
{
    ResultOperation<List<Tickets>> FindAllTicket();
    ResultOperation<Tickets> FindIdTicket(string id);
    ResultOperation<Tickets> CreateTicket(TicketCreateDto ticketDto);
    Task<List<Show>> SearchShow(string name);
    ResultOperation<Tickets> DeleteTicket(string id);
    ResultOperation<Tickets> UpdateTicket(string id, TicketUpdateDto ticketDto);
}
