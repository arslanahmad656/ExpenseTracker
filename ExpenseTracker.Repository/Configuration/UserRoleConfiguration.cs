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

        List<UserRole> userRoles = [];

        foreach (var u in Employees)
        {
            var role = new UserRole
            {
                Id = nextId,
                PrincipalId = nextId++,
                RoleId = 1
            };

            userRoles.Add(role);
        }

        foreach (var u in Accountants)
        {
            var role = new UserRole
            {
                Id = nextId,
                PrincipalId = nextId++,
                RoleId = 3
            };

            userRoles.Add(role);
        }

        foreach (var u in Admins)
        {
            var role = new UserRole
            {
                Id = nextId,
                PrincipalId = nextId++,
                RoleId = 4
            };

            userRoles.Add(role);
        }

        foreach (var u in Employees.Take(3))
        {
            var role = new UserRole
            {
                Id = nextId++,
                PrincipalId = Helper.GetIdFromUsername(u),
                RoleId = 2
            };

            userRoles.Add(role);
        }

        //var userRoles = Employees.Select(u => new UserRole
        //{
        //    Id = nextId,
        //    PrincipalId = nextId++,
        //    RoleId = 1
        //});

        //userRoles = userRoles.Concat(Accountants.Select(u => new UserRole
        //{
        //    Id = nextId,
        //    PrincipalId = nextId++,
        //    RoleId = 3
        //}));

        //userRoles = userRoles.Concat(Admins.Select(u => new UserRole
        //{
        //    Id = nextId,
        //    PrincipalId = nextId++,
        //    RoleId = 4
        //}));

        //userRoles = userRoles.Concat(Employees.Take(3).Select(u => new UserRole
        //{
        //    Id = nextId++,
        //    PrincipalId = Helper.GetIdFromUsername(u),
        //    RoleId = 2
        //}));

        builder.HasData(userRoles);
    }
}
