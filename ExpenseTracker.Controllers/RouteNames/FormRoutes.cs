namespace ExpenseTracker.Controllers.RouteNames;

public static class FormRoutes
{

    public const string Base = "api/form";

    public const string CreateNew = "submit";

    public const string GetFormComplete = "{formId:int}/details";

    public const string CancelExpense = "expense/{expenseId:int}/cancel";
    
    public const string CancelForm = "{formId:int}/cancel";
    
    //public const string RejectExpense = "expense/{expenseId:int}/reject";
    
    //public const string ApproveExpense = "expense/{expenseId:int}/approve";
    
    public const string RejectForm = "{formId:int}/reject";
    
    public const string ApproveForm = "{formId:int}/approve";
    
    //public const string ReimburseExpense = "expense/{expenseId:int}/reimburse";
    
    public const string ReimburseForm = "{formId:int}/reimburse";
    
    public const string UpdateForm = "{formId:int}/update";
}
