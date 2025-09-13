using ExpenseTracker.App.ServiceInstallers;
using Serilog;
using System.Reflection;

namespace ExpenseTracker.App.Extensions;

public static class ApplicationExtensions
{
    public static void InstallServices(this WebApplicationBuilder builder)
    {
        var assembly = Assembly.GetExecutingAssembly();

        var installers = assembly.DefinedTypes
            .Where(t => typeof(IServiceInstaller).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
            .Select(Activator.CreateInstance)
            .Cast<IServiceInstaller>();

        foreach (var installer in installers)
        {
            installer.Install(builder);
        }
    }

    public static void RunApp(this WebApplication app)
    {
        try
        {
            Log.Information("Starting up the application...");
            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application failed to start");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
