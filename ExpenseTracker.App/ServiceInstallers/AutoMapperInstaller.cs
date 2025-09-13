
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseTracker.App.ServiceInstallers;

public class AutoMapperInstaller : IServiceInstaller
{
    public void Install(WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(typeof(Program).Assembly);
    }
}
