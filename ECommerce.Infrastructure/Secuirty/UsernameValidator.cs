using System.Text.RegularExpressions;
using ECommerce.Application.Features.Auth;
using ECommerce.Application.Interfaces;

namespace ECommerce.Infrastructure.Security
{
    public class UsernameValidator : IValidator<RegisterRequest>
    {
        public void Validate(RegisterRequest request)
        {
            var username = request.Username;

            if (string.IsNullOrWhiteSpace(username))
            {
                throw new Exception("Username cannot be empty or whitespace only.");
            }

            // Username length between 3 and 20 characters
            if (username.Length < 3 || username.Length > 20)
            {
                throw new Exception("Username must be between 3 and 20 characters.");
            }

            if (!Regex.IsMatch(username, "^[a-zA-Z0-9_]+$"))
            {
                throw new Exception("Username can only contain letters, digits, or underscores.");
            }

            if (Regex.IsMatch(username, "^[0-9]+$"))
            {
                throw new Exception("Username cannot consist of only numbers.");
            }

            if (!Regex.IsMatch(username, "[a-zA-Z]"))
            {
                throw new Exception("Username must contain at least one alphabetical character (a-z or A-Z).");
            }

            if (Regex.IsMatch(username, "__"))
             {
                 throw new Exception("Username cannot contain consecutive underscores.");
             }
        }
    }
}
