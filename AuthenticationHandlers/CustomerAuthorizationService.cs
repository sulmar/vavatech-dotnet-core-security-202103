using IServices;
using Microsoft.AspNetCore.Identity;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthenticationHandlers
{
    public class CustomerAuthorizationService : IAuthorizationService
    {
        private readonly ICustomerService customerService;
        private readonly IPasswordHasher<Customer> passwordHasher;

        public CustomerAuthorizationService(ICustomerService customerService, IPasswordHasher<Customer> passwordHasher)
        {
            this.customerService = customerService;
            this.passwordHasher = passwordHasher;
        }

        public bool TryValidate(string login, string password, out Customer customer)
        {
            customer = customerService.Get(login);

            if (customer!=null)
            {
                return passwordHasher.VerifyHashedPassword(customer, customer.HashedPassword, password) == PasswordVerificationResult.Success;
            }

            return false;
        }
    }
}
