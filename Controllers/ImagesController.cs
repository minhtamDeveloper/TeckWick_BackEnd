using Microsoft.AspNetCore.Mvc;
using PlantNest.Services;
using PlantNestBackEnd.Services;

namespace PlantNest.Controllers;
[Route("api/images")]
[ApiController]
public class ImagesController : Controller
{
    private IImage imagesService;
    private IWebHostEnvironment webHostEnvironment;
    private IConfiguration configuration;
    public ImagesController(IImage _imagesService, IWebHostEnvironment _webHostEnvironment, IConfiguration _configuration)
    {
        imagesService = _imagesService;
        webHostEnvironment = _webHostEnvironment;
        configuration = _configuration;
    }

    [Produces("application/json")]
    [HttpGet("ImgId/{id}")]
    public IActionResult ImgId(int id)
    {
        try
        {
            var find = imagesService.Img(id);
            return Ok(find);
        }
        catch (Exception ex)
        {
            // ma 400: la co loi roi
            return BadRequest(ex);
        }
    }
}
