using Microsoft.EntityFrameworkCore;

namespace TweetPostingService;

public class Program
{
    private static TweetDbContext? _context;

    static async Task Main(string[] args)
    {
        // Set up the DB context
        //SetupContext();
    }

    private static void SetupContext()
    {
        // Configure DB context options
        var optionsBuilder = new DbContextOptionsBuilder<TweetDbContext>();
        optionsBuilder.UseSqlServer("Server=tweet-database,1433;Database=TweetDatabase;User Id=sa;Password=iLuvTweets4ever<3;TrustServerCertificate=True");
        var options = optionsBuilder.Options;

        // Configure context
        _context = new TweetDbContext(options);

        Console.WriteLine("DB Context created for Tweet Service");
    }
}
