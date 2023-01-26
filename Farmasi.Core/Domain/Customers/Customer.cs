using System;
using Farmasi.Core.Domain.Common;
namespace Farmasi.Core.Domain.Customers
{
    [MongoDb(DbName = "Farmasi", CollectionName = "Customers")]
    public class Customer : BaseEntity, ISoftDeletedEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Deleted { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}

