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

        public IEnumerable<Customer> Get()
        {
            return customers;
        }

        public IEnumerable<Customer> GetActive()
        {
            return customers.Where(c => !c.IsRemoved).ToList();
        }
    }

    public class FakeOrderService : IOrderService
    {
        private ICollection<Order> orders;

        public IEnumerable<Order> Get()
        {
            return orders;
        }

        public IEnumerable<Order> Get(string username)
        {
            return orders.Where(o => o.Customer.Username == username).ToList();
        }
    }
}
