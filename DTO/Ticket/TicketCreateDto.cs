using System.ComponentModel.DataAnnotations;

namespace Ticket.DTO.Ticket;

public class TicketCreateDto
{

    [Required(ErrorMessage = "QuantityTickets é obrigatório")]
    public int QuantityTickets { get; set; }

    [Required(ErrorMessage = "ShowName é obrigatório")]
    public string ShowName { get; set; }

    [Required(ErrorMessage = "Price é obrigatório")]
    public decimal Price { get; set; }
}
