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
            new() { Id = 1, Name = "Employee", Priority = 3 },
            new() { Id = 2, Name = "Manager", Priority = 2 },
            new() { Id = 3, Name = "Accountant", Priority = 4 },
            new() { Id = 4, Name = "Administrator", Priority = 1 },
        ];


        builder.HasData(roles);
    }
}
