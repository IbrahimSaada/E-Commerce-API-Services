using System.Text.RegularExpressions;
using ECommerce.Application.Interfaces;

namespace ECommerce.Infrastructure.Security
{
    public class PasswordValidator : IPasswordValidator
    {
        public void Validate(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new Exception("Password cannot be empty.");
            }

            // Minimum length of 8 characters
            if (password.Length < 8 || password.Length >= 20)
            {
                throw new Exception("Password must be at least 8 characters long or not greater than 20 characters.");
            }

            // At least one digit
            if (!password.Any(char.IsDigit))
            {
                throw new Exception("Password must contain at least one digit.");
            }

            // At least one uppercase letter
            if (!password.Any(char.IsUpper))
            {
                throw new Exception("Password must contain at least one uppercase letter.");
            }

            // At least one lowercase letter
            if (!password.Any(char.IsLower))
            {
                throw new Exception("Password must contain at least one lowercase letter.");
            }

            // At least one special character (e.g., @, #, $, etc.)
            if (!Regex.IsMatch(password, @"[!@#$%^&*(),.?""':{}|<>]"))
            {
                throw new Exception("Password must contain at least one special character (e.g., @, #, $, etc.).");
            }

            // No whitespace characters allowed
            if (password.Any(char.IsWhiteSpace))
            {
                throw new Exception("Password cannot contain whitespace characters.");
            }

            // No repetitive characters (e.g., 'aaaa')
            if (HasRepetitiveCharacters(password))
            {
                throw new Exception("Password cannot contain repetitive characters (e.g., 'aaaa').");
            }
        }

        // Helper method to check for repetitive characters
        private bool HasRepetitiveCharacters(string password)
        {
            for (int i = 0; i < password.Length - 3; i++)
            {
                if (password[i] == password[i + 1] && password[i + 1] == password[i + 2] && password[i + 2] == password[i + 3])
                {
                    return true;
                }
            }
            return false;
        }
    }
}
