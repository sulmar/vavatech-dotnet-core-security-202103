using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using Models;

namespace AuthenticationHandlers
{

    // dotnet add package Microsoft.AspNetCore.Authorization
    public class MinimumAgeRequirement : IAuthorizationRequirement  // mark interface
    {
        public int MinimumAge { get; }

        public MinimumAgeRequirement(int minimumAge)
        {
            this.MinimumAge = minimumAge;
        }
    }

    public static class MinimumAgeHandlerExtensions
    {
        public static AuthorizationPolicyBuilder RequireAge(this AuthorizationPolicyBuilder policy, int age)
        {
            policy.RequireClaim(ClaimTypes.DateOfBirth);
            policy.Requirements.Add(new MinimumAgeRequirement(26));

            return policy;
        }
    }

    public class MinimumAgeHandler : AuthorizationHandler<MinimumAgeRequirement>
    {
        // dotnet add package Microsoft.Extensions.Identity.Core
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
        {
            if (!context.User.HasClaim(p=>p.Type == ClaimTypes.DateOfBirth))
            {
                context.Fail();

                return Task.CompletedTask;
            }

            DateTime dateOfBirth = Convert.ToDateTime(context.User.FindFirstValue(ClaimTypes.DateOfBirth));

            int age = DateTime.Today.Year - dateOfBirth.Year;

            if (age >= requirement.MinimumAge)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }


    // Policy oparte o zasoby

    public class TheSameAuthorRequirement : IAuthorizationRequirement
    {
    }

    public class OrderAuthorizationHandler : AuthorizationHandler<TheSameAuthorRequirement, Order>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TheSameAuthorRequirement requirement, Order resource)
        {
            string username = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (resource.Customer.Username == username)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }


}
