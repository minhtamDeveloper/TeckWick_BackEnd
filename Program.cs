using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PlantNest.Services;
using PlantNestBackEnd.Converter;
using PlantNestBackEnd.Models;
using PlantNestBackEnd.Services;
using PlantNestBackEnd.Services.Impl;
using Swashbuckle.AspNetCore.Filters;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var stringConnection = builder.Configuration["ConnectionStrings:Default"];
builder.Services.AddDbContext<DatabaseContext>(option => option.UseLazyLoadingProxies().UseSqlServer(stringConnection));
builder.Services.AddControllers().AddJsonOptions(option =>
{
    option.JsonSerializerOptions.Converters.Add(new DateConverter());
});
builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen(option =>
{
    //option.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    //{
    //    In = ParameterLocation.Header,
    //    Name = "Authorization",
    //    Type = SecuritySchemeType.ApiKey
    //});
    //option.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddScoped<ICategory, CategoryImpl>();
builder.Services.AddScoped<IUser, UserImpl>();
builder.Services.AddScoped<IProduct, ProductImpl>();
builder.Services.AddScoped<ICarts, CartImpl>();
builder.Services.AddScoped<IFavoriteCart, FavoriteCartImpl>();
builder.Services.AddScoped<IOrder, OrderImpl>();
builder.Services.AddScoped<IOrderDetail, OrderDetailImpl>();
builder.Services.AddScoped<IAccount, AccountImpl>();
builder.Services.AddScoped<ISupplier, SupplierImpl>();
builder.Services.AddScoped<IProduct, ProductImpl>();
builder.Services.AddScoped<IImage, ImageImpl>();
builder.Services.AddScoped<iContact, contactImpl>();
builder.Services.AddScoped<IFavoriteCart, FavoriteCartImpl>();
builder.Services.AddScoped<IDelivery, DeliveryImpl>();

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Token"])),
        ValidateAudience = false,
        ValidateIssuer = false,
        ClockSkew = TimeSpan.Zero,

    };
});
//builder.Services.AddAuthorization();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(

    //{
    //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Plant Nest");
    //    c.RoutePrefix = string.Empty;
    //}
      );
}
app.UseStaticFiles();

app.UseCors(builder => builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .SetIsOriginAllowed((host) => true)
                .AllowCredentials()
            );
app.UseHttpsRedirection();

app.MapControllers();

app.Run();
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////