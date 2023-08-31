using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ticket.Model;

namespace Ticket.Data;

public class TicketContext: IdentityDbContext<User>
{
    public TicketContext(DbContextOptions<TicketContext> opts)
        : base(opts)
    {
<<<<<<< HEAD

=======
      
>>>>>>> a92f93c35c6b88aa548345237dedef916c6557c2
    }

    public DbSet<Category> Categorys { get; set; }
}