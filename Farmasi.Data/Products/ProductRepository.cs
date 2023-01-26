using System;
using Farmasi.Core.Domain.Customers;
using Farmasi.Core.Domain.Produtcs;
using Farmasi.Data.Customers;
using Farmasi.Data.MongoDataAccess;

namespace Farmasi.Data.Products
{
    public class ProductRepository : MongoRepository<Product>, IProductRepository
    {
        public ProductRepository(string connectionString) : base(connectionString)
        {
        }
    }
}
