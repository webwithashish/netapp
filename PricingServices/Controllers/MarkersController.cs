using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Infosys.DBFirstCore.DataAccessLayer;
using Infosys.DBFirstCore.DataAccessLayer.Models;

namespace PricingServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarkersController : Controller
    {
        PricingRepository repository;
        public MarkersController(PricingRepository repository)
        {
            this.repository = repository;
        }


        [HttpGet]
        public ActionResult<IEnumerable<Marker>> GetMarkers()
        {
            return Ok(repository.GetAllMarkers());
        }

        [HttpGet("{id}")]
        public ActionResult<Marker> GetMarker(int id)
        {
            var marker = repository.GetMarkerById(id);
            if (marker == null) return NotFound();
            return Ok(marker);
        }

        [HttpPost]
        public IActionResult AddMarker([FromBody] Marker marker)
        {
            if (repository.AddMarker(marker))
                return Ok("Marker added successfully.");
            return BadRequest("Failed to add marker.");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMarker(int id, [FromBody] Marker marker)
        {
            if (id != marker.MarkerId) return BadRequest();
            if (repository.UpdateMarker(marker)) return Ok("Marker updated.");
            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMarker(int id)
        {
            if (repository.DeleteMarker(id)) return Ok("Marker deleted.");
            return NotFound();
        }

    }

}