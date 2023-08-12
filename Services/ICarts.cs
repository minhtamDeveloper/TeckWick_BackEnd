using PlantNestBackEnd.Models;

namespace PlantNestBackEnd.Services;

public interface ICarts
{
    //Hiển thị danh sách toàn bộ Cart
    public dynamic findAllCart();

    public dynamic findByAccountId(int id);
    //Thêm(add) Cart
    public bool addCart(Cart Cart);
    //Cập nhật(update) Cart
    public bool updateCart(Cart Cart);
}
