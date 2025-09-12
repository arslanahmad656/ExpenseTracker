using ExpenseTracker.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseTracker.Repository.Configuration;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        var (Employees, Accountants, Admins) = Helper.GetPrincipals();

        var employees = Employees.Concat(Accountants).Concat(Admins).Select((u, i) => new Employee
        {
            Id = i + 1,
            Code = u.ToUpper(),
            HireDate = new DateTimeOffset(new DateTime(2025, (i % 12) + 1, 1)),
            Name = u,
            PrincipalId = i + 1,
        }).ToDictionary(e => e.Id);

        var same = employees.Values.Where(e => e.Id != e.PrincipalId);

        employees[4].ManagerId = 1;
        employees[5].ManagerId = 1;
        employees[6].ManagerId = 2;
        employees[7].ManagerId = 2;
        employees[8].ManagerId = 3;
        employees[9].ManagerId = 3;

        var employeesToAdd = employees.Values.ToList();
        builder.HasData(employeesToAdd);
    }
}
