using ExpenseTracker.Entities.Models;
using ExpenseTracker.Shared.Enums;
using ExpenseTracker.Shared.Models;

namespace ExpenseTracker.Services.DataServices;

public partial class FormHistoryService
{
    private async Task SaveExpenseHistoryEntry(ExpenseHistory entry)
    {
        await repositoryManager.ExpenseHistoryRepository.Add(entry).ConfigureAwait(false);
        await repositoryManager.Save().ConfigureAwait(false);
    }

    private async Task SaveFormHistoryEntry(FormHistory entry)
    {
        await repositoryManager.FormHistoryRepository.Add(entry).ConfigureAwait(false);
        await repositoryManager.Save().ConfigureAwait(false);
    }

    private void ValidateManager()
    {
        if (!authenticationService.IsCurrentUserInRole(BuiltInRole.Administrator.ToString()))
        {
            throw new ApplicationException($"Only administrators are allowed to view the history views.");
        }
    }

    private static string GetDescriptionFromHistoryRecord(FormHistoryRecordEntry entry, bool isFirst) => entry.ActionType switch
    {
        FormStatus.PendingApproval when isFirst => $"User {entry.ActorName} submitted the expense form at {FormatDateForHistoryDescription(entry.ActionDate)}.",
        FormStatus.PendingApproval => $"User {entry.ActorName} updated the expense form at {FormatDateForHistoryDescription(entry.ActionDate)}.",
        FormStatus.PendingReimbursement => $"User {entry.ActorName} approved the expense form at {FormatDateForHistoryDescription(entry.ActionDate)}.",
        FormStatus.Reimbursed => $"User {entry.ActorName} reimbursed the expense form at {FormatDateForHistoryDescription(entry.ActionDate)}.",
        FormStatus.Rejected => $"User {entry.ActorName} rejected the expense form at {FormatDateForHistoryDescription(entry.ActionDate)} with reason {entry.Note}.",
        FormStatus.Cancelled => $"User {entry.ActorName} cancelled the expense form at {FormatDateForHistoryDescription(entry.ActionDate)} with reason {entry.Note}.",
        _ => throw new ArgumentOutOfRangeException($"{entry.ActionType} is not recognized.")
    };

    private static string FormatDateForHistoryDescription(DateTimeOffset date) => date.ToString("yyyy-MM-dd HH:mm:ss");
}
