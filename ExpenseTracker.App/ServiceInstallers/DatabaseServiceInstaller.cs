
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
            options.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection")));

        builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
        builder.Services.AddScoped<IServiceManager, ServiceManager>();
    }
}
