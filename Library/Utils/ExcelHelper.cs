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

        public static void GetDataSetFromExcelSheet(string fileName, int index, string headerMarker, string footerMarker)
        {
            Logger.Info(LogHelper.LogInfo(MethodBase.GetCurrentMethod(), index));

            Excel.Application excel = new Excel.Application();
            excel.DisplayAlerts = false;

            try
            {
                excel.DisplayAlerts = false;
                Excel.Workbook xlBook = excel.Workbooks.Open(fileName);
                Excel.Worksheet xlSheet = (Excel.Worksheet)xlBook.Worksheets[index];

                DataTable dataTable = new DataTable();

                // Get range of the worksheet
                Range usedRange = xlSheet.UsedRange;
                object[,] data = usedRange.Value2;

                SortedDictionary<int, string>  dtColumns = GetHeadersFromExcelSheetData(headerMarker, usedRange, data);

            }
            catch (Exception exception)
            {
                Logger.Error(exception);
            }
            finally
            {
                excel.Quit();
            }
        }

        public static SortedDictionary<int, string> GetHeadersFromExcelSheetData(string headerMarker, Range usedRange, object[,] data)
        {
            bool headerRowFound = false;
            int headerColumnCount = 0;

            SortedDictionary<int, string> dtColumns = new SortedDictionary<int, string>();
            //var Column = new DataColumn();
            //Column.DataType = System.Type.GetType("System.String");
            //Column.ColumnName = count.ToString();
            //dataTable.Columns.Add(Column);

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
                        Logger.Error(exception);
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

                    //if (cellValue.StartsWith(footerMarker, true, CultureInfo.InvariantCulture))
                    //{
                    //    Logger.Info(cellValue);
                    //    Logger.Info("footer row found, stopping read");
                    //    footerFound = true;
                    //    break;
                    //}

                    //DataRow Row;

                    //// Add to the DataTable
                    //if (columnCount == 1)
                    //{

                    //    Row = dataTable.NewRow();
                    //    Row[columnCount.ToString()] = cellValue;
                    //    dataTable.Rows.Add(Row);
                    //}
                    //else
                    //{

                    //    Row = dataTable.Rows[rowCount + 1];
                    //    Row[columnCount.ToString()] = cellValue;

                    //}
                }
            }

            // columns
            //foreach (KeyValuePair<int, string> keyValuePair in dtColumns)
            //{
            //    Logger.Info($"{keyValuePair.Key} - {keyValuePair.Value}");
            //}

            return dtColumns;
        }
    }
}
