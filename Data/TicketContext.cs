﻿using Microsoft.AspNetCore.Identity;
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
    public DbSet<Category> Categorys { get; set; }
}