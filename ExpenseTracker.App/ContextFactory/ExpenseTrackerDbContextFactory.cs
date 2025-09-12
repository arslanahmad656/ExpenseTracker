using ExpenseTracker.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ExpenseTracker.App.ContextFactory;

public class ExpenseTrackerDbContextFactory : IDesignTimeDbContextFactory<ExpenseTrackerDbContext>
{
    public ExpenseTrackerDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();

        var builder = new DbContextOptionsBuilder<ExpenseTrackerDbContext>()
        .UseSqlServer(configuration.GetConnectionString("sqlConnection"),
            b => b.MigrationsAssembly(this.GetType().Assembly.GetName().Name));

        return new ExpenseTrackerDbContext(builder.Options);
    }
}
