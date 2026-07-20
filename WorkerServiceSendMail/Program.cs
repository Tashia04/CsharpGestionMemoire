using WorkerServiceSendMail;
using WorkerServiceSendMail.Models;
using WorkerServiceSendMail.Services;

var builder = Host.CreateApplicationBuilder(args);
builder.Services
    .AddOptions<SmtpOptions>()
    .Bind(builder.Configuration.GetSection(SmtpOptions.SectionName))
    .Validate(options => !string.IsNullOrWhiteSpace(options.Host), "Smtp:Host est obligatoire.")
    .Validate(options => options.Port is > 0 and <= 65535, "Smtp:Port doit être compris entre 1 et 65535.")
    .Validate(options => !string.IsNullOrWhiteSpace(options.From), "Smtp:From est obligatoire.")
    .ValidateOnStart();
builder.Services.AddSingleton<IEmailService, SmtpEmailService>();
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
