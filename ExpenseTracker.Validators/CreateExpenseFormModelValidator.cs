using ExpenseTracker.Shared.Models;
using FluentValidation;

namespace ExpenseTracker.Validators;

public class CreateExpenseFormModelValidator : AbstractValidator<CreateExpenseFormModel>
{
    public CreateExpenseFormModelValidator()
    {
        RuleFor(f => f.Title)
            .NotEmpty()
            .NotNull()
            .WithMessage("Title is required.");

        RuleFor(f => f.CurrencyCode)
            .NotNull()
            .NotEmpty()
            .WithMessage($"Currency code is required.");

        RuleFor(f => f.Expenses)
            .NotEmpty()
            .WithErrorCode($"Cannot create an expense form without any expenses.");

        RuleForEach(f => f.Expenses)
            .SetValidator(new CreateExpenseModelValidator());
    }
}
