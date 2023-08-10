using Microsoft.EntityFrameworkCore;
using PlantNestBackEnd.Models;
using System.Diagnostics;

namespace PlantNestBackEnd.Services.Impl;

public class ProductImpl : IProduct
{
    private DatabaseContext db;
    public ProductImpl(DatabaseContext db) { this.db = db; }
    public async Task<bool> create(Product product)
    {
        try {
            db.Products.Add(product);
            return (await db.SaveChangesAsync()) >0;
        }catch(Exception ex) { 
        Debug.WriteLine(ex);
            return false;
        }
    }

    public async Task<bool> delete(int id)
    {
        try
        {
            db.Products.Remove(db.Products.Find(id));
            return (await db.SaveChangesAsync()) > 0;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return false;
        }
    }

    public async Task<bool> exist(int id)
    {
        try
        {
            return (await db.Products.Where(p=>p.Id ==id).CountAsync()) > 0;
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
            return await db.Products.Select(p => new
            {
                id = p.Id,
                productName = p.ProductName,
                

            }).ToListAsync();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return false;
        }
    }

    public async Task<dynamic> findByID(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> update(Product product)
    {
        try
        {
            db.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return (await db.SaveChangesAsync()) > 0;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return false;
        }
    }
}
