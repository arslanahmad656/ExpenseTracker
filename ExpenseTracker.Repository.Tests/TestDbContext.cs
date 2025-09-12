using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Repository.Tests;

public class TestDbContext(DbContextOptions<ExpenseTrackerDbContext> options) : ExpenseTrackerDbContext(options)
{
    public DbSet<TestEntity> TestEntities => Set<TestEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // registering the test entity because it's not in the main DbContext
        modelBuilder.Entity<TestEntity>();
        modelBuilder.Entity<ParentEntity>();
    }
}
