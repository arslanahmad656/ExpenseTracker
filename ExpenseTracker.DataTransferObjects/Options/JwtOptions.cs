using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Shared.Options;

public class JwtOptions
{
    [Required]
    public string Key { get; set; } = string.Empty;

    [Required]
    public string Issuer { get; set; } = string.Empty;

    [Required]
    public string Audience { get; set; } = string.Empty;

    [Range(1, 1440, ErrorMessage = "ExpiresMinutes must be between 1 and 1440 (24h).")]
    public int ExpiresMinutes { get; set; }
}
