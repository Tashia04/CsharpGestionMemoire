using WorkerServiceSendMail.Models;
using WorkerServiceSendMail.Services;

namespace WorkerServiceSendMail
{
    public class Worker: BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _config;
        public Worker(ILogger<Worker> logger,
        IEmailService emailService, IConfiguration config)
        {
            _logger = logger;
            _emailService = emailService;
            _config = config;
        }
        protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
        {
            _logger.LogInformation(
            "Service Email demarre a {T}", DateTime.Now);
            while (!stoppingToken.IsCancellationRequested)
            {
                try { await TraiterCycleAsync(stoppingToken); }
                catch (Exception ex)
                { _logger.LogError(ex, "Erreur cycle principal"); }
                var min = _config.GetValue<int>(
                "ServiceSettings:IntervalleMinutes");
                await Task.Delay(
                TimeSpan.FromMinutes(min), stoppingToken);
            }
        }

        private async Task TraiterCycleAsync(CancellationToken ct)
        {
            // Email texte simple
            var emailTexte = new EmailMessage
            {
                Destinataire = _config["ServiceSettings:DestinataireTest"]!,
                NomDestinataire = "Equipe",
                Sujet = $"Rapport — {DateTime.Now:dd/MM/yyyy HH:mm}",
                Corps = $"Rapport genere le {DateTime.Now}.\n\nService : OK"
            };
            await EnvoyerAvecRetryAsync(emailTexte, ct);
            // Email HTML
            var emailHtml = new EmailMessage
            {
                Destinataire = _config["ServiceSettings:DestinataireTest"]!,
                Sujet = "Alerte systeme",
                Corps = $"""
            <html><body>
            <h2 style='color:#1F4E79;'>Rapport {DateTime.Now:dd/MM}</h2>
            <p>Statut : <strong style='color:green;'>OK</strong></p>
            </body></html>
            """,
                EstHtml = true
            };
            await EnvoyerAvecRetryAsync(emailHtml, ct);
        }

        // Retry avec delai exponentiel : 5s, 15s, 30s
        private async Task<bool> EnvoyerAvecRetryAsync(
        EmailMessage msg, CancellationToken ct)
        {
            int[] delays = { 5, 15, 30 };
            for (int i = 0; i < delays.Length; i++)
            {
                if (await _emailService.EnvoyerAsync(msg, ct)) return true;
                if (i < delays.Length - 1)
                {
                    _logger.LogWarning(
                    "Retry dans {s}s...", delays[i]);
                    await Task.Delay(delays[i] * 1000, ct);
                }
            }
            _logger.LogError("Echec apres 3 tentatives : {Dest}",
            msg.Destinataire);
            return false;
        }
        public override async Task StopAsync(
        CancellationToken cancellationToken)
        {
            _logger.LogInformation(
            "Service Email arrete a {T}", DateTime.Now);
            await base.StopAsync(cancellationToken);
        }








    }

}
