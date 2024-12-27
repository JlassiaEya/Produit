using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ProduitProj.Model.Domaine.Entities;

namespace ProduitProj.Model.Domaine.IDAO
{
    public interface IProduitDao
    {
        IEnumerable<Produit> GetProduits(); 
        Produit GetProduit(int id);
        ActionResult<IEnumerable<Produit>> AddProduit(Produit produit);
        ActionResult<IEnumerable<Produit>> UpdateProduit(int id, Produit produit);
        ActionResult<IEnumerable<Produit>> DeleteProduit(int id); 
    }
}
