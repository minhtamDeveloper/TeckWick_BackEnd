using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PlantNestBackEnd.Helpers;
using PlantNestBackEnd.Models;
using PlantNestBackEnd.Services;

namespace PlantNestBackEnd.Controllers;
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private IConfiguration configuration;
    private IUser userService;
    private IWebHostEnvironment webHostEnvironment;
    public UserController(IConfiguration configuration, IUser userService, IWebHostEnvironment webHostEnvironment)
    {
        this.configuration = configuration;
        this.userService = userService;
        this.webHostEnvironment = webHostEnvironment;
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
                account.AccountImage = "user.jpg";
                account.Password = hashpassword;
                account.Created = DateTime.Now;
                account.Status = true;
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



    [Produces("application/json")]
    [Consumes("multipart/form-data")]
    [HttpPut("update/{id}")]
    public IActionResult update(int id,string strAccount)
    {
        var account = JsonConvert.DeserializeObject<Account>(strAccount, new IsoDateTimeConverter
        {
            DateTimeFormat = "dd/MM/yyyy"
        });
        try
        {
            if (userService.existUserName(account.Username))
            {
                return BadRequest("Username Already Exists");
            }
            else { 
                if (userService.update(id,account))
                {
                    return Ok(true);
                }
                else
                {
                    return BadRequest("Email Already Exist");
                }
            }


        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Produces("application/json")]
    [Consumes("multipart/form-data")]
    [HttpPut("update-img/{id}")]
    public IActionResult updateAvater(int id, IFormFile file)
    {
        //var account = JsonConvert.DeserializeObject<Account>(strAccount, new IsoDateTimeConverter
        //{
        //    DateTimeFormat = "dd/MM/yyyy"
        //});
        try
        {
           

            var fileName = FileHelper.generateFileName(file.FileName);
            var path = Path.Combine(webHostEnvironment.WebRootPath, "img-profile", fileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }
            return Ok(userService.updateAvt(id,fileName));



        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }


    }

    [Produces("application/json")]
    [Consumes("multipart/form-data")]
    [HttpPost("send-code")]
    public IActionResult sendCode(string strAccount)
    {
        var data = JsonConvert.DeserializeObject<Account>(strAccount);
        try
        {
            if (userService.sendCode(data.Email))
            {
                return Ok(true);
            }
            else
            {
                return BadRequest("Email does not exist.");
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }


    }

    [Produces("application/json")]
    [Consumes("multipart/form-data")]
    [HttpPost("verify")]
    public IActionResult Verify(string strAccount)
    {
        var account = JsonConvert.DeserializeObject<Account>(strAccount);
        try
        {
            if (userService.verify(account.Email,account.SercurityCode))
            {
                return Ok(true);
            }
            else
            {
                return BadRequest("Invalid OTP Code.");
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }


    }


    [Produces("application/json")]
    [Consumes("multipart/form-data")]
    [HttpPost("change-pasword")]
    public IActionResult ChangePassword(string strAccount)
    {
        var account = JsonConvert.DeserializeObject<Account>(strAccount);
        try
        {
            var hashPassword = BCrypt.Net.BCrypt.HashPassword(account.Password);
            if (userService.changePassword(account.Email, hashPassword))
            {
                return Ok(true);
            }
            else
            {
                return BadRequest("Change Password Fail.");
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }


    }
}
