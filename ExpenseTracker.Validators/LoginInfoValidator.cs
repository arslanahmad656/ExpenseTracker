using ExpenseTracker.Shared.Models;
using FluentValidation;

namespace ExpenseTracker.Validators;

public class LoginInfoValidator : AbstractValidator<LoginInfo>
{
    public LoginInfoValidator()
    {
        RuleFor(i => i.Username)
            .NotNull()
            .NotEmpty()
            .WithMessage("Username is required.");

        RuleFor(i => i.Password)
            .NotNull()
            .NotEmpty()
            .WithMessage("Password is required.");
    }
}
