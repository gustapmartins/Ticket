using Ticket.DTO.Cart;
using Ticket.Model;

namespace Ticket.Interface;

public interface ICartService
{
    CartAddDto AddTicketToCart(CartAddDto CartDto);

    Tickets RemoveTickets(CartRemoveDto removeTicket);

    void ClearCart(string userId);
}
