using Ticket.Model;

namespace Ticket.DTO.Cart;

public class CartViewDto
{
    public string Id { get; set; }

    public virtual List<CartItem> CartList { get; set; }

    public int Quantity { get; set; }

    public decimal TotalPrice { get; set; }

    public string UserId { get; set; }
}
