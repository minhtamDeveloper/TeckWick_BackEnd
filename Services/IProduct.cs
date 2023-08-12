using PlantNestBackEnd.Models;

namespace PlantNestBackEnd.Services;

public interface IProduct
{
    public dynamic showAll();

    // api cho user
    public dynamic showNewProduct();
    public dynamic showBestSellersProduct();
    public dynamic findProductById(int id);

    public dynamic findProductByCategoryId(int categoryId);
    public dynamic findProductByCategoryId2(int categoryId);

    public dynamic findAllProductsOrderedByFirstLetterZA();

    public dynamic findAllProductsOrderedByFirstLetterAZ();

    public dynamic findAllProductsOrderedByPriceDescending();

    public dynamic findAllProductsOrderedByPriceAscending();
    //method hiện thị danh sách bó hoa(Bouquet) thoe ngày tạo mới nhất
    
    public dynamic SearchId(int id);

    public dynamic Search(string keyword);

}
