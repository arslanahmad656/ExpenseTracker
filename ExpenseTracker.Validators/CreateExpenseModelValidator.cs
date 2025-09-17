using ExpenseTracker.Shared.Models;
using FluentValidation;

namespace ExpenseTracker.Validators;

public class CreateExpenseModelValidator : ExpenseModelBaseValidator<CreateExpenseModel>
{
    public CreateExpenseModelValidator()
    {
    }
}