using Castle.Components.DictionaryAdapter.Xml;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PlantNestBackEnd.Models;
using PlantNestBackEnd.Services;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PlantNestBackEnd.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private IProduct productService;


     private readonly DatabaseContext _databaseContext;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public ProductController(IProduct productService,DatabaseContext databaseContext,IWebHostEnvironment webHostEnvironment) { 
        this.productService = productService; 
        _databaseContext = databaseContext;
        _webHostEnvironment = webHostEnvironment;
    }

    //[Produces("application/json")]
    //[Consumes("multipart/form-data")]
    [HttpPost("create")]
    public  IActionResult create()
    {
        
         return  Ok();
    }



    [HttpGet("Thang/getAllSupplier")]
     public async Task<IActionResult> getAllSupplier()
    {
     var a = await _databaseContext.Suppliers.AsNoTracking().Select(b => new
     {      
         b.Id,
         b.SupplierName,
         b.Status,
        
     }).ToListAsync();
        
        return Ok(a);
    }

    [HttpPost("Thang/create")]
    [Consumes("multipart/form-data")]	
       [Produces("application/json")]
    public async Task<IActionResult> createProduct([FromForm] string data , [FromForm] IFormFile? file,[FromForm] List<IFormFile>? fileSide)
    {
           try
        {
         var product = JsonConvert.DeserializeObject<Product>(data);
            if(product == null) return NotFound("Can  not  Get  Product!");
              if(file!= null)
                {
                    var  fileName = GenerateRandomString(10);
                        fileName = Path.Combine("product","index"+fileName+Path.GetExtension(file.FileName));
                     
                      var filePath = Path.Combine(_webHostEnvironment.WebRootPath, fileName);
                        using var fileStream = new FileStream(filePath, FileMode.Create);
               await file.CopyToAsync(fileStream);

                product.Images.Add(new PlantNestBackEnd.Models.Image()
                {
                    ImageUrl = fileName
                });
              //  category.CategoryImage = fileName;


            }
              if(fileSide!=null && fileSide.Count > 0)
            {
                fileSide.ForEach(fileA =>
                {
                     var  fileName = GenerateRandomString(10);
                        fileName = Path.Combine("product",fileName+Path.GetExtension(fileA.FileName));
                     
                      var filePath = Path.Combine(_webHostEnvironment.WebRootPath, fileName);
                        using var fileStream = new FileStream(filePath, FileMode.Create);
                    fileA.CopyTo(fileStream);
                    fileStream.Dispose();
                    product.Images.Add(new PlantNestBackEnd.Models.Image()
                    {
                        ImageUrl = fileName
                    });

                  //  category.CategoryImage = fileName;
                });
            }

             product.Status=true;
     
          await _databaseContext.Products.AddAsync(product);
      
            return (await _databaseContext.SaveChangesAsync()>0)?Ok():NotFound("Can not create Product !");
        }
        catch (Exception)
        {

           return NotFound("Some thing wrong");
        }
     
        
       
    }
       
    
    
        [HttpPut("Thang/update/{id}")]
    [Consumes("multipart/form-data")]	
       [Produces("application/json")]
    public async Task<IActionResult> updateProduct(int id,[FromForm] string data , [FromForm] IFormFile? file,[FromForm] List<IFormFile>? fileSide)
    {
           try
        {
         var product = JsonConvert.DeserializeObject<Product>(data);
            var dbProduct = await _databaseContext.Products.Where(a=>a.Id==id).Include(a=>a.Images).FirstOrDefaultAsync();
            if(dbProduct == null) return NotFound("Can not find Product");
            if(product == null) return NotFound("Can  not  Get  Product!");
              if(file!= null)
                {
                    var ab = dbProduct.Images.Where(img=>img.ImageUrl.Contains("index")).FirstOrDefault();
                    
                   
                                var  fileName = GenerateRandomString(10);
                                    fileName = Path.Combine("product","indexVui"+fileName+Path.GetExtension(file.FileName));
                     
                                  var filePath = Path.Combine(_webHostEnvironment.WebRootPath, fileName);
                                using var fileStream = new FileStream(filePath, FileMode.Create);
                                await file.CopyToAsync(fileStream);
                               
                        if (ab == null)
                        {
                            
                              dbProduct.Images.Add(new PlantNestBackEnd.Models.Image()
                                        {
                                            ImageUrl = fileName
                                        });
                        
                }
                else
                {
                    try
                    {
                         var filePath1 = Path.Combine(_webHostEnvironment.WebRootPath,ab.ImageUrl );

                        using var stream = new FileStream(filePath1, FileMode.Open);
                        stream.Dispose();
                        System.IO.File.Delete(filePath1);

                    }
                    catch (Exception)
                    {

                        
                    }
                     
                   dbProduct.Images.Where(img=>img.ImageUrl.Contains("index")).FirstOrDefault().ImageUrl=fileName ;
                    _databaseContext.Images.Update(ab);
                }
                              
              //  category.CategoryImage = fileName;


            }
              if(fileSide!=null && fileSide.Count > 0)
            {
                var number = 0 ;
                
                dbProduct.Images = dbProduct.Images.Where(img=>img.ImageUrl.Contains("index")).ToList() ;
              
             
               
                fileSide.ForEach(fileA =>
                {
                     var  fileName = GenerateRandomString(10);
                        fileName = Path.Combine("product","123"+fileName+Path.GetExtension(fileA.FileName));
                     
                      var filePath = Path.Combine(_webHostEnvironment.WebRootPath, fileName);
                    using var fileStream = new FileStream(filePath, FileMode.Create);
                    fileA.CopyTo(fileStream);
                    fileStream.Dispose();

                    dbProduct.Images.Add(new PlantNestBackEnd.Models.Image()
                        {
                            ImageUrl = fileName
                        });
                  
                   



                    number++;
                  //  category.CategoryImage = fileName;
                });
            }

             


            dbProduct.ProductName = product.ProductName;
            dbProduct.Description = product.Description;
            dbProduct.CostPrice = product.CostPrice;
            dbProduct.SellPrice = product.SellPrice;
            dbProduct.Quantity = product.Quantity;
            dbProduct.CreatedDate = product.CreatedDate;
            dbProduct.SupplierId = product.SupplierId;
            dbProduct.CategoryId = product.CategoryId;

            _databaseContext.Products.Update(dbProduct);
           
            return (await _databaseContext.SaveChangesAsync()>0)?Ok():NotFound("Can not create Product !");
        }
        catch (Exception)
        {

           return NotFound("Some thing wrong");
        }
     
        
       
    }
    public  string GenerateRandomString(int length)
        {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
         }
     [HttpGet("Thang/byId/{id}")]
    public async Task<IActionResult> getByIdUPdate(int id)
    {
        try
        {
             var a = await _databaseContext.Products.AsNoTracking().Where(b=>b.Id== id)
            .Include(b=>b.Images)
            .Include(b=>b.Supplier)
            .Include(b=>b.Category)
            .Select(b => new
            {
                b.Id,
                b.ProductName,
                b.Description,
                b.CostPrice,
                b.SellPrice,
                b.Quantity,
                b.CreatedDate,
                b.SupplierId,
                b.CategoryId,
                b.Status,
                Category=new 
                {
                    b.Category.CategoryId,
                      b.Category.CategoryImage,
                    b.Category.CategoryName,
                    b.Category.Created,
                    b.Category.Status
                },
                Images = b.Images.Select(c=>new
                {
                    c.Id,
                    c.ImageUrl,
                    c.ProductId,
                }),
                Supplier=new
                {
                    b.Supplier.Id,
                    b.Supplier.SupplierName,
                    b.Supplier.Status
                }
            })
            .FirstOrDefaultAsync();


        return Ok(a);
        }
        catch (Exception)
        {

          return NotFound();
        }
       
    }

}
