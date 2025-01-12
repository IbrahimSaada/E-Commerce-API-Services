using System;
using Xunit;
using ECommerce.Infrastructure.Security;
using System.Runtime.InteropServices; // adjust if your validator is in a different namespace

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
    [InlineData("")] //empty password
    [InlineData("A1!")] // short password <3
    [InlineData("Abcdefgklmsd12dsdja223@")] //long password >20
    [InlineData("Hhoommeeww!")] // missing a digit
    [InlineData("hhoommee11!!")] // missing uppercase
    [InlineData("HHOOMMEE11!!")] // missing lowercase
    [InlineData("HHoommee112")] // missing specail charachter
    [InlineData("HHoommee11 !!")] // whitespace
    [InlineData("HHHHoommee11!!")] // repetitive characters

    public void Validate_InvalidPassword_ShouldThrowException(string password)
    {
        // Act & Assert
        Assert.Throws<Exception>(() => _passwordValidator.Validate(password));
    }
}
