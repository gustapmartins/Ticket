using Ticket.DTO.Cart;
using Ticket.Model;

namespace Ticket.Interface;

public interface ICartService
{
    Cart ViewCartUserId(string clientId);

    Cart AddTicketToCart(List<CreateCartDto> ticketQuantityDt, string clientId);

    Cart RemoveTickets(string TicketId, string clientId);

    Cart ClearTicketsCart(string clientId);

    string BuyTicketsAsync(string clientId);
}
