using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlantNestBackEnd.Models;
using PlantNestBackEnd.Services;

namespace PlantNestBackEnd.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private IProduct productService;


     private readonly DatabaseContext _databaseContext;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public ProductController(IProduct productService,DatabaseContext databaseContext,IWebHostEnvironment webHostEnvironment) { 
        this.productService = productService; 
        _databaseContext = databaseContext;
        _webHostEnvironment = webHostEnvironment;
    }

    //[Produces("application/json")]
    //[Consumes("multipart/form-data")]
    [HttpPost("create")]
    public  IActionResult create()
    {
        
         return  Ok();
    }
    [HttpGet("Thang/getAllSupplier")]
     public async Task<IActionResult> getAllSupplier()
    {
     var a = await _databaseContext.Suppliers.AsNoTracking().Select(b => new
     {      
         b.Id,
         b.SupplierName,
         b.Status,
        
     }).ToListAsync();
        
        return Ok(a);
    }



}
