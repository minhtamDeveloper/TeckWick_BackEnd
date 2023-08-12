using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PlantNestBackEnd.Models;
using System.Diagnostics;
using System.IO;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace PlantNestBackEnd.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly DatabaseContext _databaseContext;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public CategoryController(DatabaseContext databaseContext,IWebHostEnvironment webHostEnvironment )
    {
        _webHostEnvironment = webHostEnvironment;
        _databaseContext = databaseContext;
    }
    [HttpGet("getAll")]
    public async Task<IActionResult> getAll()
    {

        

       var a = await _databaseContext.Categories.AsNoTracking().Select(a=>new
        {
            a.Id ,
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
       var a = await _databaseContext.Categories.AsNoTracking().Where(b=>b.Id==id).Select(a=>new
        {
            a.Id ,
            a.CategoryId,
            a.CategoryImage,
            a.CategoryName,
            a.Created,
            a.Status,


        }).FirstOrDefaultAsync();
        if(a==null) return NotFound("Not Found Category!");
     



        return Ok(a);
    }
       [HttpPost("create")]
        [Consumes("multipart/form-data")]	
       [Produces("application/json")]
    public async Task<IActionResult> create([FromForm] string data,[FromForm] IFormFile? file)
    {
      
        try
        {
         var category = JsonConvert.DeserializeObject<Category>(data);
            if(category == null) return NotFound("Can  not  Get  Category!");
              if(file!= null)
                {
                    var  fileName = GenerateRandomString(10);
                        fileName = Path.Combine("category",fileName+Path.GetExtension(file.FileName));
                     
                      var filePath = Path.Combine(_webHostEnvironment.WebRootPath, fileName);
                        using var fileStream = new FileStream(filePath, FileMode.Create);
                        await file.CopyToAsync(fileStream);

                            
                            category.CategoryImage = fileName;
                            
               
                }
             category.Status=true;
            category.Created = DateTime.Now ;
            await _databaseContext.Categories.AddAsync(category);

            return (await _databaseContext.SaveChangesAsync()>0)?Ok():NotFound("Name is exsist!");
        }
        catch (Exception)
        {

           return NotFound("Can not get Category");
        }
       
        
      
    }
       [HttpPut("update/{id}")]
        [Consumes("multipart/form-data")]	
       [Produces("application/json")]
    public async Task<IActionResult> update(int id,[FromForm] string data,[FromForm] IFormFile? file)
    {
      
        try
        {
         var category = JsonConvert.DeserializeObject<Category>(data);
            var dbCategory = await _databaseContext.Categories.Where(a=>a.Id==id).FirstOrDefaultAsync();
            if(dbCategory==null) return NotFound("Can  not  Find  Category!");
            if(category == null) return NotFound("Can  not  Get  Category!");
                
                    

                      if (file != null)
                    {
                     
                        //Nếu có đường dẫn thì xóa 
                        if (dbCategory.CategoryImage != null)
                        {
                            var filePath1 = Path.Combine(_webHostEnvironment.WebRootPath, dbCategory.CategoryImage );
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
                var fileName = GenerateRandomString(10);
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

            return (await _databaseContext.SaveChangesAsync()>0)?Ok():NotFound("Name is exsist!");
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
               var a=  await _databaseContext.Categories.Where(a=>a.Id==id).FirstOrDefaultAsync();
        if (a == null) return NotFound("Can not find Category");
        a.Status = false;
        _databaseContext.Categories.Update(a);
         


       
            return (await _databaseContext.SaveChangesAsync()>0)?Ok():NotFound("Can Not Delete!");
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
               var a=  await _databaseContext.Categories.Where(a=>a.Id==id).FirstOrDefaultAsync();
        if (a == null) return NotFound("Can not find Category");
        a.Status = true;
        _databaseContext.Categories.Update(a);
         


       
            return (await _databaseContext.SaveChangesAsync()>0)?Ok():NotFound("Can Not Delete!");
        }
        catch (Exception)
        {

            return NotFound("Something wrong");
        }
        
        }



      public  string GenerateRandomString(int length)
        {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
         }
    
}
