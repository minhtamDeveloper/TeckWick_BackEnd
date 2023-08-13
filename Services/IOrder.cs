using PlantNestBackEnd.Models;

namespace PlantNestBackEnd.Services
{
    public interface IOrder
    {
        
        public bool created(Order order);
        public dynamic findAll();
        public dynamic findOrderMax();
        public dynamic findByAccountId(int accountId);
        public dynamic findByAccountId2(int accountId);
        public dynamic findByAccountId3(int accountId);
        public bool UpdateOrderStatus(int orderId);
        public bool UpdateOrderStatus2(int orderId);

    }
}
