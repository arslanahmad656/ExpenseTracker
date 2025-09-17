using ExpenseTracker.Shared.Enums;

namespace ExpenseTracker.Services.Utility;

public record FormHistoryState(string? Title, string? Currency, FormStatus? Status);
