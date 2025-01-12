using System;
using Xunit;
using ECommerce.Infrastructure.Security; // adjust if your validator is in a different namespace

namespace ECommerce.Tests.Validators;
public class UsernameValidatorTests
{
    private readonly UsernameValidator _usernameValidator;

    public UsernameValidatorTests()
    {
        _usernameValidator = new UsernameValidator();
    }

    [Theory]
    [InlineData("valid_user")]
    [InlineData("User123")]
    [InlineData("John_Doe")]
    public void Validate_ValidUsername_ShouldNotThrowException(string username)
    {
        // Act
        var exception = Record.Exception(() => _usernameValidator.Validate(username));

        // Assert
        Assert.Null(exception); // means no exception was thrown
    }

    [Theory]
    [InlineData("12345")]          // Only digits
    [InlineData("ab")]             // Too short
    [InlineData("toolongtoolongtoolong")] // Too long
    [InlineData("user__name")]     // Consecutive underscores
    [InlineData("user name")]      // Space in name
    [InlineData("_username")]      // Starts with underscore (if your rules forbid that)
    public void Validate_InvalidUsername_ShouldThrowException(string username)
    {
        // Act & Assert
        Assert.Throws<Exception>(() => _usernameValidator.Validate(username));
    }
}
