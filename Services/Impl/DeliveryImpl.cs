using PlantNestBackEnd.Models;
using System.Diagnostics;

namespace PlantNestBackEnd.Services.Impl
{
    public class DeliveryImpl : IDelivery
    {
        private DatabaseContext db;
        private IConfiguration configuration;
        public DeliveryImpl(DatabaseContext db, IConfiguration _configuration) { this.db = db; configuration = _configuration; }
        public async Task<dynamic> findByIdOrder(int idOrder)
        {
            try
            {
                return db.Deliveries.Where(p => p.OrderId == idOrder).Select(p => new
                {
                    Id = p.OrderId,
                    OrderId = p.OrderId,
                    DeliveryDate = p.DeliveryDate,
                    ReceivingDate = p.ReceivingDate,
                    RecipientName =     p.RecipientName,
                    RecipientAddress =  p.RecipientAddress,
                    RecipientPhone  = p.RecipientPhone,
                    Message =   p.Message,
                    Status  = p.Status,

                }).OrderByDescending(p => p.OrderId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }

    }
}
