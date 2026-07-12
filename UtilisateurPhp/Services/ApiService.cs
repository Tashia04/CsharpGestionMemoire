using UtilisateurPhp.Models;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace UtilisateurPhp.Services
{
    /// <summary>
    /// Service responsable de la communication avec l'API REST PHP.
    /// Il gère les requêtes HTTP nécessaires au fonctionnement du CRUD (Lister, Créer, Modifier, Supprimer, Détails).
    /// Note : Contrairement à d'autres API qui consomment du JSON, cette API PHP attend des données formatées
    /// en "x-www-form-urlencoded" via la variable superglobale $_POST.
    /// </summary>
    public class ApiService
    {
        private readonly HttpClient _client;
        
        // URL d'accès aux différents scripts PHP gérant le CRUD
        private const string ListUrl = "http://localhost/backend/list.php";
        private const string CreateUrl = "http://localhost/backend/create.php";
        private const string UpdateUrl = "http://localhost/backend/update.php";
        private const string DeleteUrl = "http://localhost/backend/delete.php";
        private const string DetailsUrl = "http://localhost/backend/details.php";

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="ApiService"/>.
        /// Configure le client HTTP et définit un délai d'attente maximum (timeout).
        /// </summary>
        public ApiService()
        {
            _client = new HttpClient();
            _client.Timeout = TimeSpan.FromSeconds(30);
            Log.Debug("ApiService initialise — Endpoints configures.");
        }

        /// <summary>
        /// Modèle interne utilisé pour désérialiser la réponse des opérations de création, modification et suppression.
        /// La réponse attendue du backend PHP est au format JSON : {"success": true|false} ou {"id": X, "success": true|false}.
        /// </summary>
        private class ApiResponse
        {
            [JsonProperty("success")]
            public bool Success { get; set; }
        }

        /// <summary>
        /// Modèle interne utilisé pour désérialiser la réponse du script details.php.
        /// La réponse attendue est au format JSON : {"result": {"nom": "...", "prenom": "...", "age": X}}.
        /// </summary>
        private class DetailsResponse
        {
            [JsonProperty("result")]
            public Utilisateur Result { get; set; }
        }

        /// <summary>
        /// Récupère la liste de tous les utilisateurs depuis le script list.php.
        /// </summary>
        /// <returns>Une liste d'objets <see cref="Utilisateur"/>.</returns>
        public async Task<List<Utilisateur>> GetAllAsync()
        {
            Log.Information("GET tous les utilisateurs");
            try
            {
                // Envoi d'une requête GET simple
                var response = await _client.GetStringAsync(ListUrl);
                
                // Désérialisation du tableau JSON en liste d'utilisateurs
                var utilisateurs = JsonConvert.DeserializeObject<List<Utilisateur>>(response);
                if (utilisateurs == null) utilisateurs = new List<Utilisateur>();
                
                Log.Information("{Count} utilisateur(s) recupere(s)", utilisateurs.Count);
                return utilisateurs;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erreur lors de GetAllAsync");
                throw;
            }
        }

        /// <summary>
        /// Envoie les informations d'un nouvel utilisateur au script create.php.
        /// </summary>
        /// <param name="utilisateur">L'objet Utilisateur contenant le nom, prénom et l'âge.</param>
        /// <returns>True si la création a réussi avec succès, sinon False.</returns>
        public async Task<bool> CreateAsync(Utilisateur utilisateur)
        {
            Log.Information("POST creation: {@Utilisateur}", utilisateur);
            try
            {
                // Préparation des données sous forme de paires clé-valeur pour simuler un formulaire HTML (POST)
                var postData = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("nom", utilisateur.Nom),
                    new KeyValuePair<string, string>("prenom", utilisateur.Prenom),
                    new KeyValuePair<string, string>("age", utilisateur.Age.ToString())
                };
                
                // Formatage des données en "application/x-www-form-urlencoded"
                var content = new FormUrlEncodedContent(postData);
                var response = await _client.PostAsync(CreateUrl, content);

                if (!response.IsSuccessStatusCode)
                {
                    Log.Warning("Creation echouee — StatusCode: {Code}", response.StatusCode);
                    return false;
                }

                // Lecture de la réponse JSON du serveur
                var responseBody = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseBody);
                
                // Vérification de la propriété "success" renvoyée par le script PHP
                if (apiResponse != null && apiResponse.Success)
                {
                    Log.Information("Utilisateur cree avec succes");
                    return true;
                }

                Log.Warning("Creation echouee — Response: {Response}", responseBody);
                return false;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erreur CreateAsync");
                throw;
            }
        }

        /// <summary>
        /// Modifie les informations d'un utilisateur existant via le script update.php.
        /// </summary>
        /// <param name="utilisateur">L'objet Utilisateur contenant l'identifiant et les nouvelles valeurs.</param>
        /// <returns>True si la mise à jour a réussi, sinon False.</returns>
        public async Task<bool> UpdateAsync(Utilisateur utilisateur)
        {
            Log.Information("POST modification: {@Utilisateur}", utilisateur);
            try
            {
                // Préparation des données incluant l'ID unique de l'utilisateur à modifier
                var postData = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("id", utilisateur.Id.ToString()),
                    new KeyValuePair<string, string>("nom", utilisateur.Nom),
                    new KeyValuePair<string, string>("prenom", utilisateur.Prenom),
                    new KeyValuePair<string, string>("age", utilisateur.Age.ToString())
                };
                
                var content = new FormUrlEncodedContent(postData);
                var response = await _client.PostAsync(UpdateUrl, content);
                var responseBody = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    Log.Warning("Modification echouee - StatusCode: {Code}, Response: {Response}", response.StatusCode, responseBody);
                    return false;
                }

                var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseBody);

                // Vérification du succès retourné par le backend PHP
                if (apiResponse != null && apiResponse.Success)
                {
                    Log.Information("Utilisateur modifie avec succes");
                    return true;
                }

                Log.Warning("Modification echouee — Response: {Response}", responseBody);
                return false;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erreur UpdateAsync");
                throw;
            }
        }

        /// <summary>
        /// Supprime un utilisateur par son identifiant unique via le script delete.php.
        /// </summary>
        /// <param name="id">L'identifiant de l'utilisateur à supprimer.</param>
        /// <returns>True si la suppression a réussi, sinon False.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            Log.Information("POST suppression: Id={Id}", id);
            try
            {
                // Envoi de l'identifiant sous forme de formulaire url-encoded
                var postData = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("id", id.ToString())
                };
                
                var content = new FormUrlEncodedContent(postData);
                var response = await _client.PostAsync(DeleteUrl, content);
                var responseBody = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    Log.Warning("Suppression echouee - StatusCode: {Code}, Response: {Response}", response.StatusCode, responseBody);
                    return false;
                }

                var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseBody);

                // Vérification du succès retourné par le script delete.php
                if (apiResponse != null && apiResponse.Success)
                {
                    Log.Information("Utilisateur supprime avec succes");
                    return true;
                }

                Log.Warning("Suppression echouee — Response: {Response}", responseBody);
                return false;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erreur DeleteAsync");
                throw;
            }
        }

        /// <summary>
        /// Récupère les détails d'un utilisateur spécifique à partir de son identifiant unique via details.php.
        /// </summary>
        /// <param name="id">L'identifiant de l'utilisateur à inspecter.</param>
        /// <returns>L'objet <see cref="Utilisateur"/> correspondant, ou null s'il n'est pas trouvé.</returns>
        public async Task<Utilisateur> GetByIdAsync(int id)
        {
            Log.Information("POST details: Id={Id}", id);
            try
            {
                // Envoi de l'identifiant pour la recherche
                var postData = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("id", id.ToString())
                };
                
                var content = new FormUrlEncodedContent(postData);
                var response = await _client.PostAsync(DetailsUrl, content);
                var responseBody = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    Log.Warning("Details echouee — StatusCode: {Code}, Response: {Response}", response.StatusCode, responseBody);
                    return null;
                }

                var detailsResponse = JsonConvert.DeserializeObject<DetailsResponse>(responseBody);
                
                // Si l'utilisateur est trouvé, on lui réassocie l'ID passé en paramètre car le SELECT du PHP ne le renvoie pas
                if (detailsResponse?.Result != null)
                {
                    detailsResponse.Result.Id = id;
                    Log.Information("Details de l'utilisateur recuperes avec succes");
                    return detailsResponse.Result;
                }

                Log.Warning("Details non trouves ou echoues — Response: {Response}", responseBody);
                return null;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erreur GetByIdAsync");
                throw;
            }
        }
    }
}
