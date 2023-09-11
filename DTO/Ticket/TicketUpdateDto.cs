using System.ComponentModel.DataAnnotations;

namespace Ticket.DTO.Ticket;

public class TicketUpdateDto
{
    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public int ShowId { get; set; }
}
