using Bogus;
using IServices;
using Models;
using System;
using System.Collections.Generic;

namespace Fakers
{
    public class OrderFaker : Faker<Order>
    {
        public OrderFaker(ICustomerService customerService)
        {
            UseSeed(1);
            StrictMode(true);
            RuleFor(p => p.Id, f => f.IndexFaker);
            RuleFor(p => p.OrderDate, f => f.Date.Past());
            RuleFor(p => p.Customer, f => f.PickRandom(customerService.Get()));
            RuleFor(p => p.TotalAmount, f => Math.Round(f.Random.Decimal(1, 1000), 2));
        }
    }
}
