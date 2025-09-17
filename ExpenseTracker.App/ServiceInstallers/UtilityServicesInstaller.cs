
using ExpenseTracker.Contracts;
using ExpenseTracker.Services;

namespace ExpenseTracker.App.ServiceInstallers;

public class UtilityServicesInstaller : IServiceInstaller
{
    public void Install(WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IPasswordHasher, BCrypttPasswordHasher>();
        builder.Services.AddSingleton<ITrackingIdGenerator, GuidTrackingIdGenerator>();
        builder.Services.AddSingleton<ISerializer, JsonSerializer>();
    }
}
