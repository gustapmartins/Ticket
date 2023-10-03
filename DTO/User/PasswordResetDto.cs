using System.ComponentModel.DataAnnotations;

namespace Ticket.DTO.Ticket;

public class PasswordResetDto
{
    [Required(ErrorMessage = "O Password é obrigatório")]
    public string? Password { get; set; }

    [Required(ErrorMessage = "Confirm your password")]
    [Compare("Password")]
    public string? ConfirmPassword { get; set; }

    public string? Token { get; set; }
}
