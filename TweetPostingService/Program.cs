using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TweetPostingService.Models;

namespace TweetPostingService;

public class Program
{
    private static TweetDbContext? _context;

    static async Task Main(string[] args)
    {
        // Set up the DB context
        SetupContext();
    }

    private static void SetupContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<TweetDbContext>();
        optionsBuilder.UseSqlServer("Server=tweet-database,1433;Database=TweetDatabase;User Id=sa;Password=iLuvTweets4ever<3;TrustServerCertificate=True");
        var options = optionsBuilder.Options;

        _context = new TweetDbContext(options);

        Console.WriteLine("DB Context created for Tweet Service");
    }

    public static async Task<bool> PostTweet(int authorID, string tweetContent)
    {
        if (tweetContent.IsNullOrEmpty())
            throw new ArgumentException("Tweet content is empty or is not provided");

        if (authorID <= 0)
            throw new ArgumentException("Author ID is invalid");

        Tweet tweet = new Tweet { Content = tweetContent, AuthorID = authorID };

        if (_context != null)
        {
            _context.Tweets.Add(tweet);

            await _context.SaveChangesAsync();
        }

        Console.WriteLine($"Added tweet '{tweet.Content}' with ID '{tweet.AuthorID}' to the database");
        return true;

    }

    public static async Task<List<Tweet>> GetTweetsByTweetID(int tweetID)
    {
        if (tweetID <= 0)
            throw new ArgumentException("Tweet ID is invalid");

        
        if (_context != null)
        {
            return await _context.Tweets.Where(t => t.TweetID == tweetID).ToListAsync();
        }

        return new List<Tweet>();
    }

    public static async Task<List<Tweet>> GetTweetsByAuthorID(int authorID)
    {
        if (authorID <= 0)
            throw new ArgumentException("Author ID is invalid");

        if (_context != null)
        {
            return await _context.Tweets.Where(t => t.AuthorID == authorID).ToListAsync();
        }

        return new List<Tweet>();
    }

    public static async Task<List<Tweet>> GetAllTweets()
    {
        if (_context != null)
        {
            return await _context.Tweets.ToListAsync();
        }

        return new List<Tweet>();
    }

    public static async Task DeleteTweet(int tweetID)
    {
        if (tweetID <= 0)
            throw new ArgumentException("Tweet ID is invalid");

        if (_context != null)
        {
            var tweet = await _context.Tweets.Where(t => t.TweetID == tweetID).FirstOrDefaultAsync();

            if (tweet != null)
            {
                _context.Tweets.Remove(tweet);
                await _context.SaveChangesAsync();
            }
        }
    }
}
