using PlantNestBackEnd.Models;

namespace PlantNestBackEnd.Services
{
    public interface IOrderDetail
    {
        Task<dynamic> findByIdOrder(int idOrder);
        Task<dynamic> findAll();
        public bool created(List<OrderDetail> orderDetails);
    }
}
