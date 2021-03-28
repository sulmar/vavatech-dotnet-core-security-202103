using CSRFAttack.IServices;
using CSRFAttack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSRFAttack.Services
{
    public class FakeBankTransferService : IBankTransferService
    {
        private ICollection<BankTransfer> bankTransfers;

        public FakeBankTransferService()
        {
            bankTransfers = new List<BankTransfer>();
        }

        public void Add(BankTransfer bankTransfer)
        {
            bankTransfers.Add(bankTransfer);
        }

        public IEnumerable<BankTransfer> Get()
        {
            return bankTransfers;
        }
    }
}
