
namespace ExpenseTracker.App.ServiceInstallers;

public class AuthorizationInstaller : IServiceInstaller
{
    public void Install(WebApplicationBuilder builder)
    {
        builder.Services.AddAuthorization();
    }
}
