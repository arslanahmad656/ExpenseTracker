using ExpenseTracker.Shared.Enums;

namespace ExpenseTracker.Shared.DataTransferObjects;

public record FormHistoryDto(int Id, DateTimeOffset RecordedDate, string Note, FormStatus FormStatus);
