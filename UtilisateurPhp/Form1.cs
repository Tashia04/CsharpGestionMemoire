using UtilisateurPhp.Models;
using UtilisateurPhp.Services;
using Serilog;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UtilisateurPhp
{
    public partial class Form1 : Form
    {
        // Service chargé de communiquer avec l'API REST.
        // Toutes les opérations CRUD (Ajouter, Modifier, Supprimer, Lister)
        // passent par cet objet.
        private readonly ApiService _api = new ApiService();
        private bool _chargementUtilisateurs;

        public Form1()
        {
            InitializeComponent();
            dgUtilisateur.SelectionChanged += dgUtilisateur_SelectionChanged;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await ChargerUtilisateurs();
        }
        /// <summary>
        /// Cette méthode est appelée lorsqu'un utilisateur clique sur une cellule
        /// du DataGridView. Elle récupère l'utilisateur sélectionné et remplit
        /// automatiquement les zones de texte.
        /// </summary>
        /// <param name="sender">Contrôle ayant déclenché l'évènement.</param>
        /// <param name="e">Informations sur la cellule sélectionnée.</param>
        private void dgUtilisateur_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            RemplirChampsDepuisSelection();
        }

        /// <summary>
        /// Cette méthode est exécutée chaque fois que la sélection change
        /// dans le DataGridView afin de mettre à jour les champs du formulaire.
        /// </summary>
        /// <param name="sender">Contrôle ayant déclenché l'évènement.</param>
        /// <param name="e">Informations sur l'évènement.</param>
        private void dgUtilisateur_SelectionChanged(object sender, EventArgs e)
        {
            RemplirChampsDepuisSelection();
        }

        /// <summary>
        /// Cette méthode permet d'ajouter un nouvel utilisateur.
        /// Elle valide les informations saisies, crée un objet Utilisateur,
        /// l'envoie à l'API puis recharge la liste des utilisateurs.
        /// </summary>
        /// <param name="sender">Bouton Ajouter.</param>
        /// <param name="e">Informations sur le clic.</param>
        private async void btnAjouter_Click(object sender, EventArgs e)
        {
            Log.Information("Action: Ajouter utilisateur - Nom={Nom}", txtNom.Text);

            // Vérifie que toutes les données sont valides.
            if (!ValiderChamps()) return;

            // Création d'un objet Utilisateur à partir des valeurs saisies.
            var utilisateur = new Utilisateur
            {
                Nom = txtNom.Text.Trim(),
                Prenom = txtPrenom.Text.Trim(),
                Age = int.Parse(txtAge.Text)
            };

            try
            {
                // Envoie de l'utilisateur à l'API.
                bool succes = await _api.CreateAsync(utilisateur);

                if (succes)
                {
                    Log.Information("Utilisateur ajoute: {@Utilisateur}", utilisateur);

                    MessageBox.Show("Utilisateur ajoute !", "Succes",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Recharge la liste des utilisateurs.
                    await ChargerUtilisateurs();

                    // Nettoie les champs de saisie.
                    Vider();
                }
                else
                {
                    MessageBox.Show("Ajout impossible.", "Erreur",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Echec de l ajout de l utilisateur");

                MessageBox.Show("Erreur : " + ex.Message,
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Cette méthode permet de modifier l'utilisateur actuellement sélectionné.
        /// Les nouvelles informations sont envoyées à l'API puis la liste est actualisée.
        /// </summary>
        /// <param name="sender">Bouton Modifier.</param>
        /// <param name="e">Informations sur le clic.</param>
        private async void btnModifier_Click(object sender, EventArgs e)
        {
            // Récupère l'utilisateur sélectionné.
            var utilisateurSelectionne = GetUtilisateurSelectionne();

            if (utilisateurSelectionne == null)
            {
                MessageBox.Show("Selectionnez un utilisateur a modifier.",
                    "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Vérifie que l'utilisateur possède un identifiant valide.
            if (utilisateurSelectionne.Id <= 0)
            {
                MessageBox.Show("L'utilisateur selectionne n'a pas d'id valide.",
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Log.Information("Action: Modifier utilisateur - Id={Id}", utilisateurSelectionne.Id);

            // Vérifie les informations saisies.
            if (!ValiderChamps()) return;

            // Création de l'objet contenant les nouvelles valeurs.
            var utilisateur = new Utilisateur
            {
                Id = utilisateurSelectionne.Id,
                Nom = txtNom.Text.Trim(),
                Prenom = txtPrenom.Text.Trim(),
                Age = int.Parse(txtAge.Text)
            };

            try
            {
                bool succes = await _api.UpdateAsync(utilisateur);

                if (succes)
                {
                    MessageBox.Show("Utilisateur modifie !",
                        "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    await ChargerUtilisateurs();
                    Vider();
                }
                else
                {
                    MessageBox.Show("Modification impossible.",
                        "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Echec de la modification de l utilisateur");

                MessageBox.Show("Erreur : " + ex.Message,
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Cette méthode supprime l'utilisateur sélectionné après confirmation
        /// de l'utilisateur, puis recharge la liste des utilisateurs.
        /// </summary>
        /// <param name="sender">Bouton Supprimer.</param>
        /// <param name="e">Informations sur le clic.</param>
        private async void btnSupprimer_Click(object sender, EventArgs e)
        {
            var utilisateurSelectionne = GetUtilisateurSelectionne();

            if (utilisateurSelectionne == null)
            {
                MessageBox.Show("Selectionnez un utilisateur a supprimer.",
                    "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (utilisateurSelectionne.Id <= 0)
            {
                MessageBox.Show("L'utilisateur selectionne n'a pas d'id valide.",
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Demande une confirmation avant la suppression.
            var confirmation = MessageBox.Show(
                "Voulez-vous supprimer cet utilisateur ?",
                "Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmation != DialogResult.Yes)
                return;

            try
            {
                Log.Information("Action: Supprimer utilisateur - Id={Id}", utilisateurSelectionne.Id);

                bool succes = await _api.DeleteAsync(utilisateurSelectionne.Id);

                if (succes)
                {
                    MessageBox.Show("Utilisateur supprime !",
                        "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    await ChargerUtilisateurs();
                    Vider();
                }
                else
                {
                    MessageBox.Show("Suppression impossible.",
                        "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Echec de la suppression de l utilisateur");

                MessageBox.Show("Erreur : " + ex.Message,
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Vérifie que les informations saisies par l'utilisateur sont valides.
        /// Le nom et le prénom ne doivent pas être vides, et l'âge doit être un entier.
        /// </summary>
        /// <returns>
        /// true si toutes les données sont valides, sinon false.
        /// </returns>
        private bool ValiderChamps()
        {
            if (string.IsNullOrWhiteSpace(txtNom.Text))
            {
                Log.Warning("Validation echouee: champ Nom vide");

                MessageBox.Show("Le nom est obligatoire.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPrenom.Text))
            {
                Log.Warning("Validation echouee: champ Prenom vide");

                MessageBox.Show("Le prenom est obligatoire.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return false;
            }

            if (!int.TryParse(txtAge.Text, out _))
            {
                Log.Warning("Validation echouee: age invalide - valeur={V}", txtAge.Text);

                MessageBox.Show("L'age est invalide.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return false;
            }

            return true;
        }

        /// <summary>
        /// Charge tous les utilisateurs depuis l'API puis les affiche
        /// dans le DataGridView.
        /// </summary>
        /// <returns>Une tâche asynchrone.</returns>
        private async Task ChargerUtilisateurs()
        {
            try
            {
                // Indique que le chargement est en cours.
                _chargementUtilisateurs = true;

                // Génère automatiquement les colonnes du DataGridView.
                dgUtilisateur.AutoGenerateColumns = true;

                // Vide l'ancienne source de données.
                dgUtilisateur.DataSource = null;

                // Récupère la liste des utilisateurs depuis l'API.
                dgUtilisateur.DataSource = await _api.GetAllAsync();

                // Supprime la sélection.
                dgUtilisateur.ClearSelection();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Echec du chargement des utilisateurs");

                MessageBox.Show("Impossible de charger les utilisateurs : " + ex.Message,
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Le chargement est terminé.
                _chargementUtilisateurs = false;
            }
        }

        /// <summary>
        /// Vide les zones de saisie et retire la sélection
        /// du DataGridView.
        /// </summary>
        private void Vider()
        {
            txtNom.Clear();
            txtPrenom.Clear();
            txtAge.Clear();

            dgUtilisateur.ClearSelection();
        }

        /// <summary>
        /// Retourne l'utilisateur actuellement sélectionné dans le DataGridView.
        /// </summary>
        /// <returns>
        /// L'objet Utilisateur sélectionné ou null si aucun utilisateur n'est sélectionné.
        /// </returns>
        private Utilisateur GetUtilisateurSelectionne()
        {
            if (dgUtilisateur.SelectedRows.Count == 0 &&
                dgUtilisateur.SelectedCells.Count == 0)
                return null;

            if (dgUtilisateur.CurrentRow == null)
                return null;

            return dgUtilisateur.CurrentRow.DataBoundItem as Utilisateur;
        }

        /// <summary>
        /// Remplit les zones de texte avec les informations
        /// de l'utilisateur actuellement sélectionné.
        /// </summary>
        private void RemplirChampsDepuisSelection()
        {
            // Ignore l'évènement pendant le chargement des données.
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
