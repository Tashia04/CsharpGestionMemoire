namespace WorkerServiceSendMail.Models;

public sealed class EmailMessage
{
    public string Destinataire { get; init; } = string.Empty;
    public string? NomDestinataire { get; init; }
    public string Sujet { get; init; } = string.Empty;
    public string Corps { get; init; } = string.Empty;
    public bool EstHtml { get; init; }
}
