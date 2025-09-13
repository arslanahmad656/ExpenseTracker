using Serilog;

namespace ExpenseTracker.App.ServiceInstallers;

public class LoggerInstaller : IServiceInstaller
{
    public void Install(WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .Enrich.FromLogContext()
            .CreateLogger();

        builder.Host.UseSerilog();

        builder.Services.AddSingleton<Contracts.Logging.ILoggerManager, Logger.SeriLogger>();
        builder.Services.AddSingleton(typeof(Contracts.Logging.Generic.ILoggerManager<>), typeof(Logger.Generic.SeriLogger<>));
    }
}
