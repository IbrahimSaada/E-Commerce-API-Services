namespace ECommerce.Application.Interfaces
{
    public interface IEmailService
    {
        /// <summary>
        /// Sends an email to the specified recipient.
        /// </summary>
        /// <param name="to">The recipient's email address.</param>
        /// <param name="subject">The subject of the email.</param>
        /// <param name="body">The body content of the email.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task SendAsync(string to, string subject, string body);
    }
}
