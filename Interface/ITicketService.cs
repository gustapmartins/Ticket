using Ticket.DTO.Ticket;
using Ticket.Model;

namespace Ticket.Interface;

public interface ITicketService
{
    List<Tickets> FindAllTicket();
    Tickets FindIdTicket(string id);
    Tickets CreateTicket(TicketCreateDto ticketDto);
    Task<List<Show>> SearchShow(string name);
    Tickets DeleteTicket(string id);
    Tickets UpdateTicket(string id, TicketUpdateDto ticketDto);
}
