using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSRFAttack.Models
{
    public class BankTransfer
    {
        public DateTime TransferDate { get; set; }
        public string RecipientAccount { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }

    }
}
