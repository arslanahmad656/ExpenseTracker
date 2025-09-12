using AutoFixture;

namespace ExpenseTracker.TestsBase;

/// <summary>
/// A class intended to be the base of all of the test classes throughout the solution.
/// </summary>
public abstract class TestBase
{
    protected static Fixture GetFixture() => new();
}
