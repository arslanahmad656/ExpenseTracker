using ExpenseTracker.Shared.Models;
using FluentValidation;

namespace ExpenseTracker.Validators;

public class CreateExpenseModelValidator : AbstractValidator<CreateExpenseModel>
{
    public CreateExpenseModelValidator()
    {
        RuleFor(c => c.Description)
            .NotEmpty()
            .NotNull()
            .WithMessage($"Expense description is required.");

        RuleFor(c => c.Amount)
            .GreaterThan(0)
            .WithMessage($"Expense amount must be greater than zero.");

        RuleFor(c => c.ExpenseDate)
            .LessThan(DateTime.Now)
            .WithMessage($"Expense date cannot be from future.");
    }
}
