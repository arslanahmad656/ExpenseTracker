using ExpenseTracker.Shared.Models;
using FluentValidation;

namespace ExpenseTracker.Validators;

public class DenialModelValidator : AbstractValidator<DenialModel>
{
    public DenialModelValidator()
    {
        RuleFor(m => m.Reason)
            .NotNull()
            .NotEmpty();
    }
}
