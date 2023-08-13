using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlantNestBackEnd.Models;
using PlantNestBackEnd.Services;

namespace PlantNestBackEnd.Controllers;
[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private IOrder orderService;

    public OrderController(IOrder _orderService)
    {
        orderService = _orderService;

    }

    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost("created")]
    public async Task<IActionResult> Created([FromBody] Order order)
    {
        try
        {
            var status = orderService.created(order);
            return Ok(new { status });
        }
        catch
        {
            return BadRequest();
        }
    }
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpGet("findAll")]
    public IActionResult FindAll()
    {
        try
        {
            var fillAll = orderService.findAll();
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
    [HttpGet("findOrderMax")]
    public IActionResult FindOrderMax()
    {
        try
        {
            var fillAll = orderService.findOrderMax();
            return Ok(fillAll);
        }
        catch (Exception ex)
        {
            // ma 400: la co loi roi
            return BadRequest(ex);
        }
    }

    [Produces("application/json")]
    [HttpGet("findByAccountId/{accountId}")]
    public IActionResult FindByAccountId(int accountId)
    {
        try
        {
            var fillAll = orderService.findByAccountId(accountId);
            return Ok(fillAll);
        }
        catch (Exception ex)
        {
            // ma 400: la co loi roi
            return BadRequest(ex);
        }
    }

    [Produces("application/json")]
    [HttpGet("findByAccountId2/{accountId}")]
    public IActionResult FindByAccountId2(int accountId)
    {
        try
        {
            var fillAll = orderService.findByAccountId2(accountId);
            return Ok(fillAll);
        }
        catch (Exception ex)
        {
            // ma 400: la co loi roi
            return BadRequest(ex);
        }
    }

    [Produces("application/json")]
    [HttpGet("findByAccountId3/{accountId}")]
    public IActionResult FindByAccountId3(int accountId)
    {
        try
        {
            var fillAll = orderService.findByAccountId3(accountId);
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
    [HttpGet("updateOrderStatus/{orderId}")]
    public IActionResult UpdateOrderStatus(int orderId)
    {
        try
        {
            return Ok(new
            {
                status = orderService.UpdateOrderStatus(orderId)
            });
        }
        catch
        {
            return BadRequest();
        }
    }

    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpGet("updateOrderStatus2/{orderId}")]
    public IActionResult UpdateOrderStatus2(int orderId)
    {
        try
        {
            return Ok(new
            {
                status = orderService.UpdateOrderStatus2(orderId)
            });
        }
        catch
        {
            return BadRequest();
        }
    }
}
