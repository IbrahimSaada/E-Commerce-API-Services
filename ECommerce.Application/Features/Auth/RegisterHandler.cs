using ECommerce.Application.Interfaces;
using ECommerce.Application.Interfaces.ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Features.Auth
{
    public class RegisterHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IEnumerable<IValidator<RegisterRequest>> _validators;

        public RegisterHandler(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IEnumerable<IValidator<RegisterRequest>> validators
        )
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _validators = validators;
        }

        public async Task Handle(RegisterRequest request)
        {
            // Run all validators
            foreach (var validator in _validators)
            {
                validator.Validate(request);
            }

            // Check if the email is already taken
            var existingUser = await _userRepository.GetUserByEmailAsync(request.Email);
            if (existingUser != null)
            {
                throw new Exception("A user with this email already exists, please use another email.");
            }

            // Check if the username is already taken
            var existingUsername = await _userRepository.GetUserByUsernameAsync(request.Username);
            if (existingUsername != null)
            {
                throw new Exception("A user with this username already exists, please use another username.");
            }

            // Hash the password
            var hashedPassword = _passwordHasher.Hash(request.Password);

            // Create a new User entity
            var newUser = new User
            {
                Email = request.Email,
                Username = request.Username,
                Password = hashedPassword,
                FullName = request.FullName,
                DateOfBirth = request.DateOfBirth,
                CreatedAt = DateTime.UtcNow
            };

            // Save the user to the database
            await _userRepository.AddUserAsync(newUser);
        }
    }
}
