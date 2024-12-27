namespace ProduitProj.Model.Domaine.Entities
{
    public class Produit
    {
        public int IdProd { get; set; }
        public string Libelle { get; set; }
        public double Qt { get; set; }
        public double Prix { get; set; }

        public Produit() { }
        public Produit(int idProd, string libelle, double qt, double prix)
        {
            IdProd = idProd;
            Libelle = libelle;
            Qt = qt;
            Prix = prix;
        }
    }

}
