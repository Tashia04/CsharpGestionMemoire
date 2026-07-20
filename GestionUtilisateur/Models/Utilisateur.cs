using Newtonsoft.Json;

namespace GestionUtilisateur.Models
{
    /// <summary>
    /// Represente un utilisateur renvoye par l'API PHP.
    /// Les attributs [JsonProperty] font le lien entre les champs JSON
    /// (id, nom, prenom, age) et les proprietes C#.
    /// </summary>
    public class Utilisateur
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("nom")]
        public string Nom { get; set; }

        [JsonProperty("prenom")]
        public string Prenom { get; set; }

        [JsonProperty("age")]
        public int Age { get; set; }
    }
}
