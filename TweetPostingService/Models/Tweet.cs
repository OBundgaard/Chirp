using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TweetPostingService.Models;

public class Tweet
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TweetID { get; set; }

    [Required]
    public int AuthorID { get; set; }

    [Required]
    public string? Content { get; set; }
}
