using ExpenseTracker.Shared.Enums;

namespace ExpenseTracker.Services.Utility;

public record ExpenseHistoryState(string? Details, decimal? Amount, DateTimeOffset? Date, ExpenseStatus? Status);
