using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Infosys.DBFirstCore.DataAccessLayer;
using Infosys.DBFirstCore.DataAccessLayer.Models;

namespace PricingServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDiffsController : Controller
    {
        PricingRepository repository;
        public ProductDiffsController(PricingRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("product/{productId}")]
        public ActionResult<IEnumerable<ProductDiff>> GetDiffs(int productId)
        {
            return Ok(repository.GetProductDiffsByProduct(productId));
        }

        [HttpPost]
        public IActionResult AddProductDiff([FromBody] ProductDiff diff)
        {
            if (repository.AddProductDiff(diff)) return Ok("ProductDiff added.");
            return BadRequest("Failed to add ProductDiff.");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProductDiff(int id, [FromBody] ProductDiff diff)
        {
            if (id != diff.ProductDiffId) return BadRequest();
            if (repository.UpdateProductDiff(diff)) return Ok("ProductDiff updated.");
            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProductDiff(int id)
        {
            if (repository.DeleteProductDiff(id)) return Ok("ProductDiff deleted.");
            return NotFound();
        }


    }

}



