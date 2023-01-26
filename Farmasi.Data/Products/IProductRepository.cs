using System;
using Farmasi.Core.Domain.Produtcs;
using Farmasi.Data.MongoDataAccess;

namespace Farmasi.Data.Products
{
    public interface IProductRepository : IMongoRepository<Product>
    {
    }
}

