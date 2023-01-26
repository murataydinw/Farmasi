using System;
using Farmasi.Core.Domain.Categories;
using Farmasi.Data.MongoDataAccess;

namespace Farmasi.Data.Categories
{
    public interface ICategoryRepository : IMongoRepository<Category>
    {
    }
}

