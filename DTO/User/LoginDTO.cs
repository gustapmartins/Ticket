using System.ComponentModel.DataAnnotations;

namespace Ticket.DTO.User;

public class LoginDTO
{
    [Required(ErrorMessage = "O Email do filme é obrigatório")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O Password do filme é obrigatório")]
    public string Password { get; set; }

}
