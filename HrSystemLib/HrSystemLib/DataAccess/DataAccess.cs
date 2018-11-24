using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Configuration;
using HrSystemLib.Helper;

namespace HrSystemLib.DataAccess
{
    internal abstract class DataAccess
    {
        protected DbProviderFactory _dbProvider;
        protected DbConnection dbConn;
        public DataAccess()
        {
            this._dbProvider = DbProviderFactories.GetFactory(ConfigurationManager.ConnectionStrings[DataAccessHelper.DefaultConnectionStringConfig].ProviderName);
            this.dbConn = _dbProvider.CreateConnection();
            this.dbConn.ConnectionString = ConfigurationManager.ConnectionStrings[DataAccessHelper.DefaultConnectionStringConfig].ConnectionString;
        }
        protected void AddParameterWithValue(DbCommand command, string parameterName, object parameterValue)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = parameterName;
            parameter.Value = parameterValue;
            command.Parameters.Add(parameter);
        } 
    }
}
