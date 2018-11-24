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
    internal class CurrencyDA : DataAccess
    {
        public List<ICurrency> GetCurrencies()
        {
            DataTable dataTable = new DataTable();
            DbCommand dbCommand = _dbProvider.CreateCommand();
            dbConn.Open();
            dbCommand.Connection = dbConn;

            dbCommand.CommandText = "Currency_Sel";
            dbCommand.CommandType = CommandType.StoredProcedure;

            DbDataAdapter dbDataAdaptor = _dbProvider.CreateDataAdapter();
            dbDataAdaptor.SelectCommand = dbCommand;
            dbDataAdaptor.Fill(dataTable);

            if (dbConn.State == ConnectionState.Open)
                dbConn.Close();

            List<ICurrency> Currencies = null;
            if (dataTable != null & dataTable.Rows.Count > 0)
            {
                Currencies = new List<ICurrency>();
                foreach (DataRow dr in dataTable.Rows)
                {
                    Currency currency = new Currency();
                    currency.Code = Convert.ToString(dr["Code"]);
                    currency.CreatedByUserId = Convert.ToInt32(dr["CreatedByUserId"]);
                    currency.CreatedOnDate = Convert.ToDateTime(dr["CreatedOnDate"]);
                    currency.Description = Convert.ToString(dr["Description"]);
                    currency.Id = Convert.ToInt32(dr["Id"]);
                    currency.IsDeleted = Convert.ToBoolean(dr["IsDeleted"]);
                    Currencies.Add(currency);
                }
            }

            return Currencies;
        }
    }
}
