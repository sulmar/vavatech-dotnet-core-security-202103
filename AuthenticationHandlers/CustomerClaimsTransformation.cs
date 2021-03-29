using IServices;
using Microsoft.AspNetCore.Authentication;
using Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationHandlers
{
    public class CustomerClaimsTransformation : IClaimsTransformation
    {
        private readonly ICustomerService customerService;

        public CustomerClaimsTransformation(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            // Kopia bieżącej tożsamości

            ClaimsPrincipal clonePrincipal = principal.Clone();

            ClaimsIdentity identity = (ClaimsIdentity) clonePrincipal.Identity;

            string username = identity.FindFirst(ClaimTypes.NameIdentifier).Value;

            Customer customer = customerService.Get(username);

            identity.AddClaim(new Claim("http://vavatech/pl2020/03/identity/claims/licence_category", "B"));
            identity.AddClaim(new Claim("http://vavatech/pl2020/03/identity/claims/licence_category", "A"));
            identity.AddClaim(new Claim(ClaimTypes.Email, customer.Email));
            identity.AddClaim(new Claim(ClaimTypes.MobilePhone, customer.PhoneNumber));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, customer.Username));
            identity.AddClaim(new Claim(ClaimTypes.Name, customer.FullName));

            identity.AddClaim(new Claim(ClaimTypes.Role, "Creator"));
            identity.AddClaim(new Claim(ClaimTypes.Role, "Administrator"));

            return Task.FromResult(clonePrincipal);



        }
    }
}
