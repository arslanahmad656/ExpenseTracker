using ExpenseTracker.Shared.Enums;

namespace ExpenseTracker.Shared.DataTransferObjects;

public record FormDto(int Id, string Title, FormStatus Status, string RejectionReason, string TrackingId, CurrencyDto Currency, DateTimeOffset LastUpdatedOn, List<ExpenseDto> Expenses);
