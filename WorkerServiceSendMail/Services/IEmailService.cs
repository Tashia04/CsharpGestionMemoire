using WorkerServiceSendMail.Models;

namespace WorkerServiceSendMail.Services;

public interface IEmailService
{
    Task<bool> EnvoyerAsync(EmailMessage email, CancellationToken cancellationToken = default);
}
