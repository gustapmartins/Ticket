using System.ComponentModel.DataAnnotations;

namespace Ticket.DTO.Ticket;

public class LoginDTO
{
    [Required(ErrorMessage = "O Email do login é obrigatório")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "O Password do login é obrigatório")]
    public string? Password { get; set; }

}
