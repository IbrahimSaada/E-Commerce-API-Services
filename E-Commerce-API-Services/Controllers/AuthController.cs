using ECommerce.Application.Features.Auth;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly LoginHandler _loginHandler;
        private readonly RegisterHandler _registerHandler;
        private readonly VerifyEmailHandler _verifyEmailHandler;
        private readonly SendVerificationEmailHandler _sendVerificationEmailHandler;

        public AuthController(LoginHandler loginHandler, RegisterHandler registerHandler, VerifyEmailHandler verifyEmailHandler, SendVerificationEmailHandler sendVerificationEmailHandler)
        {
            _loginHandler = loginHandler;
            _registerHandler = registerHandler;
            _verifyEmailHandler = verifyEmailHandler;
            _sendVerificationEmailHandler = sendVerificationEmailHandler;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var response = await _loginHandler.Handle(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                await _registerHandler.Handle(request);
                return Ok(new { message = "Registration successful." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("send-verification-email/{userId}")]
        public async Task<IActionResult> SendVerificationEmail(long userId)
        {
            try
            {
                await _sendVerificationEmailHandler.Handle(userId);
                return Ok(new { message = "Verification email sent successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("verify-email")]
        public async Task<IActionResult> VerifyEmail([FromQuery] string token)
        {
            try
            {
                await _verifyEmailHandler.Handle(token);
                return Ok(new { message = "Email verified successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }

}
