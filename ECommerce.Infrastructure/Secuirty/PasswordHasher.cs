using ECommerce.Application.Interfaces;
using BCrypt.Net;
using ECommerce.Application.Interfaces.ECommerce.Application.Interfaces;

namespace ECommerce.Infrastructure.Security
{
    public class PasswordHasher : IPasswordHasher
    {
        public string Hash(string password)
        {
            // Use bcrypt to hash the password
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool Verify(string rawPassword, string hashedPassword)
        {
            // Verify the raw password against the hashed password
            return BCrypt.Net.BCrypt.Verify(rawPassword, hashedPassword);
        }
    }
}
