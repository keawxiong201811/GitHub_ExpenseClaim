using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.Common;
using HrSystemLib.Models.Interfaces;
using HrSystemLib.Models;

namespace HrSystemLib.DataAccess
{
    internal class BankDA : DataAccess
    {
        public List<IBank> GetBanks()
        {
            DataTable dataTable = new DataTable();
            DbCommand dbCommand = _dbProvider.CreateCommand();
            dbConn.Open();
            dbCommand.Connection = dbConn;

            dbCommand.CommandText = "Bank_Sel";
            dbCommand.CommandType = CommandType.StoredProcedure;

            DbDataAdapter dbDataAdaptor = _dbProvider.CreateDataAdapter();
            dbDataAdaptor.SelectCommand = dbCommand;
            dbDataAdaptor.Fill(dataTable);

            if (dbConn.State == ConnectionState.Open)
                dbConn.Close();

            List<IBank> Banks = null;
            if (dataTable != null & dataTable.Rows.Count > 0)
            {
                Banks = new List<IBank>();
                foreach(DataRow dr in dataTable.Rows)
                {
                    Bank bank = new Bank();
                    bank.Id = Convert.ToInt32(dr["Id"]);
                    bank.Code = Convert.ToString(dr["Code"]);
                    bank.CreatedByUserId = Convert.ToInt32(dr["CreatedByUserId"]);
                    bank.CreatedOnDate = Convert.ToDateTime(dr["CreatedOnDate"]);
                    bank.IsDeleted = Convert.ToBoolean(dr["IsDeleted"]);
                    bank.Name = Convert.ToString(dr["Name"]);
                    Banks.Add(bank);
                }
            }

            return Banks;
        }
    }
}
