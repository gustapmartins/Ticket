using System.ComponentModel.DataAnnotations;

namespace Ticket.DTO.Ticket;

public class RemoveTicketDto
{
    [Required(ErrorMessage = "O email do usuario é obrigatório")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O ticketId do usuario é obrigatório")]
    public string TicketId { get; set; }
}
