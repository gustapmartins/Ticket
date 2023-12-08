using Ticket.DTO.Cart;
using Ticket.Model;

namespace Ticket.Interface;

public interface ICartService
{
    CartViewDto ViewCartUserId(string clientId);

    Carts AddTicketToCart(List<CreateCartDto> ticketQuantityDt, string clientId);

    CartViewDto RemoveTickets(string TicketId, string clientId);

    Carts ClearTicketsCart(string clientId);

    void BuyTicketsAsync(string clientId);
}
