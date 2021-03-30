using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Order : BaseEntity
    {
        public DateTime OrderDate { get; set; }
        public Customer Customer { get; set; }

        [Range(1, 10000)]
        public decimal TotalAmount { get; set; }
        public string Note { get; set; }
    }
}
