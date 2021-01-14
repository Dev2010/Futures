using log4net;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class SqlHelper
    {

        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public string ConnectionString { get; }

        public SqlHelper(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public void Save(string spName, DataTable dataTable, string parameterName, string parameterTypeName)
        {
            Logger.Debug(LogHelper.LogInfo(MethodBase.GetCurrentMethod(), spName, parameterName, parameterTypeName));

            try
            {
                using (var sqlConnection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand(spName, sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;

                        var param = new SqlParameter(parameterName, SqlDbType.Structured);
                        param.TypeName = parameterTypeName;
                        param.Value = dataTable;

                        sqlCommand.Parameters.Add(param);

                        sqlConnection.Open();
                        sqlCommand.ExecuteNonQuery();
                        sqlConnection.Close();
                    }
                }
            }
            catch (Exception exception)
            {
                Logger.Error(exception);
            }
        }
    }
}
