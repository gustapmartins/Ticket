using System.ComponentModel.DataAnnotations;

namespace Ticket.DTO.User;

public class RegisterDTO
{
    [Required(ErrorMessage = "O Name do filme é obrigatório")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "O Email do filme é obrigatório")]
    public string? Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{4,8}$", ErrorMessage = "Must contain uppercase and lowercase letter and number")]
    public string? Password { get; set; }

    [Required]
    [Compare("Password")]
    public string? ConfirmPassword { get; set; }

    [Required]
    public int? YearsOld { get; set; }

    [Required]
    public string? Cpf { get; set; }

    [Required]
    public string? Role { get; set; }
}
