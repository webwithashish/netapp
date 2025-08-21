using Infosys.QuickKart.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Infosys.QuickKart.Services.Models;
using Infosys.QuickKart.DAL.Models;

namespace Infosys.QuicKart.Services.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : Controller
    {
        QuickKartRepository repository;
        public ProductController(QuickKartRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public JsonResult GetProducts()
        {
            try
            {
                var productList = this.repository.DisplayProductDetails();
                var products = new List<QuickKart.Services.Models.Product>();
                if (productList.Any())
                {
                    foreach (var prod in productList)
                    {
                        var product = new QuickKart.Services.Models.Product();
                        product.ProductId = prod.ProductId;
                        product.ProductName = prod.ProductName;
                        product.CategoryId = prod.CategoryId;
                        product.Price = prod.Price;
                        product.QuantityAvailable = prod.QuantityAvailable;

                        products.Add(product);
                    }
                }
                return Json(products);
            }
            catch (Exception)
            {
                return Json(null);
            }
        }
    }
}