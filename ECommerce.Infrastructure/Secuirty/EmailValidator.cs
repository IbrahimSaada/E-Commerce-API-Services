using System.Text.RegularExpressions;
using ECommerce.Application.Features.Auth;
using ECommerce.Application.Interfaces;

namespace ECommerce.Infrastructure.Security
{
    public class EmailValidator : IValidator<RegisterRequest>
    {
        public void Validate(RegisterRequest request)
        {
            var email = request.Email;

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new Exception("Email cannot be empty or whitespace only.");
            }

            // Validate the email format using a regular expression
            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                throw new Exception("Invalid email format.");
            }

            // Check for minimum and maximum length (e.g., 5-254 characters)
            if (email.Length < 5 || email.Length > 254)
            {
                throw new Exception("Email must be between 5 and 254 characters.");
            }

            // Ensure the email does not contain consecutive dots (e.g., "john..doe@example.com")
            if (email.Contains(".."))
            {
                throw new Exception("Email cannot contain consecutive dots.");
            }

            // Ensure the email does not start or end with a special character
            if (email.StartsWith(".") || email.EndsWith(".") ||
                email.StartsWith("-") || email.EndsWith("-") ||
                email.StartsWith("_") || email.EndsWith("_"))
            {
                throw new Exception("Email cannot start or end with a special character (., -, _).");
            }
        }
    }
}
