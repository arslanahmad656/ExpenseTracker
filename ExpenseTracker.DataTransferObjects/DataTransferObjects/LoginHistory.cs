namespace ExpenseTracker.Shared.DataTransferObjects;

public record LoginHistory(int Id, DateTimeOffset? LoginTime, DateTimeOffset? LogoutTime, string IPAddress, Role Role, Principal Principal);
