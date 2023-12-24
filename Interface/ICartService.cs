using Ticket.DTO.Cart;
using Ticket.Enum;
using Ticket.Model;
using Ticket.Service;

namespace Ticket.Interface;

public interface ICartService
{
    ResultOperation<CartViewDto> ViewCartPedding(string clientId, StatusPayment statusPayment);

    ResultOperation<Carts> AddTicketToCart(List<CreateCartDto> ticketQuantityDt, string clientId);

    ResultOperation<CartViewDto> RemoveTickets(string TicketId, string clientId);

    ResultOperation<CartViewDto> ClearTicketsCart(string clientId);

    void BuyTicketsAsync(string clientId);
}
