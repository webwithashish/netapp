using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Infosys.DBFirstCore.DataAccessLayer;
using Infosys.DBFirstCore.DataAccessLayer.Models;

namespace PricingServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarkerPricesController : Controller
    {
        PricingRepository repository;
        public MarkerPricesController(PricingRepository repository)
        {
            this.repository = repository;
        }



        [HttpGet("marker/{markerId}")]
        public ActionResult<IEnumerable<MarkerPrice>> GetMarkerPrices(int markerId)
        {
            return Ok(repository.GetMarkerPricesByMarker(markerId));
        }

        [HttpGet("{id}")]
        public ActionResult<MarkerPrice> GetMarkerPrice(int id)
        {
            var price = repository.GetMarkerPriceById(id);
            if (price == null) return NotFound();
            return Ok(price);
        }

        [HttpPost]
        public IActionResult AddMarkerPrice([FromBody] MarkerPrice price)
        {
            if (repository.AddMarkerPrice(price)) return Ok("MarkerPrice added.");
            return BadRequest("Failed to add MarkerPrice.");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMarkerPrice(int id, [FromBody] MarkerPrice price)
        {
            if (id != price.MarkerPriceId) return BadRequest();
            if (repository.UpdateMarkerPrice(price)) return Ok("MarkerPrice updated.");
            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMarkerPrice(int id)
        {
            if (repository.DeleteMarkerPrice(id)) return Ok("MarkerPrice deleted.");
            return NotFound();
        }


    }

}

