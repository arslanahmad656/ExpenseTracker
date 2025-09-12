using ExpenseTracker.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseTracker.Repository.Configuration;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        var (Employees, Accountants, Admins) = Helper.GetPrincipals();

        var nextId = 1;

        var userRoles = Employees.Select(u => new UserRole
        {
            Id = nextId,
            PrincipalId = nextId++,
            RoleId = 1
        });

        userRoles = userRoles.Concat(Accountants.Select(u => new UserRole
        {
            Id = nextId,
            PrincipalId = nextId++,
            RoleId = 3
        }));

        userRoles = userRoles.Concat(Admins.Select(u => new UserRole
        {
            Id = nextId,
            PrincipalId = nextId++,
            RoleId = 4
        }));

        userRoles = userRoles.Concat(Employees.Take(3).Select(u => new UserRole
        {
            Id = nextId++,
            PrincipalId = Helper.GetIdFromUsername(u),
            RoleId = 2
        }));

        builder.HasData(userRoles);
    }
}
