using log4net;
using Microsoft.Office.Interop.Excel;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;
using DataColumn = System.Data.DataColumn;
using DataRow = System.Data.DataRow;
using System.Globalization;
using System.Collections.Specialized;
using System.Runtime.Remoting.Messaging;
using System.Text.RegularExpressions;

namespace Utils
{
    public static class ExcelHelper
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static void ConvertExcelSheetToCsv(string folder, IEnumerable<string> files)
        {
            Logger.Info(LogHelper.LogInfo(MethodBase.GetCurrentMethod(), folder, files.Count()));

            Excel.Application excel = new Excel.Application();
            try
            {
                excel.DisplayAlerts = false;

                foreach (string file in files)
                {
                    string fullyQualifiedFileName = Path.Combine(folder, file);
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file);
                    string csvFileName = String.Format($"{fileNameWithoutExtension}.csv");
                    string fullyQualifiedCsvFileName = Path.Combine(folder, csvFileName);

                    Logger.Info($"{fullyQualifiedFileName} to {fullyQualifiedCsvFileName}");

                    Excel.Workbook xlBook = excel.Workbooks.Open(fullyQualifiedFileName);
                    //Excel.Worksheet xlSheet = (Excel.Worksheet)xlBook.Worksheets["Sheet1"];
                    Excel.Worksheet xlSheet = (Excel.Worksheet)xlBook.Worksheets[1];
                    xlSheet.Select(Type.Missing);
                    xlBook.SaveAs(fullyQualifiedCsvFileName, Excel.XlFileFormat.xlCSV, Excel.XlSaveAsAccessMode.xlNoChange);
                    xlBook.Close(SaveChanges: false);

                }
            }
            catch(Exception exception)
            {
                Logger.Error(exception);
            }
            finally
            {
                excel.Quit();
            }
        }

        public static DataTable GetDataSetFromExcelSheet(string fileName, int index, string headerMarker, string footerMarker, Dictionary<string, string> columnSchema)
        {
            Logger.Info(LogHelper.LogInfo(MethodBase.GetCurrentMethod(), index));

            DataTable dataTable = null;

            Excel.Application excel = new Excel.Application();
            excel.DisplayAlerts = false;

            try
            {
                excel.DisplayAlerts = false;
                Excel.Workbook xlBook = excel.Workbooks.Open(fileName);
                Excel.Worksheet xlSheet = (Excel.Worksheet)xlBook.Worksheets[index];

                // Get range of the worksheet
                Range usedRange = xlSheet.UsedRange;
                object[,] data = usedRange.Value2;

                int startOfDataIndex;
                Dictionary<int, string> dtColumns = GetHeadersFromExcelSheetData(headerMarker, usedRange, data, out startOfDataIndex);
                Dictionary<int, string> dtDBColumns = MapColumnToSchema(columnSchema, dtColumns);
                bool footerFound = false;

                dataTable = GetDataTableSchema(dtDBColumns.Values.ToArray());
                #region parse
                int rowCount = 0;
                for (int rowOffset = startOfDataIndex; rowOffset <= usedRange.Rows.Count; rowOffset++)
                {
                    Dictionary<string, string> dtRowData = new Dictionary<string, string>();
                    if (footerFound) { break; }
                    foreach (int keyIndex in dtDBColumns.Keys)
                    {
                        int colIndex = keyIndex + 1; //excel is not 0 based

                        string cellValue = String.Empty;
                        try
                        {
                            cellValue = Convert.ToString(data[rowOffset, colIndex]);

                        }
                        catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException exception)
                        {
                            Logger.Error($"Row {rowOffset} - Column {colIndex}", exception);
                            //ConvertVal = (double)(data[rowCount, columnCount]);
                            //cellValue = ConvertVal.ToString();
                        }

                        string columnName = dtDBColumns[keyIndex];
                        string columnValue = string.Empty;
                        if (cellValue.StartsWith(footerMarker, true, CultureInfo.InvariantCulture))
                        {
                            Logger.Info($"Footer marker found {footerMarker}, ending read");
                            footerFound = true;
                            break;
                        }
                        else
                        {
                            if (!string.IsNullOrWhiteSpace(cellValue))
                            {
                                cellValue = Regex.Replace(cellValue, @"\p{C}+", string.Empty);
                                if (!string.IsNullOrWhiteSpace(cellValue))
                                {
                                    columnValue = cellValue;
                                }
                            }
                        }
                        dtRowData.Add(columnName, columnValue);
                    }
                    if (!footerFound)
                    {
                        DataHelper.AddDictonaryToDataTable(dataTable, dtRowData);
                        rowCount++;
                    }
                }
                #endregion

                xlBook.Close(false);
            }
            catch (Exception exception)
            {
                Logger.Error(exception);
            }
            finally
            {
                excel.Quit();
            }

            return dataTable;
        }

        private static DataTable GetDataTableSchema(string[] columns)
        {
            DataTable dt = new DataTable();
            foreach (string columnName in columns)
            {
                DataColumn Column = new DataColumn();
                Column.DataType = System.Type.GetType("System.String");
                Column.ColumnName = columnName;
                dt.Columns.Add(Column);
            }
            return dt;
        }

        private static Dictionary<int, string> MapColumnToSchema(Dictionary<string, string> columnDefinition, Dictionary<int, string> dtColumns)
        {
            Dictionary<int, string> dtDBColumns = new Dictionary<int, string>();

            List<string> parsedColumns = new List<string>(dtColumns.Values);
            List<string> definedColumns = new List<string>(columnDefinition.Keys);
            IEnumerable<string> nonintersect = parsedColumns.Except(definedColumns).Union(definedColumns.Except(parsedColumns));

            if (nonintersect.Count() > 0)
            {
                throw new ColumnMismatchException("File columns do not match column definition");
            }

            foreach (KeyValuePair<int, string> keyValuePair in dtColumns)
            {
                string dbColumnName = columnDefinition[keyValuePair.Value];
                int excelColumnIndex = keyValuePair.Key;
                dtDBColumns.Add(excelColumnIndex, dbColumnName);
            }

            return dtDBColumns;
        }

        public static Dictionary<int, string> GetHeadersFromExcelSheetData(string headerMarker, Range usedRange, object[,] data, out int startOfDataIndex )
        {
            bool headerRowFound = false;
            int headerColumnCount = 0;
            startOfDataIndex = 0;
            Dictionary<int, string> dtColumns = new Dictionary<int, string>();

            // get the headers and column count
            for (int rowCount = 1; rowCount <= usedRange.Rows.Count; rowCount++)
            {
                if (headerRowFound)
                {
                    Logger.Info("Header row has already passed, breaking");
                    break;
                }

                for (int columnCount = 1; columnCount <= usedRange.Columns.Count; columnCount++)
                {
                    string cellValue = String.Empty;
                    try
                    {
                        cellValue = Convert.ToString(data[rowCount, columnCount]);

                    }
                    catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException exception)
                    {
                        Logger.Error($"Row {rowCount} - Column {columnCount}", exception);
                        //ConvertVal = (double)(data[rowCount, columnCount]);
                        //cellValue = ConvertVal.ToString();
                    }

                    if (cellValue.StartsWith(headerMarker, true, CultureInfo.InvariantCulture) || headerRowFound)
                    {
                        if (!string.IsNullOrWhiteSpace(cellValue))
                        {
                            cellValue = Regex.Replace(cellValue, @"\p{C}+", string.Empty);
                            if (!string.IsNullOrWhiteSpace(cellValue))
                            {
                                dtColumns.Add(headerColumnCount, cellValue);
                                headerRowFound = true;
                                startOfDataIndex = rowCount + 1; // data starts from next row. this is a header row.
                                Logger.Info(cellValue);
                            }
                        }
                        headerColumnCount++;
                    }
                    else
                    {
                        Logger.Info("Skipping.. header row not found");
                        continue;
                    }
                }
            }
            
            return dtColumns;
        }
    }
}
