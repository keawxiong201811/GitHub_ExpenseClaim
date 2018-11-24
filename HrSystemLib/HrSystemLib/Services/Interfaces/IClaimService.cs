using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HrSystemLib.Models.Interfaces;

namespace HrSystemLib.Services.Interfaces
{
    public interface IClaimService
    {
        List<IClaim> ListClaims();
        List<IClaim> ListClaims(int UserId);
        IClaim GetClaim(int Id);
        int CreateClaim(int BranchId, int BankId, string BankAccountNo, string BankAccountName, int UserId);
        bool AddClaimItem(int ClaimId, int CurrencyId, DateTime TransactionDate, string CostCenter, string GlCode, string Description, decimal Amount, int UserId);
        bool RemoveClaimItem(int ClaimItemId, int UserId);
        bool SubmitClaim(int ClaimId, int UserId);
    }
}
