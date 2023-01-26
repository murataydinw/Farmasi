using System;
using Farmasi.Core.Domain.Categories;
using Farmasi.Core.Domain.Common;

namespace Farmasi.Core.Domain.Produtcs
{
    [MongoDb(DbName = "Farmasi", CollectionName = "Products")]
    public class Product : BaseEntity, ISoftDeletedEntity
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public decimal Price { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public bool Deleted { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}

