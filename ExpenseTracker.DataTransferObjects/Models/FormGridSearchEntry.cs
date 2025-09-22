using ExpenseTracker.Shared.Enums;

namespace ExpenseTracker.Shared.Models;

public record FormGridSearchEntry(int FormId, string TrackingId, string Title, string CurrencyCode, decimal Amount, FormStatus Status);
