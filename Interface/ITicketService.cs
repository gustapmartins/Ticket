using Ticket.DTO.Ticket;
using Ticket.Model;

namespace Ticket.Interface;

public interface ITicketService
{
    List<Tickets> FindAllTicket();
    Tickets FindIdTicket(string id);
    Tickets CreateTicket(TicketCreateDto ticketDto);
    Tickets DeleteTicket(string id);
    Tickets UpdateTicket(string id, TicketUpdateDto ticketDto);
}
