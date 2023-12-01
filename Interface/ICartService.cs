using Ticket.DTO.Cart;
using Ticket.Model;

namespace Ticket.Interface;

public interface ICartService
{
    Cart ViewCartUserId(string id);

    Cart AddTicketToCart(CartAddDto CartDto);

    Cart RemoveTickets(CartRemoveDto removeTicket);

    void ClearCart(string userId);
}
