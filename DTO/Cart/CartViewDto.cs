using Ticket.Enum;
using Ticket.Model;

namespace Ticket.DTO.Cart;

public class CartViewDto
{
    public string Id { get; set; }

    public virtual List<CartItem> CartList { get; set; }

    public decimal TotalPrice { get; set; }

    public string UserId { get; set; }

    public StatusPayment statusPayment { get; set; }
}
