﻿using System;
using Xunit;
using ECommerce.Infrastructure.Security;
using ECommerce.Application.Features.Auth;

namespace ECommerce.Tests.Validators
{
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
        [InlineData("123_B")]
        [InlineData("q12")]
        public void Validate_ValidUsername_ShouldNotThrowException(string username)
        {
            // Arrange
            var request = new RegisterRequest
            {
                Username = username
            };

            // Act
            var exception = Record.Exception(() => _usernameValidator.Validate(request));

            // Assert
            Assert.Null(exception); // means no exception was thrown
        }

        [Theory]
        [InlineData("12345")]          // Only digits
        [InlineData("ab")]             // Too short
        [InlineData("toolongtoolongtoolong")] // Too long
        [InlineData("user__name")]     // Consecutive underscores
        [InlineData("user name")]      // Space in name
        public void Validate_InvalidUsername_ShouldThrowException(string username)
        {
            // Arrange
            var request = new RegisterRequest
            {
                Username = username
            };

            // Act & Assert
            Assert.Throws<Exception>(() => _usernameValidator.Validate(request));
        }
    }
}
