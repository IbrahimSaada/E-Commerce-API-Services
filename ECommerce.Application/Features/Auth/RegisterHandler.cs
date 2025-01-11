using ECommerce.Application.Interfaces;
using ECommerce.Application.Interfaces.ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Features.Auth
{
    public class RegisterHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public RegisterHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task Handle(RegisterRequest request)
        {
            // (Optional) Check if email is already taken
            var existingUser = await _userRepository.GetUserByEmailAsync(request.Email);
            var existingUsername = await _userRepository.GetUserByUsernameAsync(request.Username);
            if (existingUser != null)
            {
                // You can throw an exception or use a custom result pattern
                throw new Exception("A user with this email already exists please user another email.");
            }
            if (existingUsername != null)
            {
                throw new Exception("A user with this username already exists please use another username.");
            }

            // 1. Hash the password
            var hashedPassword = _passwordHasher.Hash(request.Password);

            // 2. Create a new User entity
            var newUser = new User
            {
                Email = request.Email,
                Username = request.Username,
                Password = hashedPassword, // store the hashed password
                CreatedAt = DateTime.UtcNow
            };

            // 3. Save to DB
            await _userRepository.AddUserAsync(newUser);
        }
    }
}

