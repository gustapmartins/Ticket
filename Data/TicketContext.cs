using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ticket.Model;

namespace Ticket.Data;

public class TicketContext: IdentityDbContext<Users>
{
    public TicketContext(DbContextOptions<TicketContext> opts)
        : base(opts)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Category> Categorys { get; set; }
    public DbSet<Tickets> Tickets { get; set; }
    public DbSet<Show> Shows { get; set; }
    public DbSet<PasswordReset> PasswordResets { get; set; }
    public DbSet<Cart> Carts { get; set; }
}