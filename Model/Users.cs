using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ticket.Model;

public class Users: IdentityUser
{
    public string? Cpf { get; set; }
    public int YearsOld { get; set; }
    public string Role { get; set; }
    public virtual List<Tickets> Tickets { get; set; }
    public decimal TotalPrice { get; set; }
}
