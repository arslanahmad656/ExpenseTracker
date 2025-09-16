using ExpenseTracker.Shared.Enums;

namespace ExpenseTracker.Shared.DataTransferObjects;

public record ExpenseDto(int Id, string Details, decimal Amount, DateTimeOffset Date, DateTimeOffset LastUpdatedOn, string TrackingId, ExpenseStatus Status);
