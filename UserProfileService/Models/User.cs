using System.ComponentModel.DataAnnotations;

namespace UserProfileService.Models;

public class User
{
    [Key]
    public int UserID { get; set; }

    [Required]
    public string? Username { get; set; }

    [Required]
    public string? DisplayName { get; set; }
}
