using ECommerce.Application.Features.Auth;
using ECommerce.Application.Interfaces;
using System;
using System.Globalization;

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

            // Validate that the provided date is in a complete format (Year, Month, Day)
            if (request.DateOfBirth.Year < 1900 || request.DateOfBirth.Year > DateTime.UtcNow.Year)
            {
                throw new Exception("Invalid Date of Birth format. Please provide a complete date (e.g., 1990-01-01).");
            }

            if (request.DateOfBirth.Month < 1 || request.DateOfBirth.Month > 12 || request.DateOfBirth.Day < 1 || request.DateOfBirth.Day > 31)
            {
                throw new Exception("Invalid Date of Birth format. Please provide a valid and complete date (e.g., 1990-01-01).");
            }

            if (request.DateOfBirth > DateTime.UtcNow)
            {
                throw new Exception("Date of Birth cannot be in the future.");
            }

            var today = DateTime.UtcNow;
            var age = today.Year - request.DateOfBirth.Year;

            // Adjust for the actual birthday in the current year
            if (request.DateOfBirth.Date > today.AddYears(-age).Date)
            {
                age--;
            }

            if (age < 18)
            {
                throw new Exception("You must be at least 18 years old to register.");
            }
        }
    }
}
