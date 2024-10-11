﻿using UserProfileService.Models;
using Microsoft.EntityFrameworkCore;

namespace UserProfileService;

public class UserDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserStatistic> UserStatistics { get; set; }

    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Users
        modelBuilder.Entity<User>().HasKey(u => u.UserID);
        modelBuilder.Entity<User>().Property(u => u.UserID).ValueGeneratedOnAdd();

        // User statistics
        modelBuilder.Entity<UserStatistic>().HasKey(s => s.UserID);
        modelBuilder.Entity<UserStatistic>().Property(s => s.UserID).ValueGeneratedNever();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("An error occurred while saving changes to the database.", ex);
        }
    }
}
