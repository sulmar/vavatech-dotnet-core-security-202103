using Bogus;
using IServices;
using Models;
using System.Collections.Generic;
using System.Linq;

namespace FakeServices
{
    public class FakeOrderService : IOrderService
    {
        private ICollection<Order> orders;

        public FakeOrderService(Faker<Order> faker)
        {
            orders = faker.Generate(50);
        }

        public IEnumerable<Order> Get()
        {
            return orders;
        }

        public IEnumerable<Order> Get(string username)
        {
            return orders.Where(o => o.Customer.Username == username).ToList();
        }

        public Order Get(int id)
        {
            return orders.SingleOrDefault(o => o.Id == id);
        }
    }
}
