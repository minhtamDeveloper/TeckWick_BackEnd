using Microsoft.EntityFrameworkCore;
using PlantNestBackEnd.Models;
using System.Diagnostics;

namespace PlantNestBackEnd.Services.Impl;

public class CategoryImpl : ICategory
{
    private DatabaseContext db;

    public CategoryImpl(DatabaseContext db)
    {
        this.db = db;
    }

    public async Task<bool> create(Category category)
    {
        try
        {
            db.Categories.Add(category);
            return (await db.SaveChangesAsync()) > 0;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return false;
        }
    }

    public async Task<bool> delete(int id)
    {
        try
        {
            db.Categories.Remove(db.Categories.Find(id));
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
            return (await db.Categories.Where(p => p.Id == id).CountAsync()) > 0;
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
            return await db.Categories.Select(c => new
            {
                id = c.Id,
                categoryName = c.CategoryName,
                categoryImage = c.CategoryImage,
                created = c.Created,
                status = c.Status,
                categoryId = c.CategoryId
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
        try
        {
            return await db.Categories.Where(c=>c.Id == id).Select(c => new
            {
                id = c.Id,
                categoryName = c.CategoryName,
                categoryImage = c.CategoryImage,
                created = c.Created,
                status = c.Status,
                categoryId=c.CategoryId
            }).FirstOrDefaultAsync();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return false;
        }
    }

    public async Task<bool> update(Category category)
    {
        try
        {
            db.Entry(category).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return (await db.SaveChangesAsync()) > 0;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return false;
        }
    }
}
