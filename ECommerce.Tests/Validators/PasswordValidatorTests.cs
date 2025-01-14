using System;
using Xunit;
using ECommerce.Infrastructure.Security;
using ECommerce.Application.Features.Auth;

namespace ECommerce.Tests.Validators
{
    public class PasswordValidatorTests
    {
        private readonly PasswordValidator _passwordValidator;

        public PasswordValidatorTests()
        {
            _passwordValidator = new PasswordValidator();
        }

        [Theory]
        [InlineData("Abc@1234")]       // Valid example
        [InlineData("StrongP@ssw0rd")] // Valid example
        public void Validate_ValidPassword_ShouldNotThrowException(string password)
        {
            // Arrange
            var request = new RegisterRequest
            {
                Password = password
            };

            // Act
            var exception = Record.Exception(() => _passwordValidator.Validate(request));

            // Assert
            Assert.Null(exception);
        }

        [Theory]
        [InlineData("")] // Empty password
        [InlineData("A1!")] // Short password <8
        [InlineData("Abcdefgklmsd12dsdja223@")] // Long password >20
        [InlineData("Hhoommeeww!")] // Missing a digit
        [InlineData("hhoommee11!!")] // Missing uppercase
        [InlineData("HHOOMMEE11!!")] // Missing lowercase
        [InlineData("HHoommee112")] // Missing special character
        [InlineData("HHoommee11 !!")] // Whitespace
        [InlineData("HHHHoommee11!!")] // Repetitive characters
        public void Validate_InvalidPassword_ShouldThrowException(string password)
        {
            // Arrange
            var request = new RegisterRequest
            {
                Password = password
            };

            // Act & Assert
            Assert.Throws<Exception>(() => _passwordValidator.Validate(request));
        }
    }
}
