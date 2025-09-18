using ExpenseTracker.Shared.Enums;

namespace ExpenseTracker.Shared.DataTransferObjects;

public record ExpenseHistoryDto(int Id, DateTimeOffset RecordedDate, string Note, ExpenseStatus ExpenseStatus);
