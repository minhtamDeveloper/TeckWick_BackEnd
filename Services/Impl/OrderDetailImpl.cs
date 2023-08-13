using PlantNestBackEnd.Models;
using System.Diagnostics;

namespace PlantNestBackEnd.Services.Impl
{
    public class OrderDetailImpl : IOrderDetail
    {
        private DatabaseContext db;
        private IConfiguration configuration;
        public OrderDetailImpl(DatabaseContext db, IConfiguration _configuration) { this.db = db; configuration = _configuration; }
        public async Task<dynamic> findByIdOrder(int idOrder)
        {
            try
            {
                return db.OrderDetails.Where(p => p.OrderId == idOrder).Select(p => new
                {
                  OrderId = p.OrderId,
                  ProductId = p.ProductId,
                  Quantity = p.Quantity,
                  CommentId = p.CommentId,
                  TotalPrice = p.TotalPrice,
                  Created = p.Created,
                  ProductName = p.Product.ProductName,
                  ProductPrice = p.Product.SellPrice,
                  imageUrls = db.Images
                  .Where(image => image.ProductId == p.ProductId)
                  .OrderByDescending(image => image.Id)
                  .Select(image => configuration["BaseUrl"] + "images/" + image.ImageUrl)
                  .FirstOrDefault()
                }).OrderByDescending(p => p.OrderId).ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }
        public async Task<dynamic> findAll()
        {
            try
            {
                return db.OrderDetails.Select(p => new
                {
                    OrderId = p.OrderId,
                    ProductId = p.ProductId,
                    Quantity = p.Quantity,
                    CommentId = p.CommentId,
                    TotalPrice = p.TotalPrice,
                    Created = p.Created,
                    ProductName = p.Product.ProductName,
                    ProductPrice = p.Product.SellPrice,
                    imageUrls = db.Images
                    .Where(image => image.ProductId == p.ProductId)
                    .OrderByDescending(image => image.Id)
                    .Select(image => configuration["BaseUrl"] + "images/" + image.ImageUrl)
                    .FirstOrDefault()
            }).OrderByDescending(p => p.OrderId).ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }
        public bool created(List<OrderDetail> orderDetails)
        {
            try
            {
                db.OrderDetails.AddRange(orderDetails);
                return db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating order details: " + ex.Message);
            }
        }
    }
}
