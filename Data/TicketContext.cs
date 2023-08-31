using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Ticket.Model;

namespace Ticket.Data;

public class TicketContext: IdentityDbContext<User>
{
    public TicketContext(DbContextOptions<TicketContext> opts)
        : base(opts)
    {

    }

    public DbSet<Category> Categorys { get; set; }
}