using PlanetNest.Converters;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();

builder.Services.AddControllers().AddJsonOptions(option =>
{
    option.JsonSerializerOptions.Converters.Add(new DateConverter());
});
// doc chuoi ket noi ra va ket noi db
var connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];
//builder.Services.AddDbContext<DatabaseContext>(option => option.UseLazyLoadingProxies().UseSqlServer(connectionString));
//


var app = builder.Build();
app.UseCors(builder => builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .SetIsOriginAllowed((host) => true)
                .AllowCredentials()
            );
app.UseStaticFiles();
app.MapControllers();

app.Run();