using System.ComponentModel.DataAnnotations;
using Ticket.Enum;

namespace Ticket.Model;

public class CartItem
{
    public CartItem()
    {
        Id = Guid.NewGuid().ToString();
    }

    [Key]
    [Required]
    public string Id { get; set; }

    public int Quantity { get; set; }

    public string TicketId { get; set; }

    public virtual Tickets Ticket { get; set; }
    
    public string CartsId { get; set; }
    
    public virtual Carts Carts { get; set; }

    public StatusPayment statusPayment { get; set; } = StatusPayment.Pedding;
}
