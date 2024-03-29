﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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

        modelBuilder.Entity<Category>().HasKey(c => c.Id);

        modelBuilder.Entity<Show>().HasKey(c => c.Id);

        modelBuilder.Entity<Carts>().HasKey(c => c.Id);

        modelBuilder.Entity<CartItem>().HasKey(c => c.Id);

        modelBuilder.Entity<Tickets>().HasKey(c => c.Id);

        modelBuilder.Entity<FeatureToggle>().HasKey(c => c.Id);
    }

    public DbSet<Category> Categorys { get; set; }
    public DbSet<Tickets> Tickets { get; set; }
    public DbSet<Show> Shows { get; set; }
    public DbSet<PasswordReset> PasswordResets { get; set; }
    public DbSet<FeatureToggle> FeatureToggles { get; set; }
    public DbSet<Carts> Carts { get; set; }
    public DbSet<Address> Address { get; set; }
}