namespace ExpenseTracker.Shared.DataTransferObjects;

public record Employee(int Id, string Code, string Name, DateTimeOffset HireDate);
