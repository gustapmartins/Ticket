using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Ticket.DTO.Ticket;

public class RemoveTicketDto
{
    [Required(ErrorMessage = "O email do usuario é obrigatório")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "O ticketId do usuario é obrigatório")]
    public int TicketId { get; set; }
}
