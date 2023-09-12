using System.ComponentModel.DataAnnotations;

namespace Ticket.DTO.User;

public class RegisterDTO
{
    [Required(ErrorMessage = "Name is required")]
    public string? Username { get; set; }

    [Required(ErrorMessage = "Email is required")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    [RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{4,8}$", ErrorMessage = "Must contain uppercase and lowercase letter and number")]
    public string? Password { get; set; }

    [Required(ErrorMessage = "Confirm your password")]
    [Compare("Password")]
    public string? ConfirmPassword { get; set; }

    [Required(ErrorMessage = "Confirm is required")]
    public string? Cpf { get; set; }

    [Required(ErrorMessage = "YearsOld is required")]
    public int YearsOld { get; set; }
}
