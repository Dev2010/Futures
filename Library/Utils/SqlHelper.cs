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

        public void Save(string spName, DataTable dataTable)
        {
            Logger.Debug(LogHelper.LogInfo(MethodBase.GetCurrentMethod(), spName));

            try
            {
                using (var sqlConnection = new SqlConnection(ConnectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(spName, sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;

                        foreach (DataRow dataRow in dataTable.Rows)
                        {
                            sqlCommand.Parameters.Clear();
                            foreach (DataColumn dataColumn in dataTable.Columns)
                            {
                                object fieldValue = dataRow[dataColumn];
                                if (dataColumn.DataType == typeof(Boolean))
                                {
                                    sqlCommand.Parameters.Add(SqlParameterUtility.GetSqlParameter(dataColumn.ColumnName, fieldValue, SqlDbType.Bit));
                                }
                                else if ((dataColumn.DataType == typeof(Char)) || (dataColumn.DataType == typeof(String)))
                                {
                                    sqlCommand.Parameters.Add(SqlParameterUtility.GetSqlParameter(dataColumn.ColumnName, fieldValue, SqlDbType.Char));
                                }
                                else if (dataColumn.DataType == typeof(DateTime))
                                {
                                    sqlCommand.Parameters.Add(SqlParameterUtility.GetSqlParameter(dataColumn.ColumnName, fieldValue, SqlDbType.DateTime));
                                }
                                else if ((dataColumn.DataType == typeof(Decimal)) || (dataColumn.DataType == typeof(Double)))
                                {
                                    sqlCommand.Parameters.Add(SqlParameterUtility.GetSqlParameter(dataColumn.ColumnName, fieldValue, SqlDbType.Decimal));
                                }
                                else if ((dataColumn.DataType == typeof(Int16)) || (dataColumn.DataType == typeof(Int32)) || (dataColumn.DataType == typeof(Int64))
                                    || (dataColumn.DataType == typeof(UInt16)) || (dataColumn.DataType == typeof(UInt32)) || (dataColumn.DataType == typeof(UInt64)))
                                {
                                    sqlCommand.Parameters.Add(SqlParameterUtility.GetSqlParameter(dataColumn.ColumnName, fieldValue, SqlDbType.BigInt));
                                }
                                else if ((dataColumn.DataType == typeof(Single)) || (dataColumn.DataType == typeof(TimeSpan)) || (dataColumn.DataType == typeof(SByte))
                                        || (dataColumn.DataType == typeof(Byte)) || (dataColumn.DataType == typeof(Byte[])))
                                {
                                    throw new DataTypeNotSupported();
                                }
                                else
                                {
                                    throw new DataTypeNotSupported();
                                }
                            }
                            
                            sqlCommand.ExecuteNonQuery();
                        }
                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception exception)
            {
                Logger.Error(exception);
            }
        }
    }
}
