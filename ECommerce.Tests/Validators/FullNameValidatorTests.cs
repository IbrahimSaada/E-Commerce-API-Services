using System;
using Xunit;
using ECommerce.Infrastructure.Security; // Adjust if your validator is in a different namespace
using ECommerce.Application.Features.Auth;

namespace ECommerce.Tests.Validators
{
    public class FullNameValidatorTests
    {
        private readonly FullNameValidator _fullNameValidator;

        public FullNameValidatorTests()
        {
            _fullNameValidator = new FullNameValidator();
        }

        [Theory]
        [InlineData("John Doe")]
        [InlineData("Alice Smith")]
        [InlineData("Robert Downey Jr")]
        [InlineData("A B")]
        public void Validate_ValidFullName_ShouldNotThrowException(string fullName)
        {
            // Arrange
            var request = new RegisterRequest
            {
                FullName = fullName
            };

            // Act
            var exception = Record.Exception(() => _fullNameValidator.Validate(request));

            // Assert
            Assert.Null(exception); // No exception means validation passed
        }

        [Theory]
        [InlineData("")]                // Empty name
        [InlineData("A")]               // Too short
        [InlineData("This name is way too long and exceeds the allowed character limit")] // Too long
        [InlineData("1234")]            // Numbers in name
        [InlineData("John@Doe")]        // Special characters in name
        public void Validate_InvalidFullName_ShouldThrowException(string fullName)
        {
            // Arrange
            var request = new RegisterRequest
            {
                FullName = fullName
            };

            // Act & Assert
            Assert.Throws<Exception>(() => _fullNameValidator.Validate(request));
        }
    }
}
