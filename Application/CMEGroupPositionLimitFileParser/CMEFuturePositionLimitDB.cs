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
using Utils;

namespace CMEGroupPositionLimitFileParser
{
    public class CMEFuturePositionLimitDB
    {

        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public string ConnectionString { get; }
        public CMEFuturePositionLimitDB(string connectionString)
        {
            ConnectionString = connectionString;
        }

    }
}
