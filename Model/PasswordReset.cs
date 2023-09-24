using System.ComponentModel.DataAnnotations;

namespace Ticket.Model;

public class PasswordReset
{
    [Key]
    [Required]
    public int Id { get; set; }

    public string? Token {  get; set; } 

    public string? Email { get; set; }

    public DateTime TokenExpire { get; set; }
}
