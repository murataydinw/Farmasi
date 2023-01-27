using Farmasi.Data.Categories;
using Farmasi.Data.Customers;
using Farmasi.Data.MongoDataAccess;
using Farmasi.Data.Products;
using Farmasi.Service.Categories;
using Farmasi.Service.Products;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var mongoConnectionString = builder.Configuration.GetConnectionString("MongoConnectionString");
builder.Services.AddSingleton<ICategoryRepository, CategoryRepository>(x => new CategoryRepository(mongoConnectionString));
builder.Services.AddSingleton<IProductRepository, ProductRepository>(x => new ProductRepository(mongoConnectionString));
builder.Services.AddSingleton<ICustomerRepository, CustomerRepository>(x => new CustomerRepository(mongoConnectionString));
builder.Services.AddTransient<IDbContext, MongoDbContext>();
builder.Services.AddSingleton<ICategoryService, CategoryService>();
builder.Services.AddSingleton<IProductService, ProductService>();



builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});
// services.AddSingleton<ICacheManager, MemoryCacheManager>();
var app = builder.Build();

// Configure the HTTP request pipeline.
#region Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Farmasi api v1");
});
#endregion

app.UseHttpsRedirection();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    _ = endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});


app.UseAuthorization();

app.MapControllers();

app.Run();

