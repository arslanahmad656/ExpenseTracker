namespace ExpenseTracker.Shared.DataTransferObjects;

public record Principal(int Id, string Username, string PasswordHash, bool IsActive);
