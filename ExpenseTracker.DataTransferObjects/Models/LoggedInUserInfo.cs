namespace ExpenseTracker.Shared.Models;

public record LoggedInUserInfo(string Username, string PrimaryRole, List<string> Roles);
