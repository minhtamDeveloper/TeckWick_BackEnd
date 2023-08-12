
using Microsoft.AspNetCore.Mvc;
using PlantNestBackEnd.Models;
using PlantNestBackEnd.Services;

namespace PlantNestBackEnd.Controllers;
[Route("api/cart")]
[ApiController]
public class CartController : ControllerBase
{
    private DatabaseContext db;
    private IWebHostEnvironment webHostEnvironment;
    private IConfiguration configuration;
    private ICarts cartService;
    public CartController(IWebHostEnvironment _webHostEnvironment, IConfiguration _configuration, ICarts _cartService, DatabaseContext _db)
    {
        webHostEnvironment = _webHostEnvironment;
        configuration = _configuration;
        cartService = _cartService;
        db = _db;
    }

    // show tất cả list cart
    [Produces("application/json")]
    [Consumes("application/json")]
    [HttpGet("findAll")]
    public IActionResult ShowAll()
    {
        try
        {
            var fillAll = cartService.findAllCart();
            return Ok(fillAll);
        }
        catch (Exception ex)
        {
            // ma 400: la co loi roi
            return BadRequest(ex);
        }
    }

    //Thêm Cart
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost("addCart")]
    public IActionResult AddCart([FromBody] Cart _Cart)
    {
        try
        {
            var existingCart = db.Carts
                .FirstOrDefault(cart => cart.ProductId == _Cart.ProductId && cart.AccountId == _Cart.AccountId);

            if (existingCart != null)
            {
                if (_Cart.Quantity > 1)
                {
                    existingCart.Quantity += _Cart.Quantity;
                }
                else
                {
                    existingCart.Quantity += 1;
                }
                db.SaveChanges(); // Lưu thay đổi quantity
            }
            else
            {
                db.Carts.Add(_Cart);
                db.SaveChanges(); // Thêm mới bản ghi
            }

            return Ok(new
            {
                status = "Cart updated successfully."
            });
        }
        catch
        {
            return BadRequest();
        }
    }


}
