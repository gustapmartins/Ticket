using Ticket.DTO.Ticket;
using Ticket.Model;

namespace Ticket.Interface;

public interface ITicketService
{
    List<Tickets> FindAllTicket();
    Tickets FindIdTicket(int id);
    Tickets CreateTicket(TicketCreateDto ticketDto);
    Tickets DeleteTicket(int id);
    BuyTicketDto BuyTicketsAsync(BuyTicketDto buyTicket);
    Tickets RemoveTicketsAsync(RemoveTicketDto removeTicket);
    Tickets UpdateTicket(int id, TicketUpdateDto ticketDto);
}
