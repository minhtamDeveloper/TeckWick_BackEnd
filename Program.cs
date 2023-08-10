using Microsoft.EntityFrameworkCore;
using PlantNestBackEnd.Models;
using PlantNestBackEnd.Services;
using PlantNestBackEnd.Services.Impl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var stringConnection = "workstation id=PlantNestDB.mssql.somee.com;packet size=4096;user id=minhtamceo2_SQLLogin_1;pwd=pf71j8t97t;data source=PlantNestDB.mssql.somee.com;persist security info=False;initial catalog=PlantNestDB";
builder.Services.AddDbContext<DatabaseContext>(option =>option.UseLazyLoadingProxies().UseSqlServer(stringConnection));
builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IProduct, ProductImpl>();
builder.Services.AddScoped<ICategory, CategoryImpl>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();

app.UseCors(builder => builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .SetIsOriginAllowed((host) => true)
                .AllowCredentials()
            );
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
