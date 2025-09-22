using ExpenseTracker.App.ServiceInstallers;
using ExpenseTracker.Contracts.Logging;
using Microsoft.AspNetCore.Diagnostics;
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

    public static void ConfigureExceptionHandler(this WebApplication app, ILoggerManager logger)
    {
        app.UseExceptionHandler(errorApp =>
        {
            errorApp.Run(async context =>
            {
                context.Response.ContentType = "application/json";

                var handler = context.Features.Get<IExceptionHandlerFeature>();
                if (handler != null)
                {
                    context.Response.StatusCode = handler.Error switch
                    {
                        UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                        _ => StatusCodes.Status500InternalServerError
                    };

                    logger.LogError(handler.Error, "Error occurred while processing the request.");

                    await context.Response.WriteAsJsonAsync(new
                    {
                        context.Response.StatusCode,
                        Message = handler.Error.Message
                    });
                }
            });
        });
    }
}
