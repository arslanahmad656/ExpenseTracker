namespace ExpenseTracker.Controllers.RouteNames;

public static class FormRoutes
{
    public const string Base = "api/form";

    public const string CreateNew = "submit";

    public const string GetFormComplete = "{formId:int}/details";
}
