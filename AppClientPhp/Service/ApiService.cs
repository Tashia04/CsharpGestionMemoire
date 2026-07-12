using AppClientPhp.Model;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppClientPhp.Service
{
    public class ApiService
    {
        private readonly HttpClient _client;
        private const string BaseUrl = "http://localhost/api/produits.php";
        public ApiService()
        {
            _client = new HttpClient();
            _client.Timeout = TimeSpan.FromSeconds(30);
            Log.Debug("ApiService initialise — BaseUrl: {BaseUrl}", BaseUrl);
        }
        public async Task<List<Produit>> GetAllAsync()
        {
            Log.Information("GET tous les produits");
            try
            {
                var response = await _client.GetStringAsync(BaseUrl);
                var produits = JsonConvert.DeserializeObject<List<Produit>>(response);
                if (produits == null) produits = new List<Produit>();
                Log.Information("{Count} produit(s) recupere(s)", produits.Count);
                return produits;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erreur lors de GetAllAsync");
                throw;
            }
        }
        public async Task<bool> CreateAsync(Produit produit)
        {
            Log.Information("POST creation: {@Produit}", produit);
            try
            {
                var json = JsonConvert.SerializeObject(produit);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _client.PostAsync(BaseUrl, content);
                if (response.IsSuccessStatusCode)
                    Log.Information("Produit cree avec succes");
                else Log.Warning("Creation echouee — StatusCode: {Code}", response.StatusCode);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erreur CreateAsync"); throw;
            }
        }

        public async Task<bool> UpdateAsync(Produit produit)
        {
            Log.Information("PUT modification: {@Produit}", produit);
            try
            {
                var json = JsonConvert.SerializeObject(produit);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _client.PutAsync(BaseUrl + "?id=" + produit.Id, content);
                var responseBody = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                    Log.Information("Produit modifie avec succes");
                else Log.Warning("Modification echouee - StatusCode: {Code}, Response: {Response}", response.StatusCode, responseBody);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erreur UpdateAsync"); throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Log.Information("DELETE suppression: Id={Id}", id);
            try
            {
                var response = await _client.DeleteAsync(BaseUrl + "?id=" + id);
                if (response.IsSuccessStatusCode)
                    Log.Information("Produit supprime avec succes");
                else Log.Warning("Suppression echouee - StatusCode: {Code}", response.StatusCode);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erreur DeleteAsync"); throw;
            }
        }
    }


}
