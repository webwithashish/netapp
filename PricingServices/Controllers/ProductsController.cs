using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Infosys.DBFirstCore.DataAccessLayer;
using Infosys.DBFirstCore.DataAccessLayer.Models;

namespace PricingServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        PricingRepository repository;
        public ProductsController(PricingRepository repository)
        {
            this.repository = repository;
        }

         [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            var products = repository.GetAllProducts();
            return Ok(products);
        }

        // GET: api/products/5
        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            var product = repository.GetProductById(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        // POST: api/products
        [HttpPost]
        public IActionResult AddProduct([FromBody] Product product)
        {
            if (repository.AddProduct(product))
                return CreatedAtAction(nameof(GetProduct), new { id = product.ProductId }, product);

            return BadRequest("Failed to add product.");
        }

        // PUT: api/products/5
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] Product product)
        {
            if (id != product.ProductId)
                return BadRequest("Product ID mismatch.");

            if (repository.UpdateProduct(product))
                return Ok("Product updated successfully.");

            return NotFound("Product not found.");
        }

        // DELETE: api/products/5
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            if (repository.DeleteProduct(id))
                return Ok("Product deleted successfully.");

            return NotFound("Product not found.");
        }

        // GET: api/products/5/price?date=2025-01-01
        [HttpGet("{id}/price")]
        public ActionResult<decimal> GetFinalPrice(int id, [FromQuery] DateTime date)
        {
            var price = repository.CalculateFinalPrice(id, date);
            return Ok(price);
        }
    }
}
