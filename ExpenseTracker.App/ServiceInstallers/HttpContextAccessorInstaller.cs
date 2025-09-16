
namespace ExpenseTracker.App.ServiceInstallers;

public class HttpContextAccessorInstaller : IServiceInstaller
{
    public void Install(WebApplicationBuilder builder)
    {
        builder.Services.AddHttpContextAccessor();
    }
}
