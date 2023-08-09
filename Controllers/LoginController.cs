using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PlantNestBackEnd.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PlantNestBackEnd.Controllers;
[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private static Account account = new Account();
    private IConfiguration configuration;
    public LoginController(IConfiguration configuration) { 
        this.configuration = configuration; 
    }

    [Produces("application/json")]
    [Consumes("application/json")]
    [HttpPost("login")]
    public IActionResult login(Account account)
    {
        ;
        return Ok(CreateToken(account.Username, account.Username, account.Username));
    }
    private string CreateToken(string username,string name, string role)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier,username),
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
