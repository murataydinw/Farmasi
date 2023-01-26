using System;
using Farmasi.Core.Domain.Categories;
using Farmasi.Data.MongoDataAccess;

namespace Farmasi.Data.Categories
{
    public class CategoryRepository : MongoRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(string connectionString) : base(connectionString)
        {
        }
    }
}

