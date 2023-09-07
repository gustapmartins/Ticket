using System.ComponentModel.DataAnnotations;

namespace Ticket.DTO.Ticket;

public class TicketCreateDto
{
    public int QuantityTickets { get; set; }

    public decimal Price { get; set; }

    public int ShowId { get; set; }
}
