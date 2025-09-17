using AutoMapper;
using ExpenseTracker.Contracts;
using ExpenseTracker.Contracts.Logging.Generic;
using ExpenseTracker.Contracts.Repositories;
using ExpenseTracker.Contracts.Services;
using ExpenseTracker.Entities.Models;
using ExpenseTracker.Shared.DataTransferObjects;
using ExpenseTracker.Shared.Models;

namespace ExpenseTracker.Services.DataServices;

public partial class FormService(
    IRepositoryManager repositoryManager,
    ILoggerManager<FormService> logger,
    ITrackingIdGenerator trackingIdGenerator,
    IAuthenticationService authenticationService,
    IMapper mapper,
    ISerializer serializer
) : IFormService
{
    private readonly IRepositoryManager repositoryManager = repositoryManager;

    public async Task<int> SubmitExpenseForm(CreateExpenseFormModel expenseForm, IEnumerable<CreateExpenseModel> expenses)
    {
        logger.LogInfo("Creating expense form {@expenseForm}.", expenseForm);
        logger.LogDebug("Validating the arguments.");

        await Validate(expenseForm, expenses).ConfigureAwait(false);
        logger.LogDebug($"Validation successful.");

        logger.LogDebug("Getting current context user's ID.");

        var userId = authenticationService.GetCurrentUserId();
        if (userId is null || !int.TryParse(userId, out var userIdParsed))
        {
            logger.LogWarn($"Could not retrieve the current context user id.");
            throw new ApplicationException($"Could not retrieve the current context user id. Cannot continue futher.");
        }

        logger.LogInfo($"Context user id: {userIdParsed}.");

        logger.LogDebug($"Getting the currency by code {expenseForm.CurrencyCode}.");
        var currency = await repositoryManager.CurrencyRepository.GetCurrencyByCode(expenseForm.CurrencyCode).ConfigureAwait(false);
        if (currency is null)
        {
            logger.LogWarn($"Currency not found with code {expenseForm.CurrencyCode}.");
            throw new InvalidOperationException($"Cannot create an expense form without a valid currency.");
        }

        logger.LogDebug("Preparing the entity for insertion.");
        var form = PrepareFormForCreation(expenseForm.Title, currency.Id, userIdParsed, expenses);

        logger.LogDebug("Performing insertion.");
        await repositoryManager.FormRepository.Create(form).ConfigureAwait(false);
        await repositoryManager.Save();
        logger.LogInfo($"Expense form created with id {form.Id}.");

        return form.Id;
    }

    public async Task<FormDto> GetFormDetailedOwnedByCurrentUser(int formId)
    {
        logger.LogDebug("Getting expense form with id {formId}", formId);

        var form = await GetFormOwnedByCurrentUser(formId).ConfigureAwait(false);

        logger.LogDebug("Retrieved form with id {formId}", formId);

        var formDto = mapper.Map<FormDto>(form);
        formDto = formDto with
        {
            LastUpdatedOn = form.FormHistories.MaxBy(fh => fh.RecordedDate)?.RecordedDate ?? throw new ApplicationException($"Could not determine the last update date f the form {formDto.Id}."),
            Expenses = [.. form.Expenses.Select(e => mapper.Map<Expense, ExpenseDto>(e) with
            {
                LastUpdatedOn = e.ExpenseHistories.MaxBy(eh => eh.RecordedDate)?.RecordedDate ?? throw new ApplicationException($"Could not determine the last update date of the expense {e.Id}.")
            })]
        };

        return formDto;
    }

    public async Task UpdateExpenseFormForCurrentUser(int formId, UpdateExpenseFormModel expenseForm)
    {
        logger.LogDebug("Updating the expense form {@expenseForm}.", expenseForm);

        var form = await GetFormOwnedByCurrentUser(formId).ConfigureAwait(false);
    }
}
