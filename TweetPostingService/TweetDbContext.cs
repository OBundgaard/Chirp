using TweetPostingService.Models;
using Microsoft.EntityFrameworkCore;

namespace TweetPostingService;

public class TweetDbContext : DbContext
{
    public DbSet<Tweet> Tweets { get; set; }

    public TweetDbContext(DbContextOptions<TweetDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tweet>().HasKey(t => t.TweetID);
        modelBuilder.Entity<Tweet>().Property(t => t.TweetID).ValueGeneratedOnAdd();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = "Server=tweet-database,1433;Database=TweetDatabase;User Id=sa;Password=iLuvTweets4ever<3;TrustServerCertificate=True";
            optionsBuilder.UseSqlServer(connectionString);
        }
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
