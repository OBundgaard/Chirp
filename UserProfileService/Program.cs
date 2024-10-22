using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UserProfileService.Models;

namespace UserProfileService;

public class Program
{
    private static UserDbContext? _context;

    static async Task Main(string[] args)
    {

        SetupContext();
    }

    private static void SetupContext()
    {

        var optionsBuilder = new DbContextOptionsBuilder<UserDbContext>();
        optionsBuilder.UseSqlServer("Server=user-database,1433;Database=UserDatabase;User Id=sa;Password=iLuvUsers4ever<3;TrustServerCertificate=True");
        var options = optionsBuilder.Options;


        _context = new UserDbContext(options);

        Console.WriteLine("DB Context created for User Service");
    }

    public static async Task<bool> AddUser(string username, string displayName)
    {
        if (username.IsNullOrEmpty() || displayName.IsNullOrEmpty())
            throw new ArgumentException("Arguments empty or is not provided");


        User user = new User { Username = username, DisplayName = displayName };


        if (_context == null)
            return false;

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        
        Console.WriteLine($"Added '{user.Username}' with display name '{user.DisplayName}' to the database");

        return true;
    }

    public static async Task<User?> GetUser(int userID)
    {
        if (userID < 0)
            throw new ArgumentException("UserID is below 0");

        if (_context == null)
            return null;

        return await _context.Users.FirstOrDefaultAsync(u => u.UserID == userID);
    }

    public static async Task<bool> DeleteUser(int userID)
    {
        if (_context == null)
            return false;

        User? user = await _context.Users.FirstOrDefaultAsync(u => u.UserID == userID);
        if (user == null)
        {
            Console.WriteLine("User does not exist");
            return false;
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        Console.WriteLine("User deleted successfully");

        return true;
    }
}
