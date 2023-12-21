using Ticket.DTO.Cart;
using Ticket.Enum;
using Ticket.Model;

namespace Ticket.Interface;

public interface ICartService
{
    CartViewDto ViewCartPedding(string clientId, StatusPayment statusPayment);

    Carts AddTicketToCart(List<CreateCartDto> ticketQuantityDt, string clientId);

    CartViewDto RemoveTickets(string TicketId, string clientId);

    ResultOperation<Carts> ClearTicketsCart(string clientId);

    void BuyTicketsAsync(string clientId);
}
