using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Produit.Entities
{
    public class Produits
    {
            public int IdProd { get; set; }
            public string Libelle { get; set; }
            public double Qt { get; set; }
            public double Prix { get; set; }

            public Produits() { }
            public Produits(int idProd, string libelle, double qt, double prix)
            {
                IdProd = idProd;
                Libelle = libelle;
                Qt = qt;
                Prix = prix;
            }
    }

   
}
