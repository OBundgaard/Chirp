using System.ComponentModel.DataAnnotations;

namespace TweetPostingService.Models;

public class Tweet
{
    [Key]
    public int TweetID { get; set; }

    [Required]
    public int AuthorID { get; set; }

    [Required]
    public string? Content { get; set; }
}
