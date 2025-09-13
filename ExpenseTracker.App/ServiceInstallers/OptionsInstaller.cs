
using ExpenseTracker.Shared.Options;

namespace ExpenseTracker.App.ServiceInstallers;

public class OptionsInstaller : IServiceInstaller
{
    public void Install(WebApplicationBuilder builder)
    {
        builder.Services.AddOptions<JwtOptions>()
        .Bind(builder.Configuration.GetSection("Jwt"))
        .ValidateDataAnnotations()
        .Validate(o => !string.IsNullOrWhiteSpace(o.Key), "JWT Key must be provided")
        .ValidateOnStart();
    }
}
