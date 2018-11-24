using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HrSystemLib.Models.Interfaces;

namespace HrSystemLib.Models
{
    public class ExpenseClaim : IClaim
    {
        public int Id { get; set; }
        public int BranchId { get; set; }
        public string BranchCode { get; set; }
        public int BankId { get; set; }
        public string BankCode { get; set; }
        public string BankAccountNo { get; set; }
        public string BankAccountName { get; set; }
        public ClaimStatus Status { get; set; }
        public decimal TotalLocalAmount { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedOnDate { get; set; }
        public int ModifiedByUserId { get; set; }
        public DateTime ModifiedOnDate { get; set; }
        public bool IsDeleted { get; set; }
        public List<IClaimItem> ClaimItems { get; set; }
    }
}
