using Microsoft.EntityFrameworkCore;
using PlantNestBackEnd.Models;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PlantNestBackEnd.Services.Impl;

public class ProductImpl : IProduct
{
    private DatabaseContext db;
    private IConfiguration configuration;
    public ProductImpl(DatabaseContext _db, IConfiguration _configuration)
    {
        db = _db;
        configuration = _configuration;
    }
    public dynamic Search(string keyword)
    {
        return db.Products.Where(p => p.ProductName.Contains(keyword)).Select(p => new
        {
            productName = p.ProductName,
            quantity = p.Quantity,
            costPrice = p.CostPrice,
            sellPrice = p.SellPrice,
            status = p.Status,
            imageUrl = configuration["BaseUrl"] + p.Images

        }).ToList();
    }

    public dynamic SearchId(int id)
    {
        var data = db.Products
     .OrderByDescending(product => product.Id)
     .Where(product => product.Id == id)
     .Select(product => new
     {
        id = product.Id,
         productName = product.ProductName,
         description = product.Description,
         costPrice = product.CostPrice,
         sellPrice = product.SellPrice,
         quantity = product.Quantity,
         createdDate = product.CreatedDate,
         categoryId = product.CategoryId,
         categoryName = product.Category!.CategoryName + " - " + product.Category.CategoryNavigation!.CategoryName,
         supplierId = product.SupplierId,
         supplierName = product.Supplier!.SupplierName,
         status = product.Status,
         imageUrl = configuration["BaseUrl"]  + db.Images
             .Where(image => image.ProductId == product.Id && image.ImageUrl.Contains("index"))
             .OrderByDescending(image => image.Id)
             .Select(image => image.ImageUrl)
             .FirstOrDefault()
     });

        return data.FirstOrDefault()!;
    }



    public dynamic showAll()
    {

        var data = db.Products
       .OrderByDescending(product => product.Id)
       .Select(product => new
       {
           id = product.Id,
           productName = product.ProductName,
           description = product.Description,
           costPrice = product.CostPrice,
           sellPrice = product.SellPrice,
           quantity = product.Quantity,
           createdDate = product.CreatedDate,
           categoryId = product.CategoryId,
           categoryName = product.Category!.CategoryName + " - " + product.Category.CategoryNavigation!.CategoryName,
           supplierId = product.SupplierId,
           supplierName = product.Supplier!.SupplierName,
           status = product.Status,
           imageUrl = configuration["BaseUrl"] + db.Images
               .Where(image => image.ProductId == product.Id && image.ImageUrl.Contains("index"))
               .OrderByDescending(image => image.Id)
               .Select(image => image.ImageUrl)
               .FirstOrDefault()
       });

        return data.ToList();
    }

    public dynamic showNewProduct()
    {
      var data = db.Products
     .Where(product => product.Status == true)
     .OrderByDescending(product => product.Id)
     .Take(8)
     .Select(product => new
     {
         id = product.Id,
         productName = product.ProductName,
         description = product.Description,
         costPrice = product.CostPrice,
         sellPrice = product.SellPrice,
         quantity = product.Quantity,
         createdDate = product.CreatedDate,
         categoryId = product.CategoryId,
         categoryName = product.Category!.CategoryName + " - " + product.Category.CategoryNavigation!.CategoryName,
         supplierId = product.SupplierId,
         supplierName = product.Supplier!.SupplierName,
         status = product.Status,
         imageUrl = configuration["BaseUrl"] + db.Images
             .Where(image => image.ProductId == product.Id && image.ImageUrl.Contains("index"))
             .OrderByDescending(image => image.Id)
             .Select(image => image.ImageUrl)
             .FirstOrDefault()
     });

        return data.ToList();
    }

