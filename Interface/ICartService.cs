using Ticket.DTO.Cart;
using Ticket.Model;

namespace Ticket.Interface;

public interface ICartService
{
    CartViewDto ViewCartUserId(string clientId);

    Cart AddTicketToCart(List<CreateCartDto> ticketQuantityDt, string clientId);

    CartViewDto RemoveTickets(string TicketId, string clientId);

    Cart ClearTicketsCart(string clientId);

    string BuyTicketsAsync(string clientId);
}
