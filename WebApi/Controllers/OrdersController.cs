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
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrdersController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        // GET http://localhost:5000/api/orders
        
       // [HttpGet]
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

        [Authorize(Roles = "Creator")]
        [HttpPost]
        public IActionResult Post(Order order)
        {
            string email = User.FindFirstValue(ClaimTypes.Email);

            Trace.WriteLine($"Send email to {email}");

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