    public dynamic showBestSellersProduct()
    {
        var productQuantities = db.OrderDetails
       .GroupBy(orderDetail => orderDetail.ProductId)
       .Select(group => new
       {
           productId = group.Key,
           totalQuantity = group.Sum(orderDetail => orderDetail.Quantity)
       })
       .OrderByDescending(group => group.totalQuantity)
       .Take(4)
       .ToList();

        var productIds = productQuantities.Select(item => item.productId).ToList();

        var data = db.Products
            .Where(product => product.Status == true && productIds.Contains(product.Id))
            .Select(product => new
            {
                id = product.Id,
                productName = product.ProductName,
                description = product.Description,
                costPrice = product.CostPrice,
                sellPrice = product.SellPrice,
                quantity = product.Quantity,
                createdDate = product.CreatedDate,
                categoryId = product.CategoryId,
                categoryName = product.Category!.CategoryName + " - " + product.Category.CategoryNavigation!.CategoryName,
                supplierId = product.SupplierId,
                supplierName = product.Supplier!.SupplierName,
                status = product.Status,
                imageUrl = configuration["BaseUrl"] + db.Images
                    .Where(image => image.ProductId == product.Id && image.ImageUrl.Contains("index"))
                    .OrderByDescending(image => image.Id)
                    .Select(image => image.ImageUrl)
                    .FirstOrDefault()
            })
            .ToList();

        return data;
    }

    public dynamic findProductById(int id)
    {
        var data = db.Products
      .Where(product => product.Id == id)
      .Select(product => new
      {
          id = product.Id,
          productName = product.ProductName,
          description = product.Description,
          costPrice = product.CostPrice,
          sellPrice = product.SellPrice,
          quantity = product.Quantity,
          createdDate = product.CreatedDate,
          categoryId = product.CategoryId,
          categoryName = product.Category!.CategoryName + " - " + product.Category.CategoryNavigation!.CategoryName,
          supplierId = product.SupplierId,
          supplierName = product.Supplier!.SupplierName,
          status = product.Status,
          imageUrl = configuration["BaseUrl"]  + db.Images
              .Where(image => image.ProductId == product.Id && image.ImageUrl.Contains("index"))
              .OrderByDescending(image => image.Id)
              .Select(image => image.ImageUrl)
              .FirstOrDefault()
      });

        return data.FirstOrDefault()!;
    }

    public dynamic findProductByCategoryId(int categoryId)
    {
        var data = db.Products
     .Where(product => product.CategoryId == categoryId)
     .Take(4)
     .Select(product => new
     {
         id = product.Id,
         productName = product.ProductName,
         description = product.Description,
         costPrice = product.CostPrice,
         sellPrice = product.SellPrice,
         quantity = product.Quantity,
         createdDate = product.CreatedDate,
         categoryId = product.CategoryId,
         categoryName = product.Category!.CategoryName + " - " + product.Category.CategoryNavigation!.CategoryName,
         supplierId = product.SupplierId,
         supplierName = product.Supplier!.SupplierName,
         status = product.Status,
         imageUrl = configuration["BaseUrl"]  + db.Images
             .Where(image => image.ProductId == product.Id && image.ImageUrl.Contains("index"))
             .OrderByDescending(image => image.Id)
             .Select(image => image.ImageUrl)
             .FirstOrDefault()
     });

        return data.ToList();
    }

    public dynamic findProductByCategoryId2(int categoryId)
    {
        var data = db.Products
     .Where(product => product.CategoryId == categoryId)
     .Select(product => new
     {
         id = product.Id,
         productName = product.ProductName,
         description = product.Description,
         costPrice = product.CostPrice,
         sellPrice = product.SellPrice,
         quantity = product.Quantity,
         createdDate = product.CreatedDate,
         categoryId = product.CategoryId,
         categoryName = product.Category!.CategoryName + " - " + product.Category.CategoryNavigation!.CategoryName,
         supplierId = product.SupplierId,
         supplierName = product.Supplier!.SupplierName,
         status = product.Status,
         imageUrl = configuration["BaseUrl"]  + db.Images
             .Where(image => image.ProductId == product.Id && image.ImageUrl.Contains("index"))
             .OrderByDescending(image => image.Id)
             .Select(image => image.ImageUrl)
             .FirstOrDefault()
     });

        return data.ToList();
    }

