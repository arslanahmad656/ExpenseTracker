using ExpenseTracker.App.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.InstallServices();

var app = builder.Build();


app.UseSerilogRequestLogging();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.RunApp();
