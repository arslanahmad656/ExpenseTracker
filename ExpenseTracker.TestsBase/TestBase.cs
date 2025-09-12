using AutoFixture;

namespace ExpenseTracker.TestsBase;

public abstract class TestBase
{
    protected Fixture GetFixture() => new();
}
