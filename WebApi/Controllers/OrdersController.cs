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
            if (this.User.Identity.IsAuthenticated)
            {
                IEnumerable<Order> orders = orderService.Get();

                return Ok(orders);
            }
            else
            {
                return Unauthorized();
            }
           
        }

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
