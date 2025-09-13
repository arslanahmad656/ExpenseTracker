namespace ExpenseTracker.Shared.Models;

public record AuthenticationResponse(string Token, DateTimeOffset Expiry);
