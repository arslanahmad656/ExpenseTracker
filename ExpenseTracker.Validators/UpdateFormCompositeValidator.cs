using ExpenseTracker.Shared.Models;
using FluentValidation;

namespace ExpenseTracker.Validators;

public class UpdateFormCompositeValidator : AbstractValidator<UpdateFormComposite>
{
    public UpdateFormCompositeValidator()
    {
        RuleFor(v => v.Form)
            .SetValidator(new UpdateExpenseFormModelValidator());

        RuleFor(v => v.Expenses)
            .NotNull();

        RuleForEach(v => v.Expenses)
            .SetValidator(v => new UpdateExpenseModelValidator());
    }
}
