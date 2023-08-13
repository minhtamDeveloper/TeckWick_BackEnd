
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using PlantNestBackEnd.Models;
using PlantNestBackEnd.Services;

namespace PlantNestBackEnd.Controllers;
[Route("api/favoritecart")]
[ApiController]
public class FavoriteCartController : ControllerBase
{
    private DatabaseContext db;
    private IWebHostEnvironment webHostEnvironment;
    private IConfiguration configuration;
    private IFavoriteCart favoriteCartService;
    public FavoriteCartController(IWebHostEnvironment _webHostEnvironment, IConfiguration _configuration, IFavoriteCart _favoriteCartService, DatabaseContext _db)
    {
        webHostEnvironment = _webHostEnvironment;
        configuration = _configuration;
        favoriteCartService = _favoriteCartService;
        db = _db;
    }


    //Thêm Cart
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost("addFavoriteCart")]
    public IActionResult AddCart([FromBody] FavoriteCart _FavoriteCart)
    {
        try
        {
            var existingFavoriteCart = db.FavoriteCarts
                .FirstOrDefault(favoriteCart => favoriteCart.ProductId == _FavoriteCart.ProductId && favoriteCart.AccountId == _FavoriteCart.AccountId);

            if (existingFavoriteCart != null)
            {
                return BadRequest(new { status = "Product exsit in cart favorite" });
            }
            else
            {
                db.FavoriteCarts.Add(_FavoriteCart);
                db.SaveChanges(); // Thêm mới bản ghi
            }

            return Ok(new
            {
                status = "Add FavoriteCart successfully."
            });
        }
        catch
        {
            return BadRequest();
        }
    }

    // show tất cả list cart
    [Produces("application/json")]
    [Consumes("application/json")]
    [HttpGet("findAll")]
    public IActionResult ShowAll()
    {
        try
        {
            var fillAll = favoriteCartService.findAllFavoriteCart();
            return Ok(fillAll);
        }
        catch (Exception ex)
        {
            // ma 400: la co loi roi
            return BadRequest(ex);
        }
    }

    [Produces("application/json")]
    [Consumes("application/json")]
    [HttpGet("findByAccountId/{accId}")]
    public IActionResult FindByAccountId(int accId)
    {
        try
        {
            var fillAll = favoriteCartService.findByAccountId(accId);
            return Ok(fillAll);
        }
        catch (Exception ex)
        {
            // ma 400: la co loi roi
            return BadRequest(ex);
        }
    }

}
