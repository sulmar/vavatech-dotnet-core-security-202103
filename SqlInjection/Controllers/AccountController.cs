using Microsoft.AspNetCore.Mvc;
using SqlInjection.IServices;
using SqlInjection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SqlInjection.Controllers
{
    public class AccountController : Controller
    {
        private readonly IEmployeeRepository employeeRepository;

        public AccountController(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginViewModel model)
        {
            Employee employee = employeeRepository.Validate(model.UserName, model.Password);

            if (employee != null)
            {
                TempData["msg"] = "You are welcome!";
            }
            else
            {
                TempData["msg"] = "Username or password is invalid.";
            }

            return View();
        }
    }
}
