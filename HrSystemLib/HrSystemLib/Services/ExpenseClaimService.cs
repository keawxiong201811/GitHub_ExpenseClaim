using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HrSystemLib.Services.Interfaces;
using HrSystemLib.Services;
using HrSystemLib.Models.Interfaces;
using HrSystemLib.DataAccess;
using HrSystemLib.Helper;

namespace HrSystemLib.Services
{
    public class ExpenseClaimService : IClaimService
    {
        private ExpenseClaimDA expenseclaimda;

        public ExpenseClaimService()
        {
            this.expenseclaimda = new ExpenseClaimDA();
        }
        public List<IClaim> ListClaims()
        {
            List<IClaim> claims = expenseclaimda.GetExpenseClaims();
            List<IBranch> branches = new BranchDA().GetBranches();
            List<IBank> banks = new BankDA().GetBanks();
            foreach(IClaim c in claims)
            {
                c.BranchCode = branches.Where(b => b.Id == c.BranchId).SingleOrDefault().Code;
                c.BankCode = banks.Where(b => b.Id == c.BankId).SingleOrDefault().Code;
            }
            return expenseclaimda.GetExpenseClaims();
        }
        public List<IClaim> ListClaims(int UserId)
        {
            List<IClaim> claims = expenseclaimda.GetExpenseClaims();
            if (claims == null)
                return null;
            List<IBranch> branches = new BranchDA().GetBranches();
            List<IBank> banks = new BankDA().GetBanks();
            foreach (IClaim c in claims)
            {
                c.BranchCode = branches.Where(b => b.Id == c.BranchId).SingleOrDefault().Code;
                c.BankCode = banks.Where(b => b.Id == c.BankId).SingleOrDefault().Code;
            }
            return claims.Where(c => c.CreatedByUserId == UserId).ToList();
        }
        public IClaim GetClaim(int Id)
        {
            IClaim expenseClaim = expenseclaimda.GetExpenseClaims().Where(c => c.Id == Id).SingleOrDefault();
            expenseClaim.ClaimItems = expenseclaimda.GetExpenseClaimItems(expenseClaim.Id);
            if (expenseClaim.ClaimItems == null)
                expenseClaim.ClaimItems = new List<IClaimItem>();
            List<ICurrency> currencies = new CurrencyDA().GetCurrencies();
            foreach (IClaimItem item in expenseClaim.ClaimItems)
            {
                item.CurrencyCode = currencies.Where(c => c.Id == item.CurrencyId).SingleOrDefault().Code;
            }
            return expenseClaim;
        }
        public int CreateClaim(int BranchId, int BankId, string BankAccountNo, string BankAccountName, int UserId)
        {
            string errorMsg = "";

            if (new BranchService().GetBranch(BranchId) == null)
                errorMsg += String.Format("Branch Id: {0} is not exist.", BranchId) + Environment.NewLine;
                //throw new Exception(String.Format("Branch Id: {0} is not exist.", BranchId));

            if(new BankService().GetBank(BankId) == null)
                errorMsg += String.Format("Bank Id: {0} is not exist.", BankId) + Environment.NewLine;
                //throw new Exception(String.Format("Bank Id: {0} is not exist.", BankId));

            if (BankAccountNo == null || BankAccountNo.Trim() == "")
                errorMsg += String.Format("Bank Account No is required.") + Environment.NewLine;
                //throw new Exception(String.Format("Bank Account No is required."));

            if (BankAccountName == null || BankAccountName.Trim() == "")
                errorMsg += String.Format("Bank Account Name is required.") + Environment.NewLine;
                //throw new Exception(String.Format("Bank Account Name No is required."));

            if (errorMsg.Length > 0)
                throw new Exception(errorMsg);

            return this.expenseclaimda.AddExpenseClaim(BranchId, BankId, BankAccountNo, BankAccountName, (short)ClaimStatus.New, UserId);
        }
        public bool AddClaimItem(int ClaimId, int CurrencyId, DateTime TransactionDate, string CostCenter, string GlCode, string Description, decimal Amount, int UserId)
        {
            IClaim expenseClaim = GetClaim(ClaimId);
            if (expenseClaim == null)
                throw new Exception(String.Format("ClaimId:{0} is not exist.", ClaimId));

            if (expenseClaim.Status != ClaimStatus.New && expenseClaim.Status != ClaimStatus.Rejected)
                throw new Exception(String.Format("Status:{0} is not allowed to update.", expenseClaim.Status.ToString()));

            string errorMsg = "";
            if (TransactionDate == DateTime.MinValue || TransactionDate == DateTime.MaxValue)
                errorMsg += String.Format("TransactionDate is required.") + Environment.NewLine;
            if (CostCenter == null) CostCenter = "";
            if (GlCode == null) GlCode = "";
            if(Description == null || Description.Trim() == "")
                errorMsg += String.Format("Description is required.") + Environment.NewLine;
            if(Amount <= 0)
                errorMsg += String.Format("Amount is required and must be more than 0.00.") + Environment.NewLine;

            if (errorMsg.Length > 0)
                throw new Exception(errorMsg);

            CurrencyService currencyService = new CurrencyService();
            ICurrency currency = currencyService.GetCurrency(CurrencyId);
            if(currency == null)
                throw new Exception(String.Format("CurrencyId:{0} is not exist.", CurrencyId));

            ICurrency branchCurrency = currencyService.GetCurrency((new BranchService().GetBranch(expenseClaim.BranchId)).CurrencyId);

            decimal currencyExchangeRate = new CurrencyRateService(branchCurrency).GetRate(currency, TransactionDate);

            decimal localAmount = Decimal.Round(Amount * currencyExchangeRate, CashDecimalRoundingHelper.CashDecimals);

            if(expenseclaimda.AddExpenseClaimItem(ClaimId, CurrencyId, TransactionDate, CostCenter, GlCode, Description, Amount, 0, currencyExchangeRate, localAmount, UserId) > 0)
            {
                decimal TotalLocalAmount = expenseClaim.TotalLocalAmount + localAmount;
                expenseclaimda.UpdateExpenseClaimTotalLocalAmount(ClaimId, TotalLocalAmount, UserId);
                return true;
            }
            return false; 
        }
        public bool RemoveClaimItem(int ClaimItemId, int UserId)
        {
            //string errorMsg = "";
            IClaimItem expenseClaimItem = expenseclaimda.GetExpenseClaimItem(ClaimItemId);
            if (expenseClaimItem == null)
                //errorMsg += String.Format("ClaimItemId:{0} is not exist.", ClaimItemId) + Environment.NewLine;
                throw new Exception(String.Format("ClaimItemId:{0} is not exist.", ClaimItemId));

            IClaim expenseClaim = GetClaim(expenseClaimItem.ClaimId);

            if(UserId != expenseClaim.CreatedByUserId)
                //errorMsg += String.Format("Not authorized to remove item.", ClaimItemId) + Environment.NewLine;
                throw new Exception("Not authorized to remove item.");

            if (expenseClaim.Status != ClaimStatus.New && expenseClaim.Status != ClaimStatus.Rejected)
                throw new Exception(String.Format("Status:{0} is not allowed to update.", expenseClaim.Status.ToString()));

            if (expenseclaimda.DeleteExpenseClaimItem(ClaimItemId, UserId) > 0)
            {
                decimal TotalLocalAmount = expenseClaim.TotalLocalAmount - expenseClaimItem.LocalAmount;
                expenseclaimda.UpdateExpenseClaimTotalLocalAmount(expenseClaim.Id, TotalLocalAmount, UserId);
                return true;
            }
            else
                return false;
        }
        public bool SubmitClaim(int ClaimId, int UserId)
        {
            IClaim expenseClaim = GetClaim(ClaimId);

            if (UserId != expenseClaim.CreatedByUserId)
                throw new Exception("Not authorized to submit this claim.");

            if (expenseClaim.Status != ClaimStatus.New && expenseClaim.Status != ClaimStatus.Rejected)
                throw new Exception(String.Format("Status:{0} is not allowed to submit.", expenseClaim.Status.ToString()));

            if (expenseClaim.TotalLocalAmount <= 0)
                throw new Exception("Amount must be more than 0.00 to submit the from.");

            expenseclaimda.AddExpenseClaimSubmission(ClaimId, UserId);
            return expenseclaimda.UpdateExpenseClaim(ClaimId, (short)ClaimStatus.Submitted, UserId) > 0;
        }
    }
}
