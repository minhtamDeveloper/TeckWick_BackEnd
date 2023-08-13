using PlantNestBackEnd.Models;

namespace PlantNestBackEnd.Services
{
    public interface IDelivery
    {
        //Task<dynamic> findByIdOrder(int idOrder);
        public bool created(Delivery delivery);
        public dynamic findByOrderId(int orderId);
        public bool UpdateDeliveryStatus(int id);
        public bool UpdateDeliveryStatus2(int id);
    }
}
