using System.ComponentModel.DataAnnotations;

namespace Ticket.DTO.Ticket;

public class TicketCreateDto
{
    [Required(ErrorMessage = "Price é obrigatório")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "QuantityTickets é obrigatório")]
    public int QuantityTickets { get; set; }

    [Required(ErrorMessage = "ShowId é obrigatório")]
    public int ShowId { get; set; }
}
