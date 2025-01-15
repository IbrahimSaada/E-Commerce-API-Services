using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Features.Auth
{
    public class SendVerificationEmailHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;

        public SendVerificationEmailHandler(IUserRepository userRepository, IEmailService emailService)
        {
            _userRepository = userRepository;
            _emailService = emailService;
        }

        public async Task Handle(long userId)
        {
            // Retrieve the user
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            // Generate a verification token
            var token = Guid.NewGuid().ToString();
            user.EmailVerificationToken = token;
            user.EmailVerificationTokenExpires = DateTime.UtcNow.AddHours(24);

            // Save the user with the new token
            await _userRepository.UpdateAsync(user);

            // Send the email
            var verificationLink = $"https://localhost:7001/api/auth/verify-email?token={token}";

            await _emailService.SendAsync(
                user.Email,
                "Verify Your Email",
                $"Please verify your email by clicking the following link: {verificationLink}"
            );
        }
    }
}
