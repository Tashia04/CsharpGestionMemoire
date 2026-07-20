using Serilog;
using System.IO;
using System.Windows;
using System.Windows.Threading;

namespace GestionUtilisateur
{
    /// <summary>
    /// Point d'entree de l'application WPF.
    /// Equivalent du fichier Program.cs du projet UtilisateurPhp :
    /// configuration de Serilog et capture des exceptions non gerees.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Initialise la journalisation avant l'affichage de la fenetre principale.
        /// </summary>
        protected override void OnStartup(StartupEventArgs e)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File(
                    path: Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs", "app-.log"),
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 14,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();

            Log.Information("=== Demarrage de l application ===");

            // Capture des exceptions non gerees du thread UI
            DispatcherUnhandledException += (s, args) =>
            {
                Log.Fatal(args.Exception, "Exception non geree sur le thread UI");

                MessageBox.Show("Erreur : " + args.Exception.Message,
                    "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);

                args.Handled = true;
            };

            base.OnStartup(e);
        }

        /// <summary>
        /// Journalise la fermeture et vide les tampons de Serilog.
        /// </summary>
        protected override void OnExit(ExitEventArgs e)
        {
            Log.Information("=== Fermeture de l application ===");
            Log.CloseAndFlush();

            base.OnExit(e);
        }
    }
}
