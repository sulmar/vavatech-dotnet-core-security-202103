using IServices;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthenticationHandlers
{
    public class CustomerAuthorizationService : IAuthorizationService
    {
        private readonly ICustomerService customerService;

        public CustomerAuthorizationService(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        public bool TryValidate(string login, string password, out Customer customer)
        {
            customer = customerService.Get(login, password);

            return customer != null;
        }
    }
}
