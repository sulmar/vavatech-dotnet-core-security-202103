using IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Models;
using System;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace AuthenticationHandlers
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private const string authorizationKey = "Authorization";

        private readonly IAuthorizationService authorizationService;

        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options, 
            ILoggerFactory logger, 
            UrlEncoder encoder, 
            ISystemClock clock,
            IAuthorizationService authorizationService
            ) : base(options, logger, encoder, clock)
        {
            this.authorizationService = authorizationService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey(authorizationKey))
            {
                return AuthenticateResult.Fail("Missing auhorization header");
            }

            var authorizationHeader = AuthenticationHeaderValue.Parse(Request.Headers[authorizationKey]);

            if (authorizationHeader.Scheme != "Basic")
            {
                return AuthenticateResult.Fail("Invalid schema");
            }

            byte[] credentialBytes = Convert.FromBase64String(authorizationHeader.Parameter);
            string[] credentials = Encoding.UTF8.GetString(credentialBytes).Split(':');

            string login = credentials[0];
            string password = credentials[1];

            if (!authorizationService.TryValidate(login, password, out Customer customer))
            {
                return AuthenticateResult.Fail("Invalid login or password");
            }

            ClaimsIdentity identity = new ClaimsIdentity(Scheme.Name);

            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, customer.Username));

            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            AuthenticationTicket authenticationTicket = new AuthenticationTicket(principal, Scheme.Name);
                
            return AuthenticateResult.Success(authenticationTicket);
        }
    }
}
