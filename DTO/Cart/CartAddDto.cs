namespace Ticket.DTO.Cart;

public class CartAddDto
{
    public string UserId { get; set; }

    public List<string> TicketsId { get; set; }
}
