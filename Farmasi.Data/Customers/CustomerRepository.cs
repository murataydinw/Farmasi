using System;
using Farmasi.Core.Domain.Categories;
using Farmasi.Core.Domain.Customers;
using Farmasi.Data.Categories;
using Farmasi.Data.MongoDataAccess;

namespace Farmasi.Data.Customers
{
    public class CustomerRepository : MongoRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(string connectionString) : base(connectionString)
        {
        }
    }
}

