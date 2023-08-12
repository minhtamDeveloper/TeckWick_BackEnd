using Microsoft.Identity.Client;
using PlantNestBackEnd.Models;
using System.Diagnostics;

namespace PlantNestBackEnd.Services.Impl
{
    public class OrderImpl : IOrder
    {
        private DatabaseContext db;
        public OrderImpl(DatabaseContext db) { this.db = db; }
        public async Task<dynamic> findAll()
        {
            try
            {
                return db.Orders.Select(p => new
                {
                    Id = p.Id,
                    AccountId = p.AccountId,
                    PaymentMethod = p.PaymentMethod,
                    TotalOrder  = p.TotalOrder,
                    OrderDate = p.OrderDate,
                    OrderTime = p.OrderTime.Value.ToString("hh':'mm"),
                    Status = p.Status,
                    FullName = p.Account.Username
                }).OrderByDescending(p => p.Id).ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }

    }
}
