using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlantNestBackEnd.Models;
using PlantNestBackEnd.Services;

namespace PlantNestBackEnd.Controllers;
[Route("api/[controller]")]
[ApiController]
public class DeliveryController : ControllerBase
{
    private IDelivery deliveryService;

    public DeliveryController(IDelivery _deliveryService)
    {
        deliveryService = _deliveryService;

    }

    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost("created")]
    public async Task<IActionResult> Created([FromBody] Delivery delivery)
    {
        try
        {
            var status = deliveryService.created(delivery);
            return Ok(new { status });
        }
        catch
        {
            return BadRequest();
        }
    }

    [Produces("application/json")]
    [HttpGet("findByOrderId/{orderId}")]
    public IActionResult FindByOrderId(int orderId)
    {
        try
        {
            var fillAll = deliveryService.findByOrderId(orderId);
            return Ok(fillAll);
        }
        catch (Exception ex)
        {
            // ma 400: la co loi roi
            return BadRequest(ex);
        }
    }

    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpGet("UpdateDeliveryStatus/{id}")]
    public IActionResult UpdateDeliveryStatus(int id)
    {
        try
        {
            return Ok(new
            {
                status = deliveryService.UpdateDeliveryStatus(id)
            });
        }
        catch
        {
            return BadRequest();
        }
    }

    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpGet("UpdateDeliveryStatus2/{id}")]
    public IActionResult UpdateDeliveryStatus2(int id)
    {
        try
        {
            return Ok(new
            {
                status = deliveryService.UpdateDeliveryStatus2(id)
            });
        }
        catch
        {
            return BadRequest();
        }
    }
}
