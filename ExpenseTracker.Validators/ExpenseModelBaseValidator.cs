using ExpenseTracker.Shared.Models;
using FluentValidation;

namespace ExpenseTracker.Validators;

public abstract class ExpenseModelBaseValidator<T> : AbstractValidator<T>
    where T : ExpenseModelBase
{
    protected ExpenseModelBaseValidator()
    {
        RuleFor(f => f.Description)
            .NotEmpty()
            .NotNull()
            .WithMessage("Description is required.");

        RuleFor(f => f.Amount)
            .GreaterThan(0)
            .WithMessage("Amount must be greater than 0.");

        RuleFor(f => f.ExpenseDate)
            .LessThanOrEqualTo(DateTimeOffset.Now)
            .WithMessage("Expense date cannot be in the future.");
    }
}