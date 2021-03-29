using IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    
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
      //  [Authorize]
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
    }
}
