using Microsoft.EntityFrameworkCore;
using PlantNestBackEnd.Models;
using PlantNestBackEnd.Services;
using PlantNestBackEnd.Services.Impl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var stringConnection = builder.Configuration["ConnectionStrings:Default"];
builder.Services.AddDbContext<DatabaseContext>(option =>option.UseLazyLoadingProxies().UseSqlServer(stringConnection));
builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IProduct, ProductImpl>();
builder.Services.AddScoped<ICategory, CategoryImpl>();
builder.Services.AddScoped<IUser, UserImpl>();
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
