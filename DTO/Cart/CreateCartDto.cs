using System.ComponentModel.DataAnnotations;

namespace Ticket.DTO.Cart;

public class CreateCartDto
{
    [Required]
    public string TicketId { get; set; }

    [Required]
    public int Quantity { get; set; }
}
