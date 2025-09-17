using ExpenseTracker.Shared.Models;
using FluentValidation;

namespace ExpenseTracker.Validators;

public class CreateExpenseFormCompositeValidator : AbstractValidator<CreateExpenseForm>
{
    public CreateExpenseFormCompositeValidator()
    {
        RuleFor(cf => cf.Form)
            .NotNull()
            .SetValidator(new CreateExpenseFormModelValidator());

        RuleFor(cf => cf.Expenses)
            .NotNull()
            .NotEmpty()
            .WithMessage("Expense cannot be empty.");

        RuleForEach(cf =>  cf.Expenses)
            .SetValidator(new CreateExpenseModelValidator());
    }
}
