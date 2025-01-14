using System;
using Xunit;
using ECommerce.Infrastructure.Security;
using ECommerce.Application.Features.Auth;

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

        var request = new RegisterRequest
        { 
            Email = email,
        };
        // Act
        var exception = Record.Exception(() => _emailValidator.Validate(request));

        // Assert
        Assert.Null(exception); // No exception thrown, so valid
    }

    [Theory]
    [InlineData("testexample.com")] // Invalid, missing '@'
    [InlineData("test@")] // Invalid, missing domain
    [InlineData("@example.com")] // Invalid, missing username
    [InlineData("test@.com")] // Invalid, domain missing name
    [InlineData("test@com")] // Invalid, missing top-level domain
    [InlineData("")] // Invalid, empty string
    [InlineData("a@b")] // Invalid: Less than 5 characters
    [InlineData("")] // Invalid: Empty string
    public void Validate_InvalidEmail_ShouldThrowException(string email)
    {
        var request = new RegisterRequest
        {
            Email = email
        };
        // Act & Assert
        Assert.Throws<Exception>(() => _emailValidator.Validate(request));
    }
}
