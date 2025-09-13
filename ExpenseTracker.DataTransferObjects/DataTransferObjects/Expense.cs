using ExpenseTracker.Shared.Enums;

namespace ExpenseTracker.Shared.DataTransferObjects;

public record Expense(int Id, string Details, decimal Amount, DateTimeOffset Date, ExpenseStatus ExpenseStatus);
