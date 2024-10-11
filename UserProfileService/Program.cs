using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UserProfileService.Models;

namespace UserProfileService;

public class Program
{
    private static UserDbContext? _context;

    static async Task Main(string[] args)
    {
        // Set up the DB context
        SetupContext();
    }

    private static void SetupContext()
    {
        // Configure DB context options
        var optionsBuilder = new DbContextOptionsBuilder<UserDbContext>();
        optionsBuilder.UseSqlServer("Server=user-database,1433;Database=UserDatabase;User Id=sa;Password=iLuvUsers4ever<3;TrustServerCertificate=True");
        var options = optionsBuilder.Options;

        // Configure context
        _context = new UserDbContext(options);

        Console.WriteLine("DB Context created for User Service");
    }

    public static async Task AddUser(string username, string displayName)
    {
        if (username.IsNullOrEmpty() || displayName.IsNullOrEmpty())
            throw new ArgumentException("Arguments empty or is not provided");

        // Create the user object
        User user = new User { Username = username, DisplayName = displayName };

        // Context operations
        if (_context != null)
        {
            // Add the user to the context
            _context.Users.Add(user);

            // Save the changes to the database
            await _context.SaveChangesAsync();
        }

        Console.WriteLine($"Added '{user.Username}' with display name '{user.DisplayName}' to the database");
    }
}
