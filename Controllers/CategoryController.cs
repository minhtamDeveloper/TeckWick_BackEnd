using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PlantNestBackEnd.Helplers;
using PlantNestBackEnd.Models;
using PlantNestBackEnd.Services;

namespace PlantNestBackEnd.Controllers;
[Route("api/category")]
[ApiController]
public class CategoryController : ControllerBase
{
    private DatabaseContext db;
    private readonly DatabaseContext _databaseContext;
    private IWebHostEnvironment _webHostEnvironment;
    private IConfiguration configuration;
    public CategoryController(DatabaseContext _db, IWebHostEnvironment _webHostEnvironment, IConfiguration _configuration, DatabaseContext databaseContext)
    {
        db = _db;
        this._webHostEnvironment = _webHostEnvironment;
        configuration = _configuration;
        _databaseContext = databaseContext;
    }

    // show theo thằng cha
    [Produces("application/json")]
    [Consumes("application/json")]
    [HttpGet("showCategoryFather")]
    public async Task<IActionResult> ShowFather()
    {
        var result = await db.Categories.AsNoTracking()
            .Where(c => c.CategoryNavigation == null)
            .Select(a => new
            {
                a.Id,
                a.CategoryId,
                a.CategoryImage,
                a.CategoryName,
                a.Created,
                a.Status,
            })
            .OrderByDescending(a => a.Id)
            .ToListAsync();

        return Ok(result);
    }

    [Produces("application/json")]
    [Consumes("application/json")]
    [HttpGet("showCategorySon")]
    public async Task<IActionResult> ShowSon()
    {
        var result = await db.Categories.AsNoTracking()
            .Where(c => c.CategoryNavigation != null)
            .Select(a => new
            {
                a.Id,
                a.CategoryId,
                a.CategoryImage,
                a.CategoryName,
                a.Created,
                a.Status,
            })
            .OrderByDescending(a => a.Id)
            .ToListAsync();

        return Ok(result);
    }

    [Produces("application/json")]
    [Consumes("application/json")]
    [HttpGet("selectByCategory/{id}")]
    public async Task<IActionResult> SelectByCategory(int id)
    {
        var result = await db.Categories.AsNoTracking()
            .Where(c =>
                (id == 0 && c.CategoryNavigation != null) ||  // Lọc theo điều kiện mới
                (id != 0 && c.CategoryNavigation != null && c.CategoryId == id))  // Lọc theo categoryId nếu khác 0
            .Select(a => new
            {
                a.Id,
                a.CategoryId,
                a.CategoryImage,
                a.CategoryName,
                a.Created,
                a.Status,
            })
            .OrderByDescending(a => a.Id)
            .ToListAsync();

        return Ok(result);
    }


    [HttpGet("getAll")]
    public async Task<IActionResult> getAll()
    {



        var a = await _databaseContext.Categories.AsNoTracking().Select(a => new
        {
            a.Id,
            a.CategoryId,
            a.CategoryImage,
            a.CategoryName,
            a.Created,
            a.Status,


        }).ToListAsync();

        return Ok(a);
    }



    [HttpGet("getById/{id}")]
    public async Task<IActionResult> getById(int id)
    {
        var a = await _databaseContext.Categories.AsNoTracking().Where(b => b.Id == id).Select(a => new
        {
            a.Id,
            a.CategoryId,
            a.CategoryImage,
            a.CategoryName,
            a.Created,
            a.Status,


        }).FirstOrDefaultAsync();
        if (a == null) return NotFound("Not Found Category!");




        return Ok(a);
    }
    [HttpPost("create")]
    [Consumes("multipart/form-data")]
    [Produces("application/json")]
    public async Task<IActionResult> create([FromForm] string data, [FromForm] IFormFile? file)
    {

        try
        {
            var category = JsonConvert.DeserializeObject<Category>(data);
            if (category == null) return NotFound("Can  not  Get  Category!");
            if (file != null)
            {
                var fileName = RandomHelper.RandomString(10);
                fileName = Path.Combine("category", fileName + Path.GetExtension(file.FileName));

                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, fileName);
                using var fileStream = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(fileStream);


                category.CategoryImage = fileName;


            }
            category.Status = true;
            category.Created = DateTime.Now;
            await _databaseContext.Categories.AddAsync(category);

            return (await _databaseContext.SaveChangesAsync() > 0) ? Ok() : NotFound("Name is exsist!");
        }
        catch (Exception)
        {

            return NotFound("Can not get Category");
        }



    }
    [HttpPut("update/{id}")]
    [Consumes("multipart/form-data")]
    [Produces("application/json")]
    public async Task<IActionResult> update(int id, [FromForm] string data, [FromForm] IFormFile? file)
    {

        try
        {
            var category = JsonConvert.DeserializeObject<Category>(data);
            var dbCategory = await _databaseContext.Categories.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (dbCategory == null) return NotFound("Can  not  Find  Category!");
            if (category == null) return NotFound("Can  not  Get  Category!");



            if (file != null)
            {

                //Nếu có đường dẫn thì xóa 
                if (dbCategory.CategoryImage != null)
                {
                    var filePath1 = Path.Combine(_webHostEnvironment.WebRootPath, dbCategory.CategoryImage);
                    using var stream = new FileStream(filePath1, FileMode.Open);


                    stream.Dispose();
                    try
                    {
                        System.IO.File.Delete(filePath1);
                    }
                    catch (Exception)
                    {


                    }

                }
                var fileName = RandomHelper.RandomString(10);
                fileName = Path.Combine("category", fileName + Path.GetExtension(file.FileName));

                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, fileName);
                using var fileStream = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(fileStream);


                //Nếu có file mói thì thêm đường dẫn mới vào 
                dbCategory.CategoryImage = fileName;


            }






            dbCategory.CategoryName = category.CategoryName;
            dbCategory.CategoryId = category.CategoryId;
            _databaseContext.Categories.Update(dbCategory);

            return (await _databaseContext.SaveChangesAsync() > 0) ? Ok() : NotFound("Name is exsist!");
        }
        catch (Exception)
        {

            return NotFound("Can not get Category");
        }



    }
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> delete(int id)
    {
        try
        {
            var a = await _databaseContext.Categories.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (a == null) return NotFound("Can not find Category");
            a.Status = false;
            _databaseContext.Categories.Update(a);




            return (await _databaseContext.SaveChangesAsync() > 0) ? Ok() : NotFound("Can Not Delete!");
        }
        catch (Exception)
        {

            return NotFound("Something wrong");
        }

    }
    [HttpDelete("enable/{id}")]
    public async Task<IActionResult> enable(int id)
    {
        try
        {
            var a = await _databaseContext.Categories.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (a == null) return NotFound("Can not find Category");
            a.Status = true;
            _databaseContext.Categories.Update(a);




            return (await _databaseContext.SaveChangesAsync() > 0) ? Ok() : NotFound("Can Not Delete!");
        }
        catch (Exception)
        {

            return NotFound("Something wrong");
        }

    }



    //public string GenerateRandomString(int length)
    //{
    //    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    //    var random = new Random();
    //    return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
    //}

}
