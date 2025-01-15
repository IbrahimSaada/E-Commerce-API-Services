using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Features.Auth
{
    public class VerifyEmailHandler
    {
        private readonly IUserRepository _userRepository;

        public VerifyEmailHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(string token)
        {
            // Retrieve the user by token
            var user = await _userRepository.GetByVerificationTokenAsync(token);
            if (user == null || user.EmailVerificationTokenExpires < DateTime.UtcNow)
            {
                throw new Exception("Invalid or expired token.");
            }

            // Verify the email
            user.IsEmailVerified = true;
            user.EmailVerificationToken = null;
            user.EmailVerificationTokenExpires = null;

            // Update the user
            await _userRepository.UpdateAsync(user);
        }
    }
}
