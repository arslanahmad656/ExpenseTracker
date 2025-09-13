
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace ExpenseTracker.App.ServiceInstallers;

public class AuthenticationInstaller : IServiceInstaller
{
    public void Install(WebApplicationBuilder builder)
    {
        var jwtSection = builder.Configuration.GetSection("Jwt");
        var key = Encoding.UTF8.GetBytes(jwtSection["Key"] ?? throw new InvalidOperationException($"Key not found in the app settings."));

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtSection["Issuer"],

                ValidateAudience = true,
                ValidAudience = jwtSection["Audience"],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),

                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromMinutes(1)
            };
        });
    }
}
