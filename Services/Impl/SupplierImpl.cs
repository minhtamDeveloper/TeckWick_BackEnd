using PlantNestBackEnd.Models;
using PlantNestBackEnd.Services;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace PlantNest.Services;

public class SupplierImpl : ISupplier
{
    private DatabaseContext db;
    private IConfiguration configuration;
    public SupplierImpl(DatabaseContext _db, IConfiguration _configuration)
    {
        db = _db;
        configuration = _configuration;
    }
    public dynamic SearchId(int id)
    {
        return db.Suppliers
         .Where(s => s.Id == id)
         .SelectMany(s => s.Products.Select(p => new
         {
             id = s.Id,
             supplierName = s.SupplierName,
             status=s.Status,
             productId = p.Id,
             productName = p.ProductName,
             costPrice=p.CostPrice,
             // Add more properties you want to retrieve
         }))
         .ToList();
    }

    public dynamic showAll()
    {
        return db.Suppliers.OrderByDescending(a => a.Id).Select(a => new
        {
            id = a.Id,
            supplierName = a.SupplierName,
            status = a.Status,
            
        }).ToList();
    }
    public dynamic Search(string keyword)
    {
        return db.Suppliers.Where(a => a.SupplierName.Contains(keyword)).Select(a => new
        {
            id = a.Id,
            supplierName = a.SupplierName,
            status = a.Status,
        }).ToList();
    }
}
