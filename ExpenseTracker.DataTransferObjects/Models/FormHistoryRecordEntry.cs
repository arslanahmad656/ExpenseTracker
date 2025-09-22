using ExpenseTracker.Shared.Enums;

namespace ExpenseTracker.Shared.Models;

public record FormHistoryRecordEntry(string ActorName, DateTimeOffset ActionDate, FormStatus ActionType, string Note);
