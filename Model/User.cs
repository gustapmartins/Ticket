using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Ticket.Model;

public class User: IdentityUser
{
    public string? Name { get; set; }

    public int? YearsOld { get; set; }

    public string? Cpf { get; set; }
}
