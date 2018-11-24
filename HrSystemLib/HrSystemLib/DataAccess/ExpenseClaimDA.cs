using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using HrSystemLib.Models.Interfaces;
using HrSystemLib.Models;

namespace HrSystemLib.DataAccess
{
    internal class ExpenseClaimDA : DataAccess
    {
        public List<IClaim> GetExpenseClaims()
        {
            DataTable dataTable = new DataTable();
            DbCommand dbCommand = _dbProvider.CreateCommand();
            dbConn.Open();
            dbCommand.Connection = dbConn;

            dbCommand.CommandText = "ExpenseClaim_Sel";
            dbCommand.CommandType = CommandType.Text;

            DbDataAdapter dbDataAdaptor = _dbProvider.CreateDataAdapter();
            dbDataAdaptor.SelectCommand = dbCommand;
            dbDataAdaptor.Fill(dataTable);

            if (dbConn.State == ConnectionState.Open)
                dbConn.Close();

            List<IClaim> ExpenseClaims = null;
            if (dataTable != null & dataTable.Rows.Count > 0)
            {
                ExpenseClaims = new List<IClaim>();
                foreach (DataRow dr in dataTable.Rows)
                {
                    ExpenseClaim expenseclaim = new ExpenseClaim();
                    expenseclaim.BankAccountName = Convert.ToString(dr["BankAccountName"]);
                    expenseclaim.BankAccountNo = Convert.ToString(dr["BankAccountNo"]);
                    expenseclaim.BankId = Convert.ToInt32(dr["BankId"]);
                    expenseclaim.BranchId = Convert.ToInt32(dr["BranchId"]);
                    expenseclaim.CreatedByUserId = Convert.ToInt32(dr["CreatedByUserId"]);
                    expenseclaim.CreatedOnDate = Convert.ToDateTime(dr["CReatedOnDate"]);
                    expenseclaim.Id = Convert.ToInt32(dr["Id"]);
                    expenseclaim.ModifiedByUserId = Convert.ToInt32(dr["ModifiedByUserId"]);
                    expenseclaim.ModifiedOnDate = Convert.ToDateTime(dr["ModifiedOnDate"]);
                    expenseclaim.Status = (ClaimStatus)Convert.ToInt16(dr["Status"]);
                    expenseclaim.TotalLocalAmount = Convert.ToDecimal(dr["TotalLocalAmount"]);
                    ExpenseClaims.Add(expenseclaim);
                }
            }

            return ExpenseClaims;
        }

