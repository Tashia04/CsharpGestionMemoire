using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using WorkerServiceSendMail.Models;

namespace WorkerServiceSendMail.Services;

public sealed class SmtpEmailService(
    IOptions<SmtpOptions> smtpOptions,
    ILogger<SmtpEmailService> logger) : IEmailService
{
    private readonly SmtpOptions _smtpOptions = smtpOptions.Value;
    private readonly ILogger<SmtpEmailService> _logger = logger;

    public async Task<bool> EnvoyerAsync(EmailMessage email, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(email);

        if (string.IsNullOrWhiteSpace(email.Destinataire))
        {
            throw new ArgumentException("Au moins un destinataire est requis.", nameof(email));
        }

        using var message = new MailMessage
        {
            From = new MailAddress(_smtpOptions.From, _smtpOptions.DisplayName),
            Subject = email.Sujet,
            Body = email.Corps,
            IsBodyHtml = email.EstHtml
        };

        message.To.Add(new MailAddress(email.Destinataire, email.NomDestinataire));

        using var client = new SmtpClient(_smtpOptions.Host, _smtpOptions.Port)
        {
            EnableSsl = _smtpOptions.EnableSsl,
            UseDefaultCredentials = false
        };

        if (!string.IsNullOrWhiteSpace(_smtpOptions.UserName))
        {
            client.Credentials = new NetworkCredential(_smtpOptions.UserName, _smtpOptions.Password);
        }

        try
        {
            await client.SendMailAsync(message, cancellationToken);
            _logger.LogInformation("E-mail envoyé à {Destinataire}.", email.Destinataire);
            return true;
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Échec de l'envoi de l'e-mail à {Destinataire}.", email.Destinataire);
            return false;
        }
    }
}
