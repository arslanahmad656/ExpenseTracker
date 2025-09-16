using ExpenseTracker.Contracts;

namespace ExpenseTracker.Services;

public class GuidTrackingIdGenerator : ITrackingIdGenerator
{
    public string Generate() => Guid.NewGuid().ToString("N").ToLower();
}
