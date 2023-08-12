using Microsoft.AspNetCore.Mvc;
using PlantNestBackEnd.Services;

namespace PlantNestBackEnd.Controllers
{
    [Route("api/[controller]")]
    public class OrderDetailController : Controller
    {
        private IOrderDetail orderDetailService;
        public OrderDetailController(IOrderDetail _orderDetailService)
        {
            this.orderDetailService = _orderDetailService;
        }
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpGet("findByIdOrder/{id}")]
        public IActionResult FindAll(int id)
        {

            try
            {
                return Ok(orderDetailService.findByIdOrder(id));
            }
            catch
            {
                return BadRequest();
            }
        }
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpGet("findAll")]
        public IActionResult FindAll()
        {

            try
            {
                return Ok(orderDetailService.findAll());
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}

