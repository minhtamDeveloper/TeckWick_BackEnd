using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using PlantNestBackEnd.Models;
using PlantNestBackEnd.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PlantNestBackEnd.Controllers;
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private IConfiguration configuration;
    private IUser userService;
    public LoginController(IConfiguration configuration, IUser userService) { 
        this.configuration = configuration;
        this.userService= userService;
    }

    [Produces("application/json")]
    [Consumes("multipart/form-data")]
    [HttpPost("login")]
    public  IActionResult login(string data)
    {
        var dataClient = JsonConvert.DeserializeObject<Account>(data);

        try
        {

            if (userService.login(dataClient.Username, dataClient.Password))
            {
                var dataReturn = userService.dataLoginSuccessful(dataClient.Username);
                return Ok(CreateToken(dataReturn.Id.ToString(), dataReturn.Username, dataReturn.Role.RoleName));
            }
            else
            {
                return BadRequest("Login Fail");
            }
        }catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
        
    }
    private string CreateToken(string id,string name, string role)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier,id),
            new Claim(ClaimTypes.Name,name),
            new Claim(ClaimTypes.Role,role)
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:Token").Value!));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var token = new JwtSecurityToken(
            claims:claims,
            expires:DateTime.Now.AddDays(1),
            signingCredentials: cred
            );
        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }
}
