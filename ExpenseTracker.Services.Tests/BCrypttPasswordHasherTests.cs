using ExpenseTracker.TestsBase;
using FluentAssertions;

namespace ExpenseTracker.Services.Tests;

public class BCrypttPasswordHasherTests : TestBase
{
    private readonly BCrypttPasswordHasher _passwordHasher = new();

    [Theory]
    [InlineData("TestPassword123!")]
    [InlineData("p")]
    [InlineData("")]
    public void Hash_WithValidString_ReturnsString(string password)
    {
        var hashedPassword = _passwordHasher.Hash(password);

        hashedPassword.Should().NotBeNullOrEmpty();
    }

    [Theory]
    [InlineData("TestPassword123!")]
    [InlineData("p")]
    [InlineData("")]
    public void Verify_WithCorrectPassword_ReturnsTrue(string password)
    {
        var hashedPassword = _passwordHasher.Hash(password);
        var result = _passwordHasher.Verify(password, hashedPassword);
        result.Should().BeTrue();
    }

    [Theory]
    [InlineData("TestPassword123!", "WrongPassword")]
    public void Verify_WithIncorrectPassword_ReturnsFalse(string password, string wrongPassword)
    {
        var hashedPassword = _passwordHasher.Hash(password);
        var result = _passwordHasher.Verify(wrongPassword, hashedPassword);
        result.Should().BeFalse();
    }
}
