using Ticket.DTO.Cart;
using Ticket.DTO.Ticket;
using Ticket.Model;

namespace Ticket.Interface;

public interface ICartService
{
    CartAddDto AddTicketToCart(CartAddDto CartDto);

    Tickets RemoveTicketsAsync(RemoveTicketDto removeTicket);

    void ClearCart(string userId);
}
