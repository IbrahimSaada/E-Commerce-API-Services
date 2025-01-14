using ECommerce.Application.Features.Auth;
using ECommerce.Application.Interfaces;
using System;

namespace ECommerce.Infrastructure.Security
{
    public class DateOfBirthValidator : IValidator<RegisterRequest>
    {
        public void Validate(RegisterRequest request)
        {
            if (request.DateOfBirth == default)
            {
                throw new Exception("Date of Birth is required.");
            }

            if (request.DateOfBirth.Year < 1900 || request.DateOfBirth.Year > DateTime.UtcNow.Year)
            {
                throw new Exception("Invalid Date of Birth format. Please provide a complete date (e.g., 1990-01-01).");
            }

            if (request.DateOfBirth > DateTime.UtcNow)
            {
                throw new Exception("Date of Birth cannot be in the future.");
            }

            var age = DateTime.UtcNow.Year - request.DateOfBirth.Year;
            if (request.DateOfBirth > DateTime.UtcNow.AddYears(-age)) age--;

            if (age < 18)
            {
                throw new Exception("You must be at least 18 years old to register.");
            }
        }
    }
}
