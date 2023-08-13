
using Microsoft.AspNetCore.Mvc;
using PlantNestBackEnd.Models;
using PlantNestBackEnd.Services;

namespace DemoOnlineFloralDelivery.Controllers;
[Route("api/orderDetail")]
public class OrderDetailController : ControllerBase
{
    private IOrderDetail orderDetailService;

    public OrderDetailController(IOrderDetail _orderDetailService)
    {
        orderDetailService = _orderDetailService;

    }
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost("created")]
    public IActionResult Created([FromBody] List<OrderDetail> orderDetails)
    {
        try
        {
            return Ok(new
            {
                status = orderDetailService.created(orderDetails)
            });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message); // Trả về thông tin lỗi cụ thể
        }
    }
}
