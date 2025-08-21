using Infosys.QuickKart.DAL;
using Infosys.QuickKart.Services.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Infosys.QuicKart.Services.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : Controller
    {
        QuickKartRepository repository;
        public CategoryController(QuickKartRepository repository)
        {
            this.repository = repository;
        }
        [HttpGet]
        public JsonResult GetCategories()
        {
            try
            {
                var categoryList = this.repository.GetCategoriesUsingLinq();
                var categories = new List<Category>();
                if (categoryList.Any())
                {
                    foreach (var cat in categoryList)
                    {

                        var category = new Category();
                        category.CategoryId = cat.CategoryId;
                        category.CategoryName = cat.CategoryName;
                        categories.Add(category);
                    }
                }
                return Json(categories);
            }
            catch (Exception ex)
            {
                return Json(null);
            }
        }
    }
}
