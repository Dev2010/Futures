using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTable = System.Data.DataTable;
using DataColumn = System.Data.DataColumn;
using DataRow = System.Data.DataRow;

namespace Utils
{
    public static class DataHelper
    {
        public static bool Equals(DataTable dt1, DataTable dt2)
        {
            if ((dt1.Columns.Count != dt2.Columns.Count) || (dt1.Rows.Count != dt2.Rows.Count))
            {
                return false;
            }

            List<string> columnNames1 = dt1.Columns.Cast<DataColumn>()
                                 .Select(x => x.ColumnName)
                                 .ToList();

            List<string> columnNames2 = dt1.Columns.Cast<DataColumn>()
                                 .Select(x => x.ColumnName)
                                 .ToList();

            IEnumerable<string> nonintersect = columnNames1.Except(columnNames2).Union(columnNames2.Except(columnNames1));
            if (nonintersect.Count() != 0)
            {
                return false;
            }

            for (int rowCount = 0; rowCount < dt1.Rows.Count; rowCount++)
            {
                foreach (string columnName in columnNames1)
                {
                    int c1 = dt1.Columns[columnName].Ordinal;
                    string s1 = dt1.Rows[rowCount][c1].ToString();
                    if (string.IsNullOrWhiteSpace(s1))
                    {
                        s1 = string.Empty;
                    }

                    s1 = s1.Trim();

                    int c2 = dt2.Columns[columnName].Ordinal;
                    string s2 = dt2.Rows[rowCount][c2].ToString();
                    if (string.IsNullOrWhiteSpace(s2))
                    {
                        s2 = string.Empty;
                    }

                    s2 = s2.Trim();

                    Console.WriteLine($"{s1} / {s2}");
                    if (s1 != s2)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        public static void AddDictonaryToDataTable(DataTable dataTable, Dictionary<string, string> dtRowData)
        {
            // check if all values are empty
            bool empty = true;
            foreach (KeyValuePair<string, string> keyValuePair in dtRowData)
            {
                if (! string.IsNullOrWhiteSpace(keyValuePair.Value))
                {
                    empty = false;
                }
            }

            if (empty)
            {
                // nothing to add
                return;
            }

            DataRow Row = dataTable.NewRow();
            foreach (KeyValuePair<string, string> keyValuePair in dtRowData)
            {
                Row[keyValuePair.Key] = keyValuePair.Value;
            }
            dataTable.Rows.Add(Row);
        }
    }
}
