using IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]        
    public class CustomersController : ControllerBase
    {
        private readonly IServices.IAuthorizationService authorizationService;

        public CustomersController(IServices.IAuthorizationService authorizationService)
        {
            this.authorizationService = authorizationService;
        }


        // POST api/customers/createtoken
        [AllowAnonymous]
        [HttpPost("createtoken")]
        public IActionResult GenerateToken([FromBody] LoginModel model, [FromServices] ITokenService tokenService)
        {
            if (authorizationService.TryValidate(model.Username, model.Password, out Customer customer))
            {
                string token = tokenService.CreateToken(customer);

                return Ok(token);
            }
            else
            {
                return Unauthorized();
            }
        }
    }

    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
