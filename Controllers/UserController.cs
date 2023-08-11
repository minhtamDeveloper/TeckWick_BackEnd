using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PlantNestBackEnd.Models;
using PlantNestBackEnd.Services;

namespace PlantNestBackEnd.Controllers;
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private IConfiguration configuration;
    private IUser userService;
    public UserController(IConfiguration configuration, IUser userService)
    {
        this.configuration = configuration;
        this.userService = userService;
    }

    [Produces("application/json")]
    [Consumes("multipart/form-data")]
    [HttpPost("create")]
    public IActionResult create(string strAccount)
    {
        var account = JsonConvert.DeserializeObject<Account>(strAccount);
        try
        {
            if (userService.existUserName(account.Username))
            {
                return BadRequest("Username Already Exists");
            }
            else if(userService.existEmail(account.Email)) 
            {
                return BadRequest("Email Already Exists");
            }
            else
            {   var hashpassword = BCrypt.Net.BCrypt.HashPassword(account.Password);
                account.Password = hashpassword;
                account.Created = DateTime.Now;
                account.Status = false;
                account.RoleId = 4;
                if (userService.create(account))
                {
                    return Ok(true);
                }
                else
                {
                    return BadRequest();
                }
            }


        }catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Produces("application/json")]
    [Consumes("application/json")]
    [HttpGet("find-by-id/{id}")]
    public IActionResult findById(int id)
    {
        
        try
        {    
                return Ok(userService.findByID(id));            
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
