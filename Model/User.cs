using System.ComponentModel.DataAnnotations;

namespace Ticket.Model;

public class User
{
    [Key]
    [Required]
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Senha { get; set; }
}
