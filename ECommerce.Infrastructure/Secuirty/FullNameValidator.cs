using ECommerce.Application.Features.Auth;
using ECommerce.Application.Interfaces;
using System.Text.RegularExpressions;

public class FullNameValidator : IValidator<RegisterRequest>
{
    public void Validate(RegisterRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.FullName))
        {
            throw new Exception("Full Name is required.");
        }

        if (request.FullName.Length < 2 || request.FullName.Length > 100)
        {
            throw new Exception("Full Name must be between 2 and 100 characters.");
        }

        if (!Regex.IsMatch(request.FullName, @"^[a-zA-Z\s]+$"))
        {
            throw new Exception("Full Name can only contain alphabetic characters and spaces.");
        }
    }
}
