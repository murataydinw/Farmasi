using Farmasi.Data.Categories;
using Farmasi.Data.Customers;
using Farmasi.Data.Products;
using Farmasi.Service.Categories;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var mongoConnectionString = builder.Configuration.GetConnectionString("MongoConnectionString");
builder.Services.AddSingleton<ICategoryRepository, CategoryRepository>(x => new CategoryRepository(mongoConnectionString));
builder.Services.AddSingleton<IProductRepository, ProductRepository>(x => new ProductRepository(mongoConnectionString));
builder.Services.AddSingleton<ICustomerRepository, CustomerRepository>(x => new CustomerRepository(mongoConnectionString));

builder.Services.AddSingleton<ICategoryService, CategoryService>();
// services.AddSingleton<ICacheManager, MemoryCacheManager>();
var app = builder.Build();

// Configure the HTTP request pipeline.

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

