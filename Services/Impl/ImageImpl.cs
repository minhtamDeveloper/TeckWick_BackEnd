using PlantNestBackEnd.Models;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using static System.Net.Mime.MediaTypeNames;

namespace PlantNestBackEnd.Services.Impl;

public class ImageImpl : IImage
{
    private DatabaseContext db;
    private IConfiguration configuration;
    public ImageImpl(DatabaseContext _db, IConfiguration _configuration)
    {
        db = _db;
        configuration = _configuration;
    }
    public dynamic Img(int productId)
    {
        return db.Images.Where(a => a.ProductId == productId).Select(a => new
        {
            id = a.Id,  
            imageUrl = configuration["BaseUrl"] + "images/" + a.ImageUrl,
            productId = a.ProductId,
        }).ToList();

    }
}
