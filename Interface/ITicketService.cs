using Microsoft.AspNetCore.JsonPatch;
using Ticket.DTO.Ticket;
using Ticket.Model;

namespace Ticket.Interface;

public interface ITicketService
{
    List<Tickets> FindAllTicket();
    Task<Tickets> FindIdTicket(int id);
    Tickets CreateTicket(TicketCreateDto ticketDto);
    Task<Tickets> DeleteTicket(int id);
    Task<BuyTicketDto> BuyTicketsAsync(BuyTicketDto buyTicket);
    Task<Tickets> RemoveTicketsAsync(RemoveTicketDto removeTicket);
    Task<Tickets> UpdateTicket(int id, TicketUpdateDto ticketDto);
}
