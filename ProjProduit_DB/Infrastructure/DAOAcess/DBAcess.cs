using Microsoft.EntityFrameworkCore;
using ProduitProj.Model.Domaine.Entities;

namespace ProjProduit_DB.Infrastructure.DAOAcess
{
    public class DBAcess : DbContext
    {
        public DBAcess(DbContextOptions<DBAcess> options) : base(options)
        {
        }

        public DbSet<Produit> Produits { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produit>()
                .HasKey(p => p.IdProd); 
        }
    }
}
