using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class SqlHelper
    {
        public string ConnectionString { get; }

        public SqlHelper(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public void Save(DataTable dataTable, string tableName )
        {

        }
    }
}
