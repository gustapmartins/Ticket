using System.ComponentModel.DataAnnotations;
using Ticket.Enum;

namespace Ticket.Model;

public class Carts
{

    public Carts() { }

    [Key]
    [Required]
    public string Id { get; set; }

    public virtual List<CartItem> CartList { get; set; }

    public virtual Users Users { get; set; }

    public decimal TotalPrice { get; set; }
}
