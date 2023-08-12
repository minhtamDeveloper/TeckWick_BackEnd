using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlantNest.Services;
using PlantNestBackEnd.Services;

namespace PlantNestBackEnd.Controllers;
[Route("api/supplier")]

[ApiController]
public class SupplierController : ControllerBase
{
    private ISupplier   supplierService;
    private IWebHostEnvironment webHostEnvironment;
    private IConfiguration configuration;
    public SupplierController(ISupplier _supplierService, IWebHostEnvironment _webHostEnvironment, IConfiguration _configuration)
    {
        supplierService = _supplierService;
        webHostEnvironment = _webHostEnvironment;
        configuration = _configuration;
    }
    // SHOW ALL ROLES
    [Produces("application/json")]
    [HttpGet("showAll")]
    public IActionResult showAll()
    {
        try
        {
            var rs = supplierService.showAll();
            return Ok(rs);
        }
        catch (Exception ex)
        {
            // ma 400: la co loi roi
            return BadRequest(ex);
        }
    }
    [Produces("application/json")]
    [HttpGet("searchBySupId/{id}")]
    public IActionResult SearchByName(int id)
    {
        try
        {
            var find = supplierService.SearchId(id);
            return Ok(find);
        }
        catch (Exception ex)
        {
            // ma 400: la co loi roi
            return BadRequest(ex);
        }
    }
    [Produces("application/json")]
    [HttpGet("search/{keyword}")]
    public IActionResult SearchByName(string keyword)
    {
        try
        {
            var find = supplierService.Search(keyword);
            return Ok(find);
        }
        catch (Exception ex)
        {
            // ma 400: la co loi roi
            return BadRequest(ex);
        }
    }
}

