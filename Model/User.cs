using Microsoft.AspNetCore.Identity;

namespace Ticket.Model;

public class User: IdentityUser
{
    public string? Cpf { get; set; }

    public int YearsOld { get; set; }
}
