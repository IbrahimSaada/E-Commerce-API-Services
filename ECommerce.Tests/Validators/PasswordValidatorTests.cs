using System;
using Xunit;
using ECommerce.Infrastructure.Security; // adjust if your validator is in a different namespace

namespace ECommerce.Tests.Validators;
public class PasswordValidatorTests
{
    private readonly PasswordValidator _passwordValidator;

    public PasswordValidatorTests()
    {
        _passwordValidator = new PasswordValidator();
    }

    [Theory]
    [InlineData("Abc@1234")]       // valid example
    [InlineData("StrongP@ssw0rd")] // valid example
    public void Validate_ValidPassword_ShouldNotThrowException(string password)
    {
        // Act
        var exception = Record.Exception(() => _passwordValidator.Validate(password));

        // Assert
        Assert.Null(exception);
    }

    [Theory]
    [InlineData("abc")]           // Too short
    [InlineData("abcdefgh")]      // Missing digit, uppercase, special char
    [InlineData("Abc1234")]       // Missing special character
    [InlineData("Abc12345 ")]     // Contains whitespace
    [InlineData("ABC@1234")]      // Missing lowercase
    [InlineData("aaaaaaaa@1")]    // Repetitive characters (if your rules forbid that)
    [InlineData("Abc@12")]        // Too short again
    [InlineData("Abc@1234abc")]   // Might contain sequential characters if your rules forbid
    public void Validate_InvalidPassword_ShouldThrowException(string password)
    {
        // Act & Assert
        Assert.Throws<Exception>(() => _passwordValidator.Validate(password));
    }
}
