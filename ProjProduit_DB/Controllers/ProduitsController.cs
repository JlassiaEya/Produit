using Microsoft.AspNetCore.Mvc;
using ProduitProj.Model.Domaine.IDAO;
using ProduitProj.Model.Domaine.Entities;

namespace ProduitProj.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProduitsController : ControllerBase
    {
        private IProduitDao _produitDao;

        public ProduitsController(IProduitDao produitDao)
        {
            _produitDao = produitDao;
        }

        [HttpGet]
        public IEnumerable<Produit> GetProduits()
        {
            return _produitDao.GetProduits();
        }

        [HttpGet("{id}")]
        public ActionResult<Produit> GetProduit(int id)
        {
            var produit = _produitDao.GetProduit(id);
            if (produit == null)
            {
                return NotFound();
            }
            return produit;
        }

        [HttpPost("add")]
        public ActionResult<Produit> AddProduct([FromBody] Produit produit)
        {
            if (produit == null)
            {
                return BadRequest("Product is null.");
            }

            _produitDao.AddProduit(produit);

            var produits = _produitDao.GetProduits();
            return Ok(produits);
        }

        [HttpPut("Update/{id}")]
        public ActionResult UpdateProduct(int id, [FromBody] Produit produit)
        {
            if (produit == null)
            {
                return BadRequest("Product is null.");
            }

            var existingProduit = _produitDao.GetProduit(id);
            if (existingProduit == null)
            {
                return NotFound();
            }

            _produitDao.UpdateProduit(id, produit);
            var produits = _produitDao.GetProduits();
            return Ok(produits);
        }

        [HttpDelete("Delete/{id}")]
        public ActionResult DeleteProduct(int id)
        {
            var existingProduit = _produitDao.GetProduit(id);
            if (existingProduit == null)
            {
                return NotFound();
            }

            _produitDao.DeleteProduit(id);
            var produits = _produitDao.GetProduits();
            return Ok(produits);
        }
    }
}  

