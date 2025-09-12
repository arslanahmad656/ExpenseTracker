using ExpenseTracker.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Repository;

public class ExpenseTrackerDbContext(DbContextOptions<ExpenseTrackerDbContext> options) : DbContext(options)
{
    public DbSet<Principal> Principals { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<LoginHistory> LoginHistories { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Currency> Currencies { get; set; }
    public DbSet<Form> Forms { get; set; }
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<ExpenseState> ExpenseStates { get; set; }
    public DbSet<ExpenseHistory> ExpenseHistories { get; set; }
    public DbSet<FormState> FormStates { get; set; }
    public DbSet<FormHistory> FormHistories { get; set; }
}
