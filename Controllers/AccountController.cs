using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlantNestBackEnd.Services;

namespace PlantNestBackEnd.Controllers;
[Route("api/account")]
[ApiController]
public class AccountController : ControllerBase
{
    private IAccount accountService;
    private IWebHostEnvironment webHostEnvironment;
    private IConfiguration configuration;
    public AccountController(IAccount _accountService, IWebHostEnvironment _webHostEnvironment, IConfiguration _configuration)
    {
        accountService = _accountService;
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
            var rs = accountService.showAll();
            return Ok(rs);
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
            var find = accountService.Search(keyword);
            return Ok(find);
        }
        catch (Exception ex)
        {
            // ma 400: la co loi roi
            return BadRequest(ex);
        }
    }
}
