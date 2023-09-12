using System.ComponentModel.DataAnnotations;

namespace Ticket.DTO.Ticket;

public class TicketUpdateDto
{
    public decimal Price { get; set; }

    public int QuantityTickets { get; set; }

    public int ShowId { get; set; }
}
