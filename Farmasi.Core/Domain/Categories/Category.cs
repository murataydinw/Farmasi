using System;
using Farmasi.Core.Domain.Produtcs;

namespace Farmasi.Core.Domain.Categories
{
    [MongoDb(DbName = "Farmasi", CollectionName = "Categories")]
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Url { get; set; }
       // IEnumerable<Product> Products { get; set; }
    }
}

