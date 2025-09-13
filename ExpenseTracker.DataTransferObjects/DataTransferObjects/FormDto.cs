using ExpenseTracker.Shared.Enums;

namespace ExpenseTracker.Shared.DataTransferObjects;

public record FormDto(int Id, string Title, FormStatus FormStatus, CurrencyDto Currency);
