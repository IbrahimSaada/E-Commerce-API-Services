using ECommerce.Application.Interfaces;
using ECommerce.Application.Interfaces.ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Features.Auth
{
    public class RegisterHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IPasswordValidator _passwordValidator;
        private readonly IUsernameValidator _usernameValidator; 
        private readonly IEmailValidator _emailValidator;

        public RegisterHandler(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IPasswordValidator passwordValidator,
            IUsernameValidator usernameValidator,
            IEmailValidator emailValidator
        )
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _passwordValidator = passwordValidator;
            _usernameValidator = usernameValidator;
            _emailValidator = emailValidator;
        }

        public async Task Handle(RegisterRequest request)
        {
            // 1. Validate the username syntax before hitting the database
            _usernameValidator.Validate(request.Username);

            // 2. Check if email or username already exists
            var existingUser = await _userRepository.GetUserByEmailAsync(request.Email);
            var existingUsername = await _userRepository.GetUserByUsernameAsync(request.Username);

            if (existingUser != null)
            {
                throw new Exception("A user with this email already exists, please use another email.");
            }
            if (existingUsername != null)
            {
                throw new Exception("A user with this username already exists, please use another username.");
            }

            //3. Validate email foramt
            _emailValidator.Validate(request.Email);

            // 3. Validate password complexity
            _passwordValidator.Validate(request.Password);

            // 4. Hash the password
            var hashedPassword = _passwordHasher.Hash(request.Password);

            // 5. Create a new User entity
            var newUser = new User
            {
                Email = request.Email,
                Username = request.Username,
                Password = hashedPassword,
                CreatedAt = DateTime.UtcNow
            };

            // 6. Save to DB
            await _userRepository.AddUserAsync(newUser);
        }
    }
}
