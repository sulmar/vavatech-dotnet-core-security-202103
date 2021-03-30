using AuthenticationHandlers;
using Ganss.XSS;
using IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrdersController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        // GET http://localhost:5000/api/orders

        // [HttpGet]
        [Authorize(Policy = "Adult")]
        public IActionResult Get()
        {
            IEnumerable<Order> orders = null;

            if (this.User.Identity.IsAuthenticated)
            {
                if (this.User.IsInRole("Administrator"))
                {
                    orders = orderService.Get();
                }
                else
                {
                    orders = orderService.Get(User.Identity.Name);
                }


                return Ok(orders);
            }
            else
            {
                return Unauthorized();
            }

        }

        // https://docs.microsoft.com/pl-pl/aspnet/core/security/authorization/resourcebased?view=aspnetcore-5.0

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, [FromServices] Microsoft.AspNetCore.Authorization.IAuthorizationService authorizationService)
        {
            Order order = orderService.Get(id);

            var authorizationResult = await authorizationService.AuthorizeAsync(User, order, new TheSameAuthorRequirement());

            if (authorizationResult.Succeeded)
            {
                return Ok(order);
            }
            else if (User.Identity.IsAuthenticated)
            {
                return Forbid();
            }
            else
            {
                return Challenge();
            }
        }

        [Authorize(Policy = "Creator")]
        [HttpPost]
        public IActionResult Post(Order order, [FromServices] IHtmlSanitizer sanitizer)
        {
            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState);

            // https://xss.ganss.org/
            order.Note = sanitizer.Sanitize(order.Note);

            string email = User.FindFirstValue(ClaimTypes.Email);

            Trace.WriteLine($"Send email to {email} {order.Note}");

            return Ok(order);
        }

        [AllowAnonymous]
        [HttpOptions]
        public IActionResult Options()
        {
            return Ok("GET,POST");
        }
    }
}
