using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utils;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using System.Collections.Generic;

namespace UtilsTest
{
    [TestClass]
    public class ExcelHelperTest
    {
        [TestMethod]
        public void GetHeadersFromExcelSheetDataTest()
        {
            Excel.Application excel = new Excel.Application();
            excel.DisplayAlerts = false;

            excel.DisplayAlerts = false;
            Workbook xlBook = excel.Workbooks.Open(@".\data\position-limits-cme.xlsx");
            Worksheet xlSheet = (Worksheet) xlBook.Worksheets[1];

            // Get range of the worksheet
            Range usedRange = xlSheet.UsedRange;
            object[,] data = usedRange.Value2;

            SortedDictionary<int, string> dtColumns = ExcelHelper.GetHeadersFromExcelSheetData("Contract Name", usedRange, data);

            excel.Quit();
        }
    }
}
