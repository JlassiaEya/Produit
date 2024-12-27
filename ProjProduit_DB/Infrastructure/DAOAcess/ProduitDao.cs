using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ProduitProj.Model.Domaine.Entities;
using ProduitProj.Model.Domaine.IDAO;

namespace ProjProduit_DB.Infrastructure.DAOAcess
{
    public class ProduitDao : IProduitDao
    {
        private readonly DBAcess _dbAccess;

        public ProduitDao(DBAcess dbAccess)
        {
            _dbAccess = dbAccess;
        }

        public IEnumerable<Produit> GetProduits()
        {
            try
            {
                return _dbAccess.Produits.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Une erreur s'est produite : {ex.Message}");
                return new List<Produit>(); 
            }
        }

        public Produit GetProduit(int id)
        {
            return _dbAccess.Produits.Find(id);
        }

        public ActionResult<IEnumerable<Produit>> AddProduit(Produit produit)
        {
            if (produit == null)
            {
                return new BadRequestResult();
            }

            _dbAccess.Produits.Add(produit);
            _dbAccess.SaveChanges();

            return GetProduits().ToList(); 
        }

        public ActionResult<IEnumerable<Produit>> UpdateProduit(int id, Produit produit)
        {
            if (produit == null)
            {
                return new BadRequestResult();
            }

            var existingProduit = GetProduit(id);
            if (existingProduit == null)
            {
                return new NotFoundResult();
            }

            existingProduit.Libelle = produit.Libelle;
            existingProduit.Qt = produit.Qt;
            existingProduit.Prix = produit.Prix;

            _dbAccess.SaveChanges();
            return GetProduits().ToList(); 
        }

        public ActionResult<IEnumerable<Produit>> DeleteProduit(int id)
        {
            var produit = GetProduit(id);
            if (produit == null)
            {
                return new NotFoundResult();
            }

            _dbAccess.Produits.Remove(produit);
            _dbAccess.SaveChanges();

            return GetProduits().ToList(); 
        }
    }
}
