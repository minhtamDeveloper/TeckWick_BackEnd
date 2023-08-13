using PlantNestBackEnd.Models;

namespace PlantNestBackEnd.Services.Impl;

public class DeliveryImpl : IDelivery
{
    private DatabaseContext db;
    private IConfiguration configuration;
    public DeliveryImpl(DatabaseContext _db, IConfiguration _configuration)
    {
        db = _db;
        configuration = _configuration;
    }

    public bool created(Delivery delivery)
    {
        try
        {
            db.Deliveries.Add(delivery);
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }

    }

    public dynamic findByOrderId(int orderId)
    {
        return db.Deliveries
            .Where(x => x.OrderId == orderId)
           .Select(p => new
           {
               Id = p.Id,
               OrderId = p.OrderId,
               DeliveryDate = p.DeliveryDate,
               RecipientName = p.RecipientName,
               RecipientAddress = p.RecipientAddress,
               RecipientPhone = p.RecipientPhone,
               Message = p.Message,
               status = p.Status

           }).FirstOrDefault()!;
    }

    public bool UpdateDeliveryStatus(int id)
    {
        try
        {
            var existingOrder = db.Deliveries.FirstOrDefault(o => o.Id == id);
            if (existingOrder != null)
            {
                existingOrder.Status = "1";

                // Save changes to the database
                return db.SaveChanges() > 0;
            }
            else
            {
                // Return false if the order with the specified OrderId does not exist
                return false;
            }
        }
        catch
        {
            return false;
        }
    }

    public bool UpdateDeliveryStatus2(int id)
    {
        try
        {
            var existingOrder = db.Deliveries.FirstOrDefault(o => o.Id == id);
            if (existingOrder != null)
            {
                existingOrder.Status = "2";

                // Save changes to the database
                return db.SaveChanges() > 0;
            }
            else
            {
                // Return false if the order with the specified OrderId does not exist
                return false;
            }
        }
        catch
        {
            return false;
        }
    }
}
