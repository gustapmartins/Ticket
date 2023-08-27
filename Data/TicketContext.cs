using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Ticket.Model;

namespace Ticket.Data;

public class TicketContext: DbContext
{
    public TicketContext(DbContextOptions<TicketContext> opts)
        : base(opts)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
    }

    public DbSet<Category> Categorys { get; set; }
}