using Models;
using System;
using System.Collections.Generic;

namespace IServices
{
    public interface ICustomerService
    {
        IEnumerable<Customer> Get();
        IEnumerable<Customer> GetActive();
        Customer Get(string username, string password);

        Customer Get(string username);
    }

}
