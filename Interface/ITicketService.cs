using Microsoft.AspNetCore.JsonPatch;
using Ticket.DTO.Ticket;
using Ticket.DTO.User;
using Ticket.Model;

namespace Ticket.Interface;

public interface ITicketService
{
    List<Tickets> FindAll();
    Tickets FindId(int id);
    TicketCreateDto CreateTicket(TicketCreateDto ticketDto);
    Tickets DeleteTicket(int id);
    Task<BuyTicketDto> BuyTicketsAsync(BuyTicketDto buyTicket);
    TicketUpdateDto UpdateTicket(int id, JsonPatchDocument<TicketUpdateDto> ticketDto);
}
