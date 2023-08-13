using PlantNestBackEnd.Models;

namespace PlantNestBackEnd.Services.Impl;

public class FavoriteCartImpl : IFavoriteCart
{
    private DatabaseContext db;
    private IConfiguration configuration;
    public FavoriteCartImpl(DatabaseContext _db, IConfiguration _configuration)
    {
        db = _db;
        configuration = _configuration;
    }
    public bool addFavorteCart(FavoriteCart favoriteCart)
    {
        try
        {
            db.FavoriteCarts.Add(favoriteCart);
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }

    public dynamic findAllFavoriteCart()
    {
        var data = db.FavoriteCarts
           .OrderByDescending(cart => cart.Id)
           .Select(cart => new
           {
               id = cart.Id,
               accountId = cart.AccountId,
               productId = cart.ProductId,
               productName = db.Products
                   .Where(product => product.Id == cart.ProductId)
                   .Select(product => product.ProductName)
                   .FirstOrDefault(),
               price = db.Products
                   .Where(product => product.Id == cart.ProductId)
                   .Select(product => product.SellPrice)
                   .FirstOrDefault(),

               imageUrl = configuration["BaseUrl"] + "images/" + db.Images
                   .Where(image => image.ProductId == cart.ProductId)
                   .OrderByDescending(image => image.Id)
                   .Select(image => image.ImageUrl)
                   .FirstOrDefault()
           });

        return data.ToList();
    }

    public dynamic findByAccountId(int accId)
    {
        var data = db.FavoriteCarts
           .OrderByDescending(cart => cart.Id)
           .Where(cart => cart.AccountId == accId)
           .Select(cart => new
           {
               id = cart.Id,
               accountId = cart.AccountId,
               productId = cart.ProductId,
               productName = db.Products
                   .Where(product => product.Id == cart.ProductId)
                   .Select(product => product.ProductName)
                   .FirstOrDefault(),
               price = db.Products
                   .Where(product => product.Id == cart.ProductId)
                   .Select(product => product.SellPrice)
                   .FirstOrDefault(),

               imageUrl = configuration["BaseUrl"] + "images/" + db.Images
                   .Where(image => image.ProductId == cart.ProductId)
                   .OrderByDescending(image => image.Id)
                   .Select(image => image.ImageUrl)
                   .FirstOrDefault()
           });

        return data.ToList();
    }
}
