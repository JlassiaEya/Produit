using System.Collections.Generic;
using System.Threading.Tasks;
using Produit.Entities;

namespace Produit.Services
{
    public interface IProduitService
    {
        Task<IEnumerable<Produits>> GetProduitsAsync();
        Task<Produits> GetProduitByIdAsync(int id);
        Task AddProduitAsync(Produits produit);
        Task UpdateProduitAsync(int id, Produits produit);
        Task DeleteProduitAsync(int id);
    }
}
