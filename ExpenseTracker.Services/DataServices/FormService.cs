using AutoMapper;
using ExpenseTracker.Contracts;
using ExpenseTracker.Contracts.Logging.Generic;
using ExpenseTracker.Contracts.Repositories;
using ExpenseTracker.Contracts.Services;
using ExpenseTracker.Entities.Models;
using ExpenseTracker.Shared.DataTransferObjects;
using ExpenseTracker.Shared.Enums;
using ExpenseTracker.Shared.Models;
using Microsoft.Extensions.Logging;

namespace ExpenseTracker.Services.DataServices;

public class FormService(
    IRepositoryManager repositoryManager,
    ILoggerManager<FormService> logger,
    ITrackingIdGenerator trackingIdGenerator,
    IAuthenticationService authenticationService,
    IMapper mapper
) : IFormService
{
    private readonly IRepositoryManager repositoryManager = repositoryManager;

    public async Task<int> SubmitExpenseForm(CreateExpenseFormModel expenseForm)
    {
        logger.LogInfo("Creating expense form {@expenseForm}.", expenseForm);
        logger.LogDebug("Validating the arguments.");

        await ValidateForm(expenseForm).ConfigureAwait(false);
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
        var form = PrepareFormForCreation(expenseForm, currency.Id, userIdParsed);

        logger.LogDebug("Performing insertion.");
        await repositoryManager.FormRepository.Create(form).ConfigureAwait(false);
        await repositoryManager.Save();
        logger.LogInfo($"Expense form created with id {form.Id}.");

        return form.Id;
    }

    public async Task<FormDto> GetExpenseFormFullForTheCurrentUser(int formId)
    {
        logger.LogDebug("Getting expense form with id {formId}", formId);

        logger.LogDebug("Getting current principal id.");
        var currentPrincipalId = authenticationService.GetCurrentUserId() ?? throw new UnauthorizedAccessException();
        logger.LogDebug("Principal ID: {principalId}", currentPrincipalId);

        logger.LogDebug("Call to the db.");
        var form = await repositoryManager.FormRepository.GetById(formId, int.Parse(currentPrincipalId)).ConfigureAwait(false);
        if (form == null)
        {
            logger.LogWarn($"Form not found with id {formId}.");

            throw new ApplicationException($"Form with id {formId} not found for the user id {currentPrincipalId}.");
        }

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

    private Form PrepareFormForCreation(CreateExpenseFormModel expenseForm, int currencyId, int userId)
    {
        var now = DateTime.Now;
        var form = new Form
        {
            CurrencyId = currencyId,
            Title = expenseForm.Title,
            TrackingId = trackingIdGenerator.Generate(),
            Status = FormStatus.PendingApproval,
            FormHistories = 
            [
            new()
            {
                RecordedDate = now,
                Status = FormStatus.PendingApproval,
                ActorId = userId,
            }
        ],
            Expenses = [.. expenseForm.Expenses.Select(e => new Expense
            {
                Amount = e.Amount,
                Date = e.ExpenseDate,
                Details = e.Description,
                Status = ExpenseStatus.PendingApproval,
                TrackingId = trackingIdGenerator.Generate(),
                ExpenseHistories =
                [
                    new()
                    {
                        ActorId = userId,
                        RecordedDate = now,
                        Status = FormStatus.PendingApproval
                    }
                ]
            })],
        };

        return form;
    }

    private async Task ValidateForm(CreateExpenseFormModel expenseForm)
    {
        var currency = await repositoryManager.CurrencyRepository.GetCurrencyByCode(expenseForm.CurrencyCode).ConfigureAwait(false);
        if (currency is null)
        {
            logger.LogWarn($"Currency is null.");
            throw new InvalidOperationException($"Cannot create an expense form without a valid currency.");
        }

        if (expenseForm.Expenses.Count == 0)
        {
            logger.LogWarn($"Expenses list is empty.");
            throw new InvalidOperationException($"Cannot create an expense form without any expenses.");
        }

        expenseForm.Expenses.ForEach(ValidateExpense);
    }

    private void ValidateExpense(CreateExpenseModel expense)
    {
        if (expense.ExpenseDate >= DateTime.Now)
        {
            logger.LogWarn($"Expense date cannot be from futrue.");
            throw new InvalidOperationException($"Expense date {expense} is not allowed since it's from future.");
        }
    }
}
