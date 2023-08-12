using Microsoft.AspNetCore.Mvc;
using PlantNestBackEnd.Services;

namespace PlantNestBackEnd.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class OrderController : ControllerBase
    {
        private IOrder orderService;
        public OrderController(IOrder _orderService)
        {
            this.orderService = _orderService;
        }
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpGet("findAll")]
        public IActionResult FindAll()
        {

            try
            {
                return Ok(orderService.findAll());
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
