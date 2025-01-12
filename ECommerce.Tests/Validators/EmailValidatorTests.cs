using System;
using Xunit;
using ECommerce.Infrastructure.Security; // adjust if your validator is in a different namespace

namespace ECommerce.Tests.Validators;
public class EmailValidatorTests
{
    private readonly EmailValidator _emailValidator;

    public EmailValidatorTests()
    {
        _emailValidator = new EmailValidator();
    }

    [Theory]
    [InlineData("john.doe@example.com")]
    [InlineData("user123@example.co.uk")]
    [InlineData("my.email@domain.org")]
    public void Validate_ValidEmail_ShouldNotThrowException(string email)
    {
        // Act
        var exception = Record.Exception(() => _emailValidator.Validate(email));

        // Assert
        Assert.Null(exception); // No exception thrown, so valid
    }

    [Theory]
    [InlineData("invalidemail")]
    [InlineData("user@.com")]
    [InlineData("user..name@example.com")]
    [InlineData(".user@example.com")]
    [InlineData("user@domain")]      // missing TLD
    [InlineData("user@domain..com")] // consecutive dots
    public void Validate_InvalidEmail_ShouldThrowException(string email)
    {
        // Act & Assert
        Assert.Throws<Exception>(() => _emailValidator.Validate(email));
    }
}
