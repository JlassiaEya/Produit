using System.Net.Http.Json;
using Produit.Entities;
using Produit.Services;

namespace Produit.Infrastructure
{
    public class ProduitService : IProduitService
    {
        private readonly HttpClient _httpClient;

        public ProduitService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
           
        }

        public async Task<IEnumerable<Produits>> GetProduitsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("Produits");

                if (response.IsSuccessStatusCode)
                {
                    var produits = await response.Content.ReadFromJsonAsync<IEnumerable<Produits>>();
                    return produits ?? new List<Produits>();
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Erreur HTTP: {response.StatusCode}. Contenu: {errorContent}");
                    return new List<Produits>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la récupération des produits : {ex.Message}");
                throw new HttpRequestException("Erreur lors de la récupération des produits.", ex);
            }
        }


        public async Task<Produits> GetProduitByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Produits>($"Produits/{id}");
            }
            catch (Exception ex)
            {
                throw new HttpRequestException($"Erreur lors de la récupération du produit avec ID {id}.", ex);
            }
        }

        public async Task AddProduitAsync(Produits produit)
        {
            try
            {
                await _httpClient.PostAsJsonAsync("Produits/add", produit);
            }
            catch (Exception ex)
            {
                throw new HttpRequestException("Erreur lors de l'ajout du produit.", ex);
            }
        }

        public async Task UpdateProduitAsync(int id, Produits produit)
        {
            try
            {
                await _httpClient.PutAsJsonAsync($"Produits/Update/{id}", produit);
            }
            catch (Exception ex)
            {
                throw new HttpRequestException($"Erreur lors de la mise à jour du produit avec ID {id}.", ex);
            }
        }

        public async Task DeleteProduitAsync(int id)
        {
            try
            {
                await _httpClient.DeleteAsync($"Produits/Delete/{id}");
            }
            catch (Exception ex)
            {
                throw new HttpRequestException($"Erreur lors de la suppression du produit avec ID {id}.", ex);
            }
        }
    }
}
