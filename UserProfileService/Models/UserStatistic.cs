using System.ComponentModel.DataAnnotations;

namespace UserProfileService.Models;

public class UserStatistic
{
    [Key]
    public int UserID { get; set; }

    [Required]
    public int TweetAmount { get; set; }
}
