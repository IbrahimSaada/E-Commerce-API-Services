using ECommerce.Application.Interfaces;
using ECommerce.Application.Interfaces.ECommerce.Application.Interfaces;

namespace ECommerce.Application.Features.Auth
{
    public class LoginHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService _tokenService;

        public LoginHandler(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            ITokenService tokenService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
        }

        public async Task<LoginResponse> Handle(LoginRequest request)
        {
            // 1. Get user by email
            var user = await _userRepository.GetUserByEmailAsync(request.Email);
            if (user == null)
                throw new Exception("User not found");

            // 2. Verify password
            bool isValid = _passwordHasher.Verify(request.Password, user.Password);
            if (!isValid)
                throw new Exception("Invalid credentials");

            // 3. Generate token
            var token = _tokenService.GenerateToken(user);

            // 4. Return response
            return new LoginResponse
            {
                Token = token,
                Username = user.Username
            };
        }
    }
}
