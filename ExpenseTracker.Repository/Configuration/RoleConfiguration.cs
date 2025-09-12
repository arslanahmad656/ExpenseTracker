using ExpenseTracker.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseTracker.Repository.Configuration;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        Role[] roles =
        [
            new() { Id = 1, Name = "Employee" },
            new() { Id = 2, Name = "Manager" },
            new() { Id = 3, Name = "Accountant" },
            new() { Id = 4, Name = "Administrator" },
        ];


        builder.HasData(roles);
    }
}
