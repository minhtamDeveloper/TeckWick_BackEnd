using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlantNestBackEnd.Models;
using PlantNestBackEnd.Services;

namespace PlantNestBackEnd.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private IProduct productService;
    public ProductController(IProduct productService) { 
        this.productService = productService; 
    }

    //[Produces("application/json")]
    //[Consumes("multipart/form-data")]
    [HttpPost("create")]
    public  IActionResult create()
    {
        
         return  Ok();
    }

}
