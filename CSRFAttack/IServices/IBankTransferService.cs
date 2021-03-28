using CSRFAttack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSRFAttack.IServices
{
    public interface IBankTransferService
    {
        IEnumerable<BankTransfer> Get();

        void Add(BankTransfer bankTransfer);

        
    }
}