        public List<IClaimItem> GetExpenseClaimItems(int ExpenseClaimId)
        {
            DataTable dataTable = new DataTable();
            DbCommand dbCommand = _dbProvider.CreateCommand();
            dbConn.Open();
            dbCommand.Connection = dbConn;

            dbCommand.CommandText = "ExpenseClaimItem_SelBy_ExpenseClaimId";
            dbCommand.CommandType = CommandType.StoredProcedure;
            AddParameterWithValue(dbCommand, "@ExpenseClaimId", ExpenseClaimId);

            DbDataAdapter dbDataAdaptor = _dbProvider.CreateDataAdapter();
            dbDataAdaptor.SelectCommand = dbCommand;
            dbDataAdaptor.Fill(dataTable);

            if (dbConn.State == ConnectionState.Open)
                dbConn.Close();

            List<IClaimItem> ExpenseClaimItems = null;
            if (dataTable != null & dataTable.Rows.Count > 0)
            {
                ExpenseClaimItems = new List<IClaimItem>();
                foreach (DataRow dr in dataTable.Rows)
                {
                    ExpenseClaimItem item = new ExpenseClaimItem();
                    item.Amount = Convert.ToDecimal(dr["Amount"]);
                    item.ClaimId = Convert.ToInt32(dr["ExpenseClaimId"]);
                    item.CostCenter = Convert.ToString(dr["CostCenter"]);
                    item.CurrencyId = Convert.ToInt32(dr["CurrencyId"]);
                    item.Description = Convert.ToString(dr["Description"]);
                    item.ExchangeRate = Convert.ToDecimal(dr["ExchangeRate"]);
                    item.GlCode = Convert.ToString(dr["GlCode"]);
                    item.Id = Convert.ToInt32(dr["Id"]);
                    item.LocalAmount = Convert.ToDecimal(dr["LocalAmount"]);
                    item.TransactionDate = Convert.ToDateTime(dr["TransactionDate"]);
                    ExpenseClaimItems.Add(item);
                }
            }

            return ExpenseClaimItems;
        }
        public IClaimItem GetExpenseClaimItem(int Id)
        {
            DataTable dataTable = new DataTable();
            DbCommand dbCommand = _dbProvider.CreateCommand();
            dbConn.Open();
            dbCommand.Connection = dbConn;

            dbCommand.CommandText = "ExpenseClaimItem_SelBy_Id";
            dbCommand.CommandType = CommandType.StoredProcedure;
            AddParameterWithValue(dbCommand, "@Id", Id);

            DbDataAdapter dbDataAdaptor = _dbProvider.CreateDataAdapter();
            dbDataAdaptor.SelectCommand = dbCommand;
            dbDataAdaptor.Fill(dataTable);

            if (dbConn.State == ConnectionState.Open)
                dbConn.Close();

            if (dataTable != null & dataTable.Rows.Count > 0)
            {
                DataRow dr = dataTable.Rows[0];
                ExpenseClaimItem item = new ExpenseClaimItem();
                item.Amount = Convert.ToDecimal(dr["Amount"]);
                item.ClaimId = Convert.ToInt32(dr["ExpenseClaimId"]);
                item.CostCenter = Convert.ToString(dr["CostCenter"]);
                item.CurrencyId = Convert.ToInt32(dr["CurrencyId"]);
                item.Description = Convert.ToString(dr["Description"]);
                item.ExchangeRate = Convert.ToDecimal(dr["ExchangeRate"]);
                item.GlCode = Convert.ToString(dr["GlCode"]);
                item.Id = Convert.ToInt32(dr["Id"]);
                item.LocalAmount = Convert.ToDecimal(dr["LocalAmount"]);
                item.TransactionDate = Convert.ToDateTime(dr["TransactionDate"]);
                return item;
            }

            return null;
        }
        public ISubmission GetExpenseClaimSubmission(int ExpenseClaimId)
        {
            DataTable dataTable = new DataTable();
            DbCommand dbCommand = _dbProvider.CreateCommand();
            dbConn.Open();
            dbCommand.Connection = dbConn;

            dbCommand.CommandText = "ExpenseClaimSubmission_SelBy_ExpenseClaimId";
            dbCommand.CommandType = CommandType.StoredProcedure;
            AddParameterWithValue(dbCommand, "@ExpenseClaimId", ExpenseClaimId);

            DbDataAdapter dbDataAdaptor = _dbProvider.CreateDataAdapter();
            dbDataAdaptor.SelectCommand = dbCommand;
            dbDataAdaptor.Fill(dataTable);

            if (dbConn.State == ConnectionState.Open)
                dbConn.Close();

            if (dataTable != null & dataTable.Rows.Count > 0)
            {
                DataRow dr = dataTable.Rows[0];
                ExpenseClaimSubmission submission = new ExpenseClaimSubmission();
                if(!DBNull.Value.Equals(dr["ActionId"])) submission.ActionId = Convert.ToInt16(dr["ActionId"]);
                if(!DBNull.Value.Equals(dr["ActionTakenByUserId"])) submission.ActionTakenByUserId = Convert.ToInt32(dr["ActionTakenByUserId"]);
                if (!DBNull.Value.Equals(dr["ActionTakenOnDate"])) submission.ActionTakenOnDate = Convert.ToDateTime(dr["ActionTakenOnDate"]);
                submission.ExpenseClaimId = Convert.ToInt32(dr["ExpenseClaimId"]);
                submission.Id = Convert.ToInt32(dr["Id"]);
                submission.SubmittedByUserId = Convert.ToInt32(dr["SubmittedByUserId"]);
                submission.SubmittedOnDate = Convert.ToDateTime(dr["SubmittedOnDate"]);
                return submission;
            }
            else 
                return null;
        }

        public int AddExpenseClaim(int BranchId, int BankId, string BankAccountNo, string BankAccountName, short Status, int CreatedByUserId)
        {
            DbCommand dbCommand = _dbProvider.CreateCommand();
            dbConn.Open();
            dbCommand.Connection = dbConn;

            dbCommand.CommandText = "ExpenseClaim_Ins";
            dbCommand.CommandType = CommandType.StoredProcedure;
            AddParameterWithValue(dbCommand, "@BranchId", BranchId);
            AddParameterWithValue(dbCommand, "@BankId", BankId);
            AddParameterWithValue(dbCommand, "@BankAccountNo", BankAccountNo);
            AddParameterWithValue(dbCommand, "@BankAccountName", BankAccountName);
            AddParameterWithValue(dbCommand, "@Status", Status);
            AddParameterWithValue(dbCommand, "@CreatedByUserId", CreatedByUserId);

            DbDataAdapter dbDataAdaptor = _dbProvider.CreateDataAdapter();
            dbDataAdaptor.InsertCommand = dbCommand;

            var result = Convert.ToInt32(dbDataAdaptor.InsertCommand.ExecuteScalar());
            if (dbConn.State == ConnectionState.Open)
                dbConn.Close();

            return result;
        }

