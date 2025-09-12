using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using ExpenseTracker.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseTracker.Repository.Configuration;

public class PrincipalConfiguration : IEntityTypeConfiguration<Principal>
{
    private readonly string defaultPasswordHash = "$2a$11$BcmWuSKqCzwxqaID7uyke.zEsmY5EPaQbXRAEaIzbhUG6nkIDbt4e"; // password: secret
    public void Configure(EntityTypeBuilder<Principal> builder)
    {
        var (Employees, Accountants, Admins) = Helper.GetPrincipals();

        var principals = Employees.Concat(Accountants).Concat(Admins)
            .Select((u, i) => new Principal
            {
                Id = i + 1,
                IsActive = i % 2 == 0,
                PasswordHash = defaultPasswordHash,
                Username = u
            });

        builder.HasData(principals);
    }
}
