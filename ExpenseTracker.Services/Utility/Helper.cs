using ExpenseTracker.Contracts;
using ExpenseTracker.Contracts.Repositories;
using ExpenseTracker.Entities.Models;

namespace ExpenseTracker.Services.Utility;

public static class Helper
{
    public static List<string> FindChangedKeys(IDictionary<string, object?> dict1, IDictionary<string, object?> dict2)
    {
        HashSet<string> allKeys = [.. dict1.Keys];
        allKeys.UnionWith(dict2.Keys);

        List<string> changedKeys = [];

        foreach (string key in allKeys)
        {
            string? str1 = dict1.TryGetValue(key, out object? value1) ? value1?.ToString() : null;
            string? str2 = dict2.TryGetValue(key, out object? value2) ? value2?.ToString() : null;

            if (str1 != str2)
            {
                changedKeys.Add(key);
            }
        }

        return changedKeys;
    }

    public static async Task<(Expense expense, string? previousState, string currentState)> GetExpenseWithStates(IRepositoryManager repositoryManager, ISerializer serializer, int expenseId)
    {
        var expense = await repositoryManager.ExpenseRepository.GetByIdWithNavigations(expenseId).ConfigureAwait(false);

        var previousState = GetExpensePreviousState(expense);   // the current state of the most recent history entry
        var currentState = GetExpenseHistorySerializedText(serializer, expense);

        return (expense, previousState, currentState);
    }

    public static async Task<(Form form, string? previousState, string currentState)> GetFormWithStates(IRepositoryManager repositoryManager, ISerializer serializer, int formId)
    {
        var form = await repositoryManager.FormRepository.GetByIdWithNavigations(formId).ConfigureAwait(false);

        var previousState = GetFormPreviousState(form!);   // the current state of the most recent history entry
        var currentState = GetFormHistorySerializedText(serializer, form!);

        return (form!, previousState, currentState);
    }

    private static string? GetFormPreviousState(Form form)
    {
        var entry = form.FormHistories.OrderBy(h => h.RecordedDate).LastOrDefault();
        return entry?.CurrentState;
    }

    private static string? GetExpensePreviousState(Expense expense)
    {
        var entry = expense.ExpenseHistories.OrderBy(h => h.RecordedDate).LastOrDefault();
        return entry?.CurrentState;
    }

    private static string GetExpenseHistorySerializedText(ISerializer serializer, Expense expense)
    {
        var historyDto = expense.ToExpenseHistoryState();
        var state = serializer.Serialize(historyDto);
        return state;
    }

    private static string GetFormHistorySerializedText(ISerializer serializer, Form form)
    {
        var historyDto = form.ToFormHistoryState();
        var state = serializer.Serialize(historyDto);
        return state;
    }
}
