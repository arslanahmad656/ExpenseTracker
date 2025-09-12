namespace ExpenseTracker.Repository.Tests;

public class TestEntityRepository(ExpenseTrackerDbContext context) : RepositoryBase<TestEntity>(context)
{
}
