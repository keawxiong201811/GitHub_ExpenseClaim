using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystemLib.Models.Interfaces
{
    public enum ClaimStatus
    {
        New,
        Submitted,
        Approved,
        Rejected,
        Cancelled
    }
    public interface IClaim
    {
        int Id { get; set; }
        int BranchId { get; set; }
        string BranchCode { get; set; }
        int BankId { get; set; }
        string BankCode { get; set; }
        string BankAccountNo { get; set; }
        string BankAccountName { get; set; }
        ClaimStatus Status { get; set; }
        decimal TotalLocalAmount { get; set; }
        int CreatedByUserId { get; set; }
        DateTime CreatedOnDate { get; set; }
        int ModifiedByUserId { get; set; }
        DateTime ModifiedOnDate { get; set; }
        bool IsDeleted { get; set; }
        List<IClaimItem> ClaimItems { get; set; }
    }
}
