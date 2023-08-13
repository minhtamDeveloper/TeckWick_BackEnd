using PlantNestBackEnd.Models;

namespace PlantNestBackEnd.Services;

public interface IFavoriteCart
{
    public dynamic findAllFavoriteCart();
    public bool addFavorteCart(FavoriteCart favoriteCart);

    public dynamic findByAccountId(int accId);
}
