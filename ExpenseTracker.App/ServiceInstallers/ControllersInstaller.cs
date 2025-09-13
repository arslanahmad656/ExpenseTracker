
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseTracker.App.ServiceInstallers;

public class ControllersInstaller : IServiceInstaller
{
    public void Install(WebApplicationBuilder builder)
    {
        builder.Services.AddControllers()
            .AddApplicationPart(typeof(Controllers.AssemblyReference).Assembly);
    }
}
