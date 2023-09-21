using System.ComponentModel.DataAnnotations;

namespace Ticket.DTO.Ticket;

public class TicketCreateDto
{

    [Required(ErrorMessage = "QuantityTickets é obrigatório")]
    public int QuantityTickets { get; set; }

    [Required(ErrorMessage = "ShowId é obrigatório")]
    public int ShowId { get; set; }
}
