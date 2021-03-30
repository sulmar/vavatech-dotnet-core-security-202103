using CSRFAttack.IServices;
using CSRFAttack.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSRFAttack.Controllers
{
    public class BankTransfersController : Controller
    {
        private readonly IBankTransferService bankTransferService;

        public BankTransfersController(IBankTransferService bankTransferService)
        {
            this.bankTransferService = bankTransferService;
        }

        public IActionResult Index()
        {
            var bankTransfers = bankTransferService.Get();

            return View(bankTransfers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
      //  [ValidateAntiForgeryToken]
        public IActionResult Create(BankTransfer bankTransfer)
        {
            bankTransferService.Add(bankTransfer);

            return RedirectToAction(nameof(Index));
        }
    }
}
