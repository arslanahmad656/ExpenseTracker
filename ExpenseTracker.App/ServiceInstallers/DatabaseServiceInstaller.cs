
using ExpenseTracker.Contracts.Repositories;
using ExpenseTracker.Contracts.Services;
using ExpenseTracker.Repository;
using ExpenseTracker.Repository.Repositories;
using ExpenseTracker.Services.DataServices;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.App.ServiceInstallers;

public class DatabaseServiceInstaller : IServiceInstaller
{
    public void Install(WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<ExpenseTrackerDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection"));

            var enableSensitiveData = builder.Configuration.GetValue<bool>("EnableEfCoreParameterValuesLogging");

            if (enableSensitiveData)
            {
                options.EnableSensitiveDataLogging(true);
            }

        });

        


        builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
        builder.Services.AddScoped<IFormHistoryService, FormHistoryService>();
        builder.Services.AddScoped<IFormService, FormService>();
        builder.Services.AddScoped<ILoginHistoryService, LoginHistoryService>();

        builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
        builder.Services.AddScoped<IServiceManager, ServiceManager>();
    }
}
