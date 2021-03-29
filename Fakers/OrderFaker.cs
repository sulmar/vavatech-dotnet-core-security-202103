using Bogus;
using Models;
using System;

namespace Fakers
{
    public class OrderFaker : Faker<Order>
    {
        public OrderFaker(Faker<Customer> customerFaker)
        {
            UseSeed(1);
            StrictMode(true);
            RuleFor(p => p.Id, f => f.IndexFaker);
            RuleFor(p => p.OrderDate, f => f.Date.Past());
            RuleFor(p => p.Customer, f => customerFaker.Generate());
            RuleFor(p => p.TotalAmount, f => Math.Round(f.Random.Decimal(1, 1000), 2));
        }
    }
}
