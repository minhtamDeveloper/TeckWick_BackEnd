using Microsoft.EntityFrameworkCore;
using PlantNestBackEnd.Models;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PlantNestBackEnd.Services.Impl;

public class CartImpl : ICarts
{
    private DatabaseContext db;
    private IConfiguration configuration;
    public CartImpl(DatabaseContext _db, IConfiguration _configuration)
    {
        db = _db;
        configuration = _configuration;
    }

    public bool addCart(Cart Cart)
    {
        try
        {
            db.Carts.Add(Cart);
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }

    public bool updateCart(Cart Cart)
    {
        try
        {
            db.Entry(Cart).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }
    public dynamic findAllCart()
    {
        var data = db.Carts
        .OrderByDescending(cart => cart.Id)
        .Select(cart => new
        {
            id = cart.Id,
            accountId = cart.AccountId,
            productId = cart.ProductId,
            quantity = cart.Quantity,
            price = cart.Price,
            productName = db.Products
                .Where(product => product.Id == cart.ProductId)
                .Select(product => product.ProductName)
                .FirstOrDefault(),
            imageUrl = configuration["BaseUrl"] + "images/" + db.Images
                .Where(image => image.ProductId == cart.ProductId)
                .OrderByDescending(image => image.Id)
                .Select(image => image.ImageUrl)
                .FirstOrDefault()
        });

        return data.ToList();
    }

    public dynamic findByAccountId(int id)
    {

        var data = db.Carts
        .OrderByDescending(cart => cart.Id)
        .Where(cart => cart.AccountId == id)
        .Select(cart => new
        {
            id = cart.Id,
            accountId = cart.AccountId,
            productId = cart.ProductId,
            quantity = cart.Quantity,
            price = cart.Price,
            productName = db.Products
                .Where(product => product.Id == cart.ProductId)
                .Select(product => product.ProductName)
                .FirstOrDefault(),
            imageUrl = configuration["BaseUrl"] + "images/" + db.Images
                .Where(image => image.ProductId == cart.ProductId)
                .OrderByDescending(image => image.Id)
                .Select(image => image.ImageUrl)
                .FirstOrDefault()
        });

        return data.ToList();
    }

    public bool DeleteCart(int cartId)
    {
        try
        {
            var carts = db.Carts.Where(be => be.Id == cartId);
            db.Carts.RemoveRange(carts);
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }

    public bool DeleteCartByAccountId(int accountId)
    {
        try
        {
            var carts = db.Carts.Where(be => be.AccountId == accountId);
            db.Carts.RemoveRange(carts);
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }
}