using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using Ticket.Model;

namespace Ticket.Data;

public class TicketContext: IdentityDbContext<Users>
{
    public TicketContext(DbContextOptions<TicketContext> opts)
        : base(opts)
    {
    }

    public DbSet<Category> Categorys { get; set; }
    public DbSet<Tickets> Tickets { get; set; }
    public DbSet<Show> Shows { get; set; }   
}