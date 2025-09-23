
using ExpenseTracker.Shared.Enums;

namespace ExpenseTracker.App.ServiceInstallers;

public class AuthorizationInstaller : IServiceInstaller
{
    public void Install(WebApplicationBuilder builder)
    {
        builder.Services.AddAuthorizationBuilder()
            .AddPolicy("AdministratorOnly", policy =>
                policy.RequireRole(BuiltInRole.Administrator.ToString()))
            .AddPolicy("ManagerOnly", policy =>
                policy.RequireRole(BuiltInRole.Manager.ToString()))
            .AddPolicy("EmployeeOnly", policy =>
                policy.RequireRole(BuiltInRole.Employee.ToString()))
            .AddPolicy("AccountantOnly", policy =>
                policy.RequireRole(BuiltInRole.Accountant.ToString()));
    }
}
