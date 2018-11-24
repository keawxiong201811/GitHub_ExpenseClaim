using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HrSystemLib.Services;
using HrSystemLib.Services.Interfaces;
using HrSystemLib.Models.Interfaces;
using HrSystemLib.Models;

namespace ExpenseClaim.Controllers
{
    [Authorize]
    public class ExpenseClaimController : Controller
    {
        public ActionResult NewExpenseClaimForm()
        {
            return ToExpenseClaimNew("");
        }

        public ActionResult MyExpenseClaimForms()
        {
            ViewBag.Title = "My Expense Claim Forms";
            IClaimService claimserv = new ExpenseClaimService();
            List<IClaim> claims = claimserv.ListClaims(WebMatrix.WebData.WebSecurity.CurrentUserId);
            if (claims == null || claims.Count == 0)
                ViewBag.Message = "No record found.";
            ViewBag.Claims = claims;
            return View("ExpenseClaimList");
        }

        [HttpPost]
        public ActionResult CreateExpenseClaim(HrSystemLib.Models.ExpenseClaim ExpenseClaim)
        {
            try
            {
                IClaimService claimserv = new ExpenseClaimService();
                int ClaimId = claimserv.CreateClaim(ExpenseClaim.BranchId, ExpenseClaim.BankId, ExpenseClaim.BankAccountNo, ExpenseClaim.BankAccountName, WebMatrix.WebData.WebSecurity.CurrentUserId);
                if (ClaimId <= 0)
                    return ToExpenseClaimNew("Failed to create expense claim.");
                else
                {
                    ViewBag.ExpenseClaim = claimserv.GetClaim(ClaimId);
                    ViewBag.Message = "Expense claim successfully created.";
                }
            }
            catch(Exception ex)
            {
                return ToExpenseClaimNew(ex.Message);
            }
            return View("ExpenseClaimDetail");
        }

        [HttpPost]
        public ActionResult AddExpenseClaimItem(HrSystemLib.Models.ExpenseClaimItem ClaimItem)
        {
            try
            {
                ClaimItem.ClaimId = (int)TempData["ClaimId"];
                IClaimService claimserv = new ExpenseClaimService();
                bool itemAdded = claimserv.AddClaimItem(ClaimItem.ClaimId, ClaimItem.CurrencyId,
                                                       ClaimItem.TransactionDate,
                                                       ClaimItem.CostCenter,
                                                       ClaimItem.GlCode,
                                                       ClaimItem.Description,
                                                       ClaimItem.Amount,
                                                       WebMatrix.WebData.WebSecurity.CurrentUserId);
                if (!itemAdded)
                    return ToExpenseClaimDetail(ClaimItem.ClaimId, "Failed to add claim item.");
                else
                    return ToExpenseClaimDetail(ClaimItem.ClaimId, "Claim item is successfully added.");
            }
            catch(Exception ex)
            {
                return ToExpenseClaimDetail(ClaimItem.ClaimId, ex.Message);
            }
        }

        public ActionResult RemoveExpenseClaimItem(int Id)
        {
            int ClaimId = (int)TempData["ClaimId"];
            try
            {
                IClaimService claimserv = new ExpenseClaimService();
                if(!claimserv.RemoveClaimItem(Id, WebMatrix.WebData.WebSecurity.CurrentUserId))
                    return ToExpenseClaimDetail(ClaimId, "Failed to remove claim item.");
                else
                    return ToExpenseClaimDetail(ClaimId, "Claim item is successfully removed.");
            }
            catch(Exception ex)
            {
                return ToExpenseClaimDetail(ClaimId, ex.Message);
            }
        }

        public ActionResult SubmitExpenseClaimFrom(int Id)
        {
            try
            {
                IClaimService claimserv = new ExpenseClaimService();
                if (!claimserv.SubmitClaim(Id, WebMatrix.WebData.WebSecurity.CurrentUserId))
                    return ToExpenseClaimDetail(Id, "Failed to submit claim.");
                else
                    return ToExpenseClaimDetail(Id, "Claim item is successfully submitted.");
            }
            catch (Exception ex)
            {
                return ToExpenseClaimDetail(Id, ex.Message);
            }
        }
        public ActionResult ViewExpenseClaimForm(int Id)
        {
            return ToExpenseClaimDetail(Id, "");
        }
        private ViewResult ToExpenseClaimDetail(int ClaimId, string message)
        {
            ViewBag.Title = "Expense Claim Form";
            ViewBag.Message = message;
            BindCurrenciesViewBag();
            IClaimService claimserv = new ExpenseClaimService();
            IBranchService branchserv = new BranchService();
            IBankService bankserv = new BankService();
            IClaim Claim = claimserv.GetClaim(ClaimId);
            IBranch branch = branchserv.GetBranch(Claim.BranchId);
            IBank bank = bankserv.GetBank(Claim.BankId);
            if (Claim == null)
                return View("Error");
            //else
            //    ViewBag.Claim = Claim;
            TempData["ClaimId"] = Claim.Id;
            ViewBag.BankDesc = String.Format("{0} - {1}", bank.Code, bank.Name);
            ViewBag.BranchDesc = String.Format("{0} - {1}", branch.Code, branch.Name);
            ViewBag.Claim = Claim;
            return View("ExpenseClaimDetail");
        }
        private ViewResult ToExpenseClaimNew(string message)
        {
            ViewBag.Title = "New Expense Claim Form";
            ViewBag.Message = message;
            BindBranchesViewBag();
            BindBanksViewBag();
            return View("ExpenseClaimNew");
        }

        private void BindBranchesViewBag()
        {
            IBranchService branchserv = new BranchService();
            List<IBranch> branches = branchserv.ListBranches();
            ViewBag.Branches = branches.Select(b => new SelectListItem() { Text = String.Format("{0} - {1}", b.Code, b.Name), Value = b.Id.ToString() });
        }
        private void BindBanksViewBag()
        {
            IBankService bankserv = new BankService();
            List<IBank> banks = bankserv.ListBanks();
            ViewBag.Banks = banks.Select(b => new SelectListItem() { Text = String.Format("{0} - {1}", b.Code, b.Name), Value = b.Id.ToString() });
        }
        private void BindCurrenciesViewBag()
        {
            ICurrencyService currencyserv = new CurrencyService();
            List<ICurrency> currencies = currencyserv.ListCurrencies();
            ViewBag.Currencies = currencies.Select(c => new SelectListItem() { Text = String.Format("{0} - {1}", c.Code, c.Description), Value = c.Id.ToString() });
        }
        public ActionResult SubmittedExpenseClaimForms()
        {
            ViewBag.Title = "My Expense Claim Forms";

            return View("ExpenseClaimList");
        }
    }
}
