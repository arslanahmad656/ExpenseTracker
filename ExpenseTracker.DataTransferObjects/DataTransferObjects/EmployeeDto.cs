namespace ExpenseTracker.Shared.DataTransferObjects;

public record EmployeeDto(int Id, string Code, string Name, DateTimeOffset HireDate);
