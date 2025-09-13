
namespace ExpenseTracker.App.ServiceInstallers;

public class SwaggerInstaller : IServiceInstaller
{
    public void Install(WebApplicationBuilder builder)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
    }
}
