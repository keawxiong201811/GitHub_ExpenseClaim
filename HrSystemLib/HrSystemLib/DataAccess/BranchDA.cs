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
    internal class BranchDA : DataAccess
    {
        public List<IBranch> GetBranches()
        {
            DataTable dataTable = new DataTable();
            DbCommand dbCommand = _dbProvider.CreateCommand();
            dbConn.Open();
            dbCommand.Connection = dbConn;

            dbCommand.CommandText = "Branch_Sel";
            dbCommand.CommandType = CommandType.StoredProcedure;

            DbDataAdapter dbDataAdaptor = _dbProvider.CreateDataAdapter();
            dbDataAdaptor.SelectCommand = dbCommand;
            dbDataAdaptor.Fill(dataTable);

            if (dbConn.State == ConnectionState.Open)
                dbConn.Close();

            List<IBranch> Branches = null;
            if (dataTable != null & dataTable.Rows.Count > 0)
            {
                Branches = new List<IBranch>();
                foreach (DataRow dr in dataTable.Rows)
                {
                    Branch branch = new Branch();
                    branch.Code = Convert.ToString(dr["Code"]);
                    branch.CreatedByUserId = Convert.ToInt32(dr["CreatedByUserId"]);
                    branch.CreatedOnDate = Convert.ToDateTime(dr["CreatedOnDate"]);
                    branch.CurrencyId = Convert.ToInt32(dr["CurrencyId"]);
                    branch.Id = Convert.ToInt32(dr["Id"]);
                    branch.IsDeleted = Convert.ToBoolean(dr["IsDeleted"]);
                    branch.Name = Convert.ToString(dr["Name"]);
                    Branches.Add(branch);
                }
            }

            return Branches;
        }
    }
}
