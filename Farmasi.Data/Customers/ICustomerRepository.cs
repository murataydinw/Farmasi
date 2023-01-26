using System;
using Farmasi.Core.Domain.Customers;
using Farmasi.Data.MongoDataAccess;

namespace Farmasi.Data.Customers
{
    public interface ICustomerRepository : IMongoRepository<Customer>
    {
    }
}

