using System;
using Farmasi.Data.Categories;
using Farmasi.Data.Customers;
using Farmasi.Data.Products;

namespace Farmasi.Data.MongoDataAccess
{
    public interface IDbContext
    {
        ICategoryRepository Categories { get; set; }
        IProductRepository Products { get; set; }
        ICustomerRepository Customers { get; set; }
    }
}