        public int AddExpenseClaimItem(int ExpenseClaimId, int CurrencyId, DateTime TransactionDate, string CostCenter, string GlCode, string Description, decimal Amount, decimal GST, decimal ExchangeRate, decimal LocalAmount, int UserId)
        {
            DbCommand dbCommand = _dbProvider.CreateCommand();
            dbConn.Open();
            dbCommand.Connection = dbConn;

            dbCommand.CommandText = "ExpenseClaimItem_Ins";
            dbCommand.CommandType = CommandType.StoredProcedure;
            AddParameterWithValue(dbCommand, "@ExpenseClaimId", ExpenseClaimId);
            AddParameterWithValue(dbCommand, "@CurrencyId", CurrencyId);
            AddParameterWithValue(dbCommand, "@TransactionDate", TransactionDate);
            AddParameterWithValue(dbCommand, "@CostCenter", CostCenter);
            AddParameterWithValue(dbCommand, "@GlCode", GlCode);
            AddParameterWithValue(dbCommand, "@Description", Description);
            AddParameterWithValue(dbCommand, "@Amount", Amount);
            AddParameterWithValue(dbCommand, "@GST", GST);
            AddParameterWithValue(dbCommand, "@ExchangeRate", ExchangeRate);
            AddParameterWithValue(dbCommand, "@LocalAmount", LocalAmount);
            AddParameterWithValue(dbCommand, "@UserId", UserId);

            DbDataAdapter dbDataAdaptor = _dbProvider.CreateDataAdapter();
            dbDataAdaptor.InsertCommand = dbCommand;

            int result = Convert.ToInt32(dbDataAdaptor.InsertCommand.ExecuteScalar());

            if (dbConn.State == ConnectionState.Open)
                dbConn.Close();

            return result;
        }

        public int AddExpenseClaimSubmission(int ExpenseClaimId, int SubmittedByUserId)
        {
            DbCommand dbCommand = _dbProvider.CreateCommand();
            dbConn.Open();
            dbCommand.Connection = dbConn;

            dbCommand.CommandText = "ExpenseClaimSubmission_Ins";
            dbCommand.CommandType = CommandType.StoredProcedure;
            AddParameterWithValue(dbCommand, "@ExpenseClaimId", ExpenseClaimId);
            AddParameterWithValue(dbCommand, "@SubmittedByUserId", SubmittedByUserId);

            DbDataAdapter dbDataAdaptor = _dbProvider.CreateDataAdapter();
            dbDataAdaptor.InsertCommand = dbCommand;

            int result = dbDataAdaptor.InsertCommand.ExecuteNonQuery();

            if (dbConn.State == ConnectionState.Open)
                dbConn.Close();

            return result;
        }

        public int UpdateExpenseClaim(int Id, short Status, int ModifiedByUserId)
        {
            DbCommand dbCommand = _dbProvider.CreateCommand();
            dbConn.Open();
            dbCommand.Connection = dbConn;

            dbCommand.CommandText = "ExpenseClaim_Upd_Status";
            dbCommand.CommandType = CommandType.StoredProcedure;
            AddParameterWithValue(dbCommand, "@Id", Id);
            AddParameterWithValue(dbCommand, "@Status", Status);
            AddParameterWithValue(dbCommand, "@ModifiedByUserId", ModifiedByUserId);

            DbDataAdapter dbDataAdaptor = _dbProvider.CreateDataAdapter();
            dbDataAdaptor.UpdateCommand = dbCommand;

            int result = dbDataAdaptor.UpdateCommand.ExecuteNonQuery();

            if (dbConn.State == ConnectionState.Open)
                dbConn.Close();

            return result;
        }

        public int UpdateExpenseClaimTotalLocalAmount(int Id, decimal TotalLocalAmount, int ModifiedByUserId)
        {
            DbCommand dbCommand = _dbProvider.CreateCommand();
            dbConn.Open();
            dbCommand.Connection = dbConn;

            dbCommand.CommandText = "ExpenseClaim_Upd_TotalLocalAmount";
            dbCommand.CommandType = CommandType.StoredProcedure;
            AddParameterWithValue(dbCommand, "@Id", Id);
            AddParameterWithValue(dbCommand, "@TotalLocalAmount", TotalLocalAmount);
            AddParameterWithValue(dbCommand, "@ModifiedByUserId", ModifiedByUserId);

            DbDataAdapter dbDataAdaptor = _dbProvider.CreateDataAdapter();
            dbDataAdaptor.UpdateCommand = dbCommand;

            int result = dbDataAdaptor.UpdateCommand.ExecuteNonQuery();

            if (dbConn.State == ConnectionState.Open)
                dbConn.Close();

            return result;
        }

        public int DeleteExpenseClaimItem(int Id, int UserId)
        {
            DbCommand dbCommand = _dbProvider.CreateCommand();
            dbConn.Open();
            dbCommand.Connection = dbConn;

            dbCommand.CommandText = "ExpenseClaimItem_DelBy_Id";
            dbCommand.CommandType = CommandType.StoredProcedure;
            AddParameterWithValue(dbCommand, "@Id", Id);
            AddParameterWithValue(dbCommand, "@UserId", UserId);

            DbDataAdapter dbDataAdaptor = _dbProvider.CreateDataAdapter();
            dbDataAdaptor.DeleteCommand = dbCommand;

            int result = dbDataAdaptor.DeleteCommand.ExecuteNonQuery();

            if (dbConn.State == ConnectionState.Open)
                dbConn.Close();

            return result;
        }

        
    }
}
