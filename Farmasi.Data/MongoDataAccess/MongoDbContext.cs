using System;
using Farmasi.Data.Categories;
using Farmasi.Data.Customers;
using Farmasi.Data.Products;

namespace Farmasi.Data.MongoDataAccess
{
    public class MongoDbContext : IDbContext
    {
        public ICategoryRepository Categories { get; set; }
        public IProductRepository Products { get; set; }
        public ICustomerRepository Customers { get; set; }
        public MongoDbContext(ICategoryRepository category, IProductRepository product, ICustomerRepository customer)
        {
            Categories = category;
            Products = product;
            Customers = customer;
        }
    }
}

