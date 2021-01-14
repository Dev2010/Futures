using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomType
{
    // cme_future_limit_table_type
    public static class CustomTypeFactory
    {
        public static DataTable CMEFutureLimitTableType()
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add(new DataColumn("run_id", typeof(long)));

            return dataTable;
        }
    }
}
