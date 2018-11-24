using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystemLib.Models
{
    public class ExpenseClaimItem : Interfaces.IClaimItem
    {
        public int Id { get; set; }
        public int ClaimId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string CostCenter { get; set; }
        public string GlCode { get; set; }
        public string Description { get; set; }
        public int CurrencyId { get; set; }
        public string CurrencyCode { get; set; }
        public decimal Amount { get; set; }
        public decimal ExchangeRate { get; set; }
        public decimal LocalAmount { get; set; }
    }
}
