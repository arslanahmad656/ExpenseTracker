
using ExpenseTracker.Contracts;
using ExpenseTracker.Services;

namespace ExpenseTracker.App.ServiceInstallers;

public class PasswordServiceInstaller : IServiceInstaller
{
    public void Install(WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IPasswordHasher, BCrypttPasswordHasher>();
    }
}
