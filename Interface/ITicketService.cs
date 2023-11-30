using Ticket.DTO.Ticket;
using Ticket.Model;

namespace Ticket.Interface;

public interface ITicketService
{
    List<Tickets> FindAllTicket();
    Tickets FindIdTicket(string id);
    TicketCreateDto CreateTicket(TicketCreateDto ticketDto);
    Tickets DeleteTicket(string id);
    BuyTicketDto BuyTicketsAsync(BuyTicketDto buyTicket);
    Tickets RemoveTicketsAsync(RemoveTicketDto removeTicket);
    Tickets UpdateTicket(string id, TicketUpdateDto ticketDto);
}
