using GestionUtilisateur.Models;
using GestionUtilisateur.Services;
using Serilog;
using System.Windows;
using System.Windows.Controls;

namespace GestionUtilisateur
{
    /// <summary>
    /// Fenetre principale de l'application.
    /// Reprend la logique du formulaire Form1 du projet UtilisateurPhp, adaptee a WPF.
    /// </summary>
    public partial class MainWindow : Window
    {
        // Service charge de communiquer avec l'API REST.
        // Toutes les operations CRUD (Ajouter, Modifier, Supprimer, Lister)
        // passent par cet objet.
        private readonly ApiService _api = new ApiService();

        // Empeche le remplissage automatique des champs pendant le rechargement de la liste.
        private bool _chargementUtilisateurs;

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Charge la liste des utilisateurs a l'ouverture de la fenetre.
        /// Equivalent de Form1_Load en WinForms.
        /// </summary>
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await ChargerUtilisateurs();
        }

        /// <summary>
        /// Cette methode est executee chaque fois que la selection change
        /// dans le DataGrid afin de mettre a jour les champs du formulaire.
        /// </summary>
        /// <param name="sender">Controle ayant declenche l'evenement.</param>
        /// <param name="e">Informations sur l'evenement.</param>
        private void dgUtilisateur_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RemplirChampsDepuisSelection();
        }

        /// <summary>
        /// Cette methode permet d'ajouter un nouvel utilisateur.
        /// Elle valide les informations saisies, cree un objet Utilisateur,
        /// l'envoie a l'API puis recharge la liste des utilisateurs.
        /// </summary>
        /// <param name="sender">Bouton Ajouter.</param>
        /// <param name="e">Informations sur le clic.</param>
        private async void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            Log.Information("Action: Ajouter utilisateur - Nom={Nom}", txtNom.Text);

            // Verifie que toutes les donnees sont valides.
            if (!ValiderChamps()) return;

            // Creation d'un objet Utilisateur a partir des valeurs saisies.
            var utilisateur = new Utilisateur
            {
                Nom = txtNom.Text.Trim(),
                Prenom = txtPrenom.Text.Trim(),
                Age = int.Parse(txtAge.Text.Trim())
            };

            try
            {
                // Envoi de l'utilisateur a l'API.
                bool succes = await _api.CreateAsync(utilisateur);

                if (succes)
                {
                    Log.Information("Utilisateur ajoute: {@Utilisateur}", utilisateur);

                    MessageBox.Show("Utilisateur ajoute !", "Succes",
                        MessageBoxButton.OK, MessageBoxImage.Information);

                    // Recharge la liste des utilisateurs.
                    await ChargerUtilisateurs();

                    // Nettoie les champs de saisie.
                    Vider();
                }
                else
                {
                    MessageBox.Show("Ajout impossible.", "Erreur",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Echec de l ajout de l utilisateur");

                MessageBox.Show("Erreur : " + ex.Message,
                    "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Cette methode permet de modifier l'utilisateur actuellement selectionne.
        /// Les nouvelles informations sont envoyees a l'API puis la liste est actualisee.
        /// </summary>
        /// <param name="sender">Bouton Modifier.</param>
        /// <param name="e">Informations sur le clic.</param>
        private async void btnModifier_Click(object sender, RoutedEventArgs e)
        {
            // Recupere l'utilisateur selectionne.
            var utilisateurSelectionne = GetUtilisateurSelectionne();

            if (utilisateurSelectionne == null)
            {
                MessageBox.Show("Selectionnez un utilisateur a modifier.",
                    "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // Verifie que l'utilisateur possede un identifiant valide.
            if (utilisateurSelectionne.Id <= 0)
            {
                MessageBox.Show("L'utilisateur selectionne n'a pas d'id valide.",
                    "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Log.Information("Action: Modifier utilisateur - Id={Id}", utilisateurSelectionne.Id);

            // Verifie les informations saisies.
            if (!ValiderChamps()) return;

            // Creation de l'objet contenant les nouvelles valeurs.
            var utilisateur = new Utilisateur
            {
                Id = utilisateurSelectionne.Id,
                Nom = txtNom.Text.Trim(),
                Prenom = txtPrenom.Text.Trim(),
                Age = int.Parse(txtAge.Text.Trim())
            };

            try
            {
                bool succes = await _api.UpdateAsync(utilisateur);

                if (succes)
                {
                    MessageBox.Show("Utilisateur modifie !",
                        "Succes", MessageBoxButton.OK, MessageBoxImage.Information);

                    await ChargerUtilisateurs();
                    Vider();
                }
                else
                {
                    MessageBox.Show("Modification impossible.",
                        "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Echec de la modification de l utilisateur");

                MessageBox.Show("Erreur : " + ex.Message,
                    "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Cette methode supprime l'utilisateur selectionne apres confirmation,
        /// puis recharge la liste des utilisateurs.
        /// </summary>
        /// <param name="sender">Bouton Supprimer.</param>
        /// <param name="e">Informations sur le clic.</param>
        private async void btnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            var utilisateurSelectionne = GetUtilisateurSelectionne();

            if (utilisateurSelectionne == null)
            {
                MessageBox.Show("Selectionnez un utilisateur a supprimer.",
                    "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (utilisateurSelectionne.Id <= 0)
            {
                MessageBox.Show("L'utilisateur selectionne n'a pas d'id valide.",
                    "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Demande une confirmation avant la suppression.
            var confirmation = MessageBox.Show(
                "Voulez-vous supprimer cet utilisateur ?",
                "Confirmation",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (confirmation != MessageBoxResult.Yes)
                return;

            try
            {
                Log.Information("Action: Supprimer utilisateur - Id={Id}", utilisateurSelectionne.Id);

                bool succes = await _api.DeleteAsync(utilisateurSelectionne.Id);

                if (succes)
                {
                    MessageBox.Show("Utilisateur supprime !",
                        "Succes", MessageBoxButton.OK, MessageBoxImage.Information);

                    await ChargerUtilisateurs();
                    Vider();
                }
                else
                {
                    MessageBox.Show("Suppression impossible.",
                        "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Echec de la suppression de l utilisateur");

                MessageBox.Show("Erreur : " + ex.Message,
                    "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Verifie que les informations saisies par l'utilisateur sont valides.
        /// Le nom et le prenom ne doivent pas etre vides, et l'age doit etre un entier.
        /// </summary>
        /// <returns>
        /// true si toutes les donnees sont valides, sinon false.
        /// </returns>
        private bool ValiderChamps()
        {
            if (string.IsNullOrWhiteSpace(txtNom.Text))
            {
                Log.Warning("Validation echouee: champ Nom vide");

                MessageBox.Show("Le nom est obligatoire.",
                    "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);

                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPrenom.Text))
            {
                Log.Warning("Validation echouee: champ Prenom vide");

                MessageBox.Show("Le prenom est obligatoire.",
                    "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);

                return false;
            }

            if (!int.TryParse(txtAge.Text.Trim(), out _))
            {
                Log.Warning("Validation echouee: age invalide - valeur={V}", txtAge.Text);

                MessageBox.Show("L'age est invalide.",
                    "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);

                return false;
            }

            return true;
        }

        /// <summary>
        /// Charge tous les utilisateurs depuis l'API puis les affiche dans le DataGrid.
        /// </summary>
        /// <returns>Une tache asynchrone.</returns>
        private async Task ChargerUtilisateurs()
        {
            try
            {
                // Indique que le chargement est en cours.
                _chargementUtilisateurs = true;
                txtStatut.Text = "Chargement des utilisateurs...";

                // Vide l'ancienne source de donnees.
                dgUtilisateur.ItemsSource = null;

                // Recupere la liste des utilisateurs depuis l'API.
                var utilisateurs = await _api.GetAllAsync();
                dgUtilisateur.ItemsSource = utilisateurs;

                // Supprime la selection.
                dgUtilisateur.UnselectAll();

                txtSousTitre.Text = utilisateurs.Count + " utilisateur(s) dans la liste";
                txtStatut.Text = "Liste chargee.";
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Echec du chargement des utilisateurs");
                txtStatut.Text = "Erreur de chargement.";

                MessageBox.Show("Impossible de charger les utilisateurs : " + ex.Message,
                    "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                // Le chargement est termine.
                _chargementUtilisateurs = false;
            }
        }

        /// <summary>
        /// Vide les zones de saisie et retire la selection du DataGrid.
        /// </summary>
        private void Vider()
        {
            txtNom.Clear();
            txtPrenom.Clear();
            txtAge.Clear();

            dgUtilisateur.UnselectAll();
            dgUtilisateur.SelectedItem = null;
        }

        /// <summary>
        /// Retourne l'utilisateur actuellement selectionne dans le DataGrid.
        /// </summary>
        /// <returns>
        /// L'objet Utilisateur selectionne ou null si aucun utilisateur n'est selectionne.
        /// </returns>
        private Utilisateur GetUtilisateurSelectionne()
        {
            return dgUtilisateur.SelectedItem as Utilisateur;
        }

        /// <summary>
        /// Remplit les zones de texte avec les informations
        /// de l'utilisateur actuellement selectionne.
        /// </summary>
        private void RemplirChampsDepuisSelection()
        {
            // Ignore l'evenement pendant le chargement des donnees.
            if (_chargementUtilisateurs)
                return;

            var utilisateur = GetUtilisateurSelectionne();

            if (utilisateur == null)
                return;

            txtNom.Text = utilisateur.Nom;
            txtPrenom.Text = utilisateur.Prenom;
            txtAge.Text = utilisateur.Age.ToString();
        }
    }
}
