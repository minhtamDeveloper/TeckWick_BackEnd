using PlantNestBackEnd.Models;

namespace PlantNestBackEnd.Services.Impl;

public class OrderImpl : IOrder
{
    private DatabaseContext db;
    private IConfiguration configuration;
    public OrderImpl(DatabaseContext _db, IConfiguration _configuration)
    {
        db = _db;
        configuration = _configuration;
    }

    public bool created(Order order)
    {
        try
        {
            db.Orders.Add(order);
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }

    public dynamic findAll()
    {
        return db.Orders.Select(p => new
        {
            OrderId = p.Id,
            AccountId = p.AccountId,
            PaymentMethod = p.PaymentMethod,
            TotalOrder = p.TotalOrder,
            OrderDate = p.OrderDate,
            OrderTime = p.OrderTime,
            Status = p.Status,
            Username = p.Account!.Username
        }).OrderByDescending(p => p.OrderId).ToList();
    }

    public dynamic findByAccountId(int accountId)
    {
        return db.Orders
       .Where(p => p.Status == "0" && p.AccountId == accountId)
       .Select(p => new
       {
           OrderId = p.Id,
           AccountId = p.AccountId,
           PaymentMethod = p.PaymentMethod,
           TotalOrder = p.TotalOrder,
           OrderDate = p.OrderDate,
           OrderTime = p.OrderTime,
           Status = p.Status,
           Username = p.Account!.Username,
           OrderDetails = p.OrderDetails.Select(od => new
           {
               OrderId = od.OrderId,
               ProductId = od.ProductId,
               ProductName = od.Product.ProductName,
               Quantity = od.Quantity,
               TotalPrice = od.TotalPrice,
               ImageUrl = configuration["BaseUrl"] + "images/" + db.Images
                    .Where(image => image.ProductId == od.ProductId)
                    .OrderByDescending(image => image.Id)
                    .Select(image => image.ImageUrl)
                    .FirstOrDefault()
           }).ToList()
       })
       .OrderByDescending(p => p.OrderId)
       .ToList();
    }

    public dynamic findByAccountId2(int accountId)
    {
        return db.Orders
     .Where(p => p.Status == "1" && p.AccountId == accountId)
     .Select(p => new
     {
         OrderId = p.Id,
         AccountId = p.AccountId,
         PaymentMethod = p.PaymentMethod,
         TotalOrder = p.TotalOrder,
         OrderDate = p.OrderDate,
         OrderTime = p.OrderTime,

         Status = p.Status,
         Username = p.Account!.Username,
         OrderDetails = p.OrderDetails.Select(od => new
         {
             OrderId = od.OrderId,
             ProductId = od.ProductId,
             ProductName = od.Product.ProductName,
             Quantity = od.Quantity,
             TotalPrice = od.TotalPrice,
             ImageUrl = configuration["BaseUrl"] + "images/" + db.Images
                    .Where(image => image.ProductId == od.ProductId)
                    .OrderByDescending(image => image.Id)
                    .Select(image => image.ImageUrl)
                    .FirstOrDefault()
         }).ToList()
     })

     .OrderByDescending(p => p.OrderId)
     .ToList();
    }

    public dynamic findByAccountId3(int accountId)
    {
        return db.Orders
     .Where(p => p.Status == "2" && p.AccountId == accountId)
     .Select(p => new
     {
         OrderId = p.Id,
         AccountId = p.AccountId,
         PaymentMethod = p.PaymentMethod,
         TotalOrder = p.TotalOrder,
         OrderDate = p.OrderDate,
         OrderTime = p.OrderTime,

         Status = p.Status,
         Username = p.Account!.Username,
         OrderDetails = p.OrderDetails.Select(od => new
         {
             OrderId = od.OrderId,
             ProductId = od.ProductId,
             ProductName = od.Product.ProductName,
             Quantity = od.Quantity,
             TotalPrice = od.TotalPrice,
             ImageUrl = configuration["BaseUrl"] + "images/" + db.Images
                    .Where(image => image.ProductId == od.ProductId)
                    .OrderByDescending(image => image.Id)
                    .Select(image => image.ImageUrl)
                    .FirstOrDefault()
         }).ToList()
     })

     .OrderByDescending(p => p.OrderId)
     .ToList();
    }

    public dynamic findOrderMax()
    {
        var latestOrder = db.Orders
       .OrderByDescending(p => p.Id)
       .Select(p => new
       {
           OrderId = p.Id,
       })
       .FirstOrDefault();

        return latestOrder!;
    }

    public bool UpdateOrderStatus(int orderId)
    {
        try
        {
            var existingOrder = db.Orders.FirstOrDefault(o => o.Id == orderId);
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

    public bool UpdateOrderStatus2(int orderId)
    {
        try
        {
            var existingOrder = db.Orders.FirstOrDefault(o => o.Id == orderId);
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
