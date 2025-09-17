using ExpenseTracker.Shared.Models;
using FluentValidation;

namespace ExpenseTracker.Validators;

public abstract class ExpenseFormModelBaseValidator<T> : AbstractValidator<T>
    where T : ExpenseFormModelBase
{
    protected ExpenseFormModelBaseValidator()
    {
        RuleFor(f => f.CurrencyCode)
            .NotEmpty()
            .NotNull()
            .WithMessage("Currency code is required.");

        RuleFor(f => f.Title)
            .NotEmpty()
            .NotNull()
            .WithMessage("Title is required.");
    }
}
