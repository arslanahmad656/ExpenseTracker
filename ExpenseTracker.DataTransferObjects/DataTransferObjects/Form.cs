using ExpenseTracker.Shared.Enums;

namespace ExpenseTracker.Shared.DataTransferObjects;

public record Form(int Id, string Title, FormStatus FormStatus, Currency Currency);
