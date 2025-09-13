using ExpenseTracker.Shared.Enums;

namespace ExpenseTracker.Shared.DataTransferObjects;

public record FormHistory(int Id, DateTimeOffset RecordedDate, FormStatus FormStatus);
