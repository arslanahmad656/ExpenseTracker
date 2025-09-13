using ExpenseTracker.Shared.Enums;

namespace ExpenseTracker.Shared.DataTransferObjects;

public record ExpenseHistory(int Id, DateTimeOffset RecordedDate, string RejectionReason, ExpenseStatus ExpenseStatus);
