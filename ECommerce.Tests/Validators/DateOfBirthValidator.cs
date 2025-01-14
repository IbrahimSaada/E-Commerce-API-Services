using System;
using Xunit;
using ECommerce.Infrastructure.Security; // Adjust if your validator is in a different namespace
using ECommerce.Application.Features.Auth;

namespace ECommerce.Tests.Validators
{
    public class DateOfBirthValidatorTests
    {
        private readonly DateOfBirthValidator _dateOfBirthValidator;

        public DateOfBirthValidatorTests()
        {
            _dateOfBirthValidator = new DateOfBirthValidator();
        }

        [Theory]
        [InlineData("2000-01-01")] // Valid date of birth
        [InlineData("1990-05-15")] // Valid date of birth
        [InlineData("1985-12-25")] // Valid date of birth
        public void Validate_ValidDateOfBirth_ShouldNotThrowException(string dateOfBirth)
        {
            // Arrange
            var request = new RegisterRequest
            {
                DateOfBirth = DateTime.Parse(dateOfBirth)
            };

            // Act
            var exception = Record.Exception(() => _dateOfBirthValidator.Validate(request));

            // Assert
            Assert.Null(exception); // No exception means validation passed
        }

        [Theory]
        [InlineData("2025-01-01")] // Future date
        [InlineData("")]           // Empty date
        [InlineData("2010-01-01")] // User is under 18 years old
        [InlineData("2010")]
        public void Validate_InvalidDateOfBirth_ShouldThrowException(string dateOfBirth)
        {
            // Arrange
            var request = new RegisterRequest
            {
                DateOfBirth = string.IsNullOrEmpty(dateOfBirth) ? default : DateTime.Parse(dateOfBirth)
            };

            // Act & Assert
            Assert.Throws<Exception>(() => _dateOfBirthValidator.Validate(request));
        }
    }
}
