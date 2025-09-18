using ExpenseTracker.Entities.Models;

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
}
