using IServices;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public IActionResult Get()
        {
            IEnumerable<Order> orders = orderService.Get();

            return Ok(orders);
        }
    }
}
