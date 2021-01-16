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
    public class SqlParameterUtility
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static SqlParameter GetSqlParameter(string sqlParameterName, object sqlParameterValue, SqlDbType sqlDbType)
        {
            SqlParameter sqlParameter = new SqlParameter(parameterName: sqlParameterName, value: sqlParameterValue);

            switch(sqlDbType)
            {
                case SqlDbType.Int:
                    sqlParameter.Value = Convert.ToInt64(sqlParameterValue);
                    break;
                case SqlDbType.DateTime:
                    sqlParameter.Value = Convert.ToDateTime(sqlParameterValue);
                    break;
                case SqlDbType.BigInt:
                    sqlParameter.Value = Convert.ToInt64(sqlParameterValue);
                    break;
                case SqlDbType.Char:
                case SqlDbType.NVarChar:
                case SqlDbType.VarChar:
                    sqlParameter.Value = Convert.ToString(sqlParameterValue);
                    break;
                case SqlDbType.Decimal:
                    sqlParameter.Value = Convert.ToDecimal(sqlParameterValue);
                    break;
                case SqlDbType.Float:
                    sqlParameter.Value = Convert.ToDouble(sqlParameterValue);
                    break;
                case SqlDbType.Bit:
                    sqlParameter.Value = Convert.ToBoolean(sqlParameterValue);
                    break;
                default:
                    throw new DataTypeNotSupported();
            }
            return sqlParameter;
        }
    }
}
