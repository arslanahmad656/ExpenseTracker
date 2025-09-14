
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using ExpenseTracker.Validators;
using FluentValidation;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Enums;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Results;

namespace ExpenseTracker.App.ServiceInstallers;

public class ValidatorsInstaller : IServiceInstaller
{
    public void Install(WebApplicationBuilder builder)
    {
        builder.Services.AddValidatorsFromAssemblyContaining<AssemblyReference>();

        builder.Services.AddFluentValidationAutoValidation(config =>
        {
            config.DisableBuiltInModelValidation = true;
            config.ValidationStrategy = ValidationStrategy.All;
            config.EnableBodyBindingSourceAutomaticValidation = true;
            config.EnableFormBindingSourceAutomaticValidation = true;
            config.EnableQueryBindingSourceAutomaticValidation = true;

            config.OverrideDefaultResultFactoryWith<LoggingValidationResultFactory>();
        });
    }
}

public class LoggingValidationResultFactory(ILogger<LoggingValidationResultFactory> logger) : IFluentValidationAutoValidationResultFactory
{
    public IActionResult CreateActionResult(ActionExecutingContext context, ValidationProblemDetails? validationProblemDetails)
    {
        if (validationProblemDetails?.Errors?.Count > 0 == true)
        {
            logger.LogWarning("Validation failed with errors: {@ValidationErrors}", validationProblemDetails.Errors);
        }

        return new BadRequestObjectResult(new
        {
            Title = "Validation errors",
            ValidationErrors = validationProblemDetails?.Errors
        });
    }
}