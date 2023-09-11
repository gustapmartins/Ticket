using System.ComponentModel.DataAnnotations;

namespace Ticket.DTO.Ticket;

public class TicketViewDto
{
    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public int ShowId { get; set; }

    public string Show { get; set; }
}
