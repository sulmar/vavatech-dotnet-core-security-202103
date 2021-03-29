using Bogus;
using IServices;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FakeServices
{
    public class FakeCustomerService : ICustomerService
    {
        private ICollection<Customer> customers;

        public FakeCustomerService(Faker<Customer> faker)
        {
            customers = faker.Generate(100);
        }

        public IEnumerable<Customer> Get()
        {
            return customers;
        }

        public IEnumerable<Customer> GetActive()
        {
            return customers.Where(c => !c.IsRemoved).ToList();
        }
    }
}
