using Microsoft.AspNetCore.Mvc;
using PlantNestBackEnd.Services;

namespace PlantNestBackEnd.Controllers
{
    [Route("api/[controller]")]
    public class DeliveryController : Controller
    {
        private IDelivery deliveryService;
        public DeliveryController(IDelivery _deliveryService)
        {
            this.deliveryService = _deliveryService;
        }
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpGet("findByIdOrder/{id}")]
        public IActionResult FindAll(int id)
        {

            try
            {
                return Ok(deliveryService.findByIdOrder(id));
            }
            catch
            {
                return BadRequest();
            }
        }
  
    }
}