    public dynamic findAllProductsOrderedByFirstLetterZA()
    {
        var data = db.Products
            .OrderByDescending(product => product.ProductName.Substring(0, 1)) // Sắp xếp theo chữ cái đầu tiên từ Z-A
            .ThenBy(product => product.ProductName) // Sắp xếp cùng chữ cái theo thứ tự tên sản phẩm
            .Select(product => new
            {
                id = product.Id,
                productName = product.ProductName,
                description = product.Description,
                costPrice = product.CostPrice,
                sellPrice = product.SellPrice,
                quantity = product.Quantity,
                createdDate = product.CreatedDate,
                categoryId = product.CategoryId,
                categoryName = product.Category!.CategoryName + " - " + product.Category.CategoryNavigation!.CategoryName,
                supplierId = product.SupplierId,
                supplierName = product.Supplier!.SupplierName,
                status = product.Status,
                imageUrl = configuration["BaseUrl"] + db.Images
                    .Where(image => image.ProductId == product.Id && image.ImageUrl.Contains("index"))
                    .OrderByDescending(image => image.Id)
                    .Select(image => image.ImageUrl)
                    .FirstOrDefault()
            });

        return data.ToList();
    }



    public dynamic findAllProductsOrderedByFirstLetterAZ()
    {
        var data = db.Products
            .OrderBy(product => product.ProductName.Substring(0, 1)) // Sắp xếp theo chữ cái đầu tiên từ A-Z
            .ThenBy(product => product.ProductName) // Sắp xếp cùng chữ cái theo thứ tự tên sản phẩm
            .Select(product => new
            {
                id = product.Id,
                productName = product.ProductName,
                description = product.Description,
                costPrice = product.CostPrice,
                sellPrice = product.SellPrice,
                quantity = product.Quantity,
                createdDate = product.CreatedDate,
                categoryId = product.CategoryId,
                categoryName = product.Category!.CategoryName + " - " + product.Category.CategoryNavigation!.CategoryName,
                supplierId = product.SupplierId,
                supplierName = product.Supplier!.SupplierName,
                status = product.Status,
                imageUrl = configuration["BaseUrl"] + db.Images
                    .Where(image => image.ProductId == product.Id && image.ImageUrl.Contains("index"))
                    .OrderByDescending(image => image.Id)
                    .Select(image => image.ImageUrl)
                    .FirstOrDefault()
            });

        return data.ToList();
    }


    public dynamic findAllProductsOrderedByPriceDescending()
    {
        var data = db.Products
            .OrderByDescending(product => product.SellPrice) // Sắp xếp theo giá giảm dần
            .Select(product => new
            {
                id = product.Id,
                productName = product.ProductName,
                description = product.Description,
                costPrice = product.CostPrice,
                sellPrice = product.SellPrice,
                quantity = product.Quantity,
                createdDate = product.CreatedDate,
                categoryId = product.CategoryId,
                categoryName = product.Category!.CategoryName + " - " + product.Category.CategoryNavigation!.CategoryName,
                supplierId = product.SupplierId,
                supplierName = product.Supplier!.SupplierName,
                status = product.Status,
                imageUrl = configuration["BaseUrl"]  + db.Images
                    .Where(image => image.ProductId == product.Id && image.ImageUrl.Contains("index"))
                    .OrderByDescending(image => image.Id)
                    .Select(image => image.ImageUrl)
                    .FirstOrDefault()
            });

        return data.ToList();
    }



    public dynamic findAllProductsOrderedByPriceAscending()
    {
        var data = db.Products
            .OrderBy(product => product.SellPrice) // Sắp xếp theo giá tăng dần
            .Select(product => new
            {
                id = product.Id,
                productName = product.ProductName,
                description = product.Description,
                costPrice = product.CostPrice,
                sellPrice = product.SellPrice,
                quantity = product.Quantity,
                createdDate = product.CreatedDate,
                categoryId = product.CategoryId,
                categoryName = product.Category!.CategoryName + " - " + product.Category.CategoryNavigation!.CategoryName,
                supplierId = product.SupplierId,
                supplierName = product.Supplier!.SupplierName,
                status = product.Status,
                imageUrl = configuration["BaseUrl"]  + db.Images
                    .Where(image => image.ProductId == product.Id && image.ImageUrl.Contains("index"))
                    .OrderByDescending(image => image.Id)
                    .Select(image => image.ImageUrl)
                    .FirstOrDefault()
            });

        return data.ToList();
    }

}
