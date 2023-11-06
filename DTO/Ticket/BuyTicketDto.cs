using System.ComponentModel.DataAnnotations;

namespace Ticket.DTO.Ticket;

public class BuyTicketDto
{
    [Required(ErrorMessage = "O email do usuario é obrigatório")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O ticketId do usuario é obrigatório")]
    public int TicketId { get; set; }

    [Required(ErrorMessage = "O ticketId do usuario é obrigatório")]
    public int QuantityTickets { get; set; }
}
