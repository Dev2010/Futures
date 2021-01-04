using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utils;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using System.Collections.Generic;
using System.IO;

namespace UtilsTest
{
    [TestClass]
    public class ExcelHelperTest
    {
        [TestMethod]
        public void GetHeadersFromExcelSheetDataTestProdVersion()
        {
            Excel.Application excel = new Excel.Application();
            excel.DisplayAlerts = false;
            string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

            string dataDir = Path.Combine(projectDirectory, "data");
            string positionLimitCMEFilePath = Path.Combine(dataDir, "position-limits-cme.xlsx");
            Workbook xlBook = excel.Workbooks.Open(positionLimitCMEFilePath);
            Worksheet xlSheet = (Worksheet)xlBook.Worksheets[1];
            Range usedRange = xlSheet.UsedRange;
            object[,] data = usedRange.Value2;

            int startOfDataIndex;
            Dictionary<int, string> dtColumns = ExcelHelper.GetHeadersFromExcelSheetData("Contract Name", usedRange, data, out startOfDataIndex);

            Dictionary<int, string> dtExpectedColumns = new Dictionary<int, string>();
            dtExpectedColumns.Add(0, @"Contract Name");
            dtExpectedColumns.Add(1, @"Rule Chapter");
            dtExpectedColumns.Add(2, @"Commodity Code");
            dtExpectedColumns.Add(3, @"Contract Size");
            dtExpectedColumns.Add(4, @"Contract Units");
            dtExpectedColumns.Add(5, @"Type");
            dtExpectedColumns.Add(6, @"Settlement");
            dtExpectedColumns.Add(7, @"Group");
            dtExpectedColumns.Add(8, @"Diminishing Balance Contract");
            dtExpectedColumns.Add(9, @"Reporting Level");
            dtExpectedColumns.Add(10, @"Spot Month Position Comprised of Futures and Deliveries");
            dtExpectedColumns.Add(11, @"Spot Month Aggregate Into Futures Equivalent Leg (1)");
            dtExpectedColumns.Add(12, @"Spot Month Aggregate Into Futures Equivalent Leg (2)");
            dtExpectedColumns.Add(13, @"Spot-Month Aggregate Into Ratio Leg (1)");
            dtExpectedColumns.Add(14, @"Spot-Month Aggregate Into Ratio Leg (2)");
            dtExpectedColumns.Add(15, @"Spot-Month Accountability Level");
            dtExpectedColumns.Add(16, @"Daily Accountability Level (For Daily Contract)");
            dtExpectedColumns.Add(17, @"Initial Spot-Month Limit (In Net Futures Equivalents) Leg (1)/ Leg (2)");
            dtExpectedColumns.Add(18, @"Initial Spot-Month Limit Effective Date");
            dtExpectedColumns.Add(19, @"Spot-Month Limit (In Contract Units) Leg (1) / Leg (2)");
            dtExpectedColumns.Add(20, @"Subsequent Spot-Month Limit(s) (In Net Futures Equivalents)");
            dtExpectedColumns.Add(21, @"Subsequent Spot-Month Limit(s) Effective Date(s)");
            dtExpectedColumns.Add(22, @"Single Month Aggregate Into Futures Equivalent Leg (1)");
            dtExpectedColumns.Add(23, @"Single Month Aggregate Into Futures Equivalent Leg (2)");
            dtExpectedColumns.Add(24, @"Single Month Aggregate Into Ratio Leg (1)");
            dtExpectedColumns.Add(25, @"Single Month Aggregate Into Ratio Leg (2)");
            dtExpectedColumns.Add(26, @"Single Month Accountability Level Leg (1) / Leg (2)");
            dtExpectedColumns.Add(27, @"Single Month Limit (In Net Futures Equivalents) Leg (1) / Leg (2)");
            dtExpectedColumns.Add(28, @"All Month Aggregate Into Futures Equivalent Leg (1)");
            dtExpectedColumns.Add(29, @"All Month Aggregate Into Futures Equivalent Leg (2)");
            dtExpectedColumns.Add(30, @"All Month Aggregate Into Ratio Leg (1)");
            dtExpectedColumns.Add(31, @"All Month Aggregate Into Ratio Leg (2)");
            dtExpectedColumns.Add(32, @"All Month Accountability Level Leg (1) / Leg (2)");
            dtExpectedColumns.Add(33, @"All Month Limit (In Net Futures Equivalents) Leg (1) / Leg (2)");

            xlBook.Close();
            excel.Quit();

            Assert.IsTrue((new DictionaryEquality<int, string>()).Equals(dtColumns, dtExpectedColumns));
        }

        [TestMethod]
        public void GetHeadersFromExcelSheetDataTestEmptyCols()
        {
            Excel.Application excel = new Excel.Application();
            excel.DisplayAlerts = false;
            string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

            string dataDir = Path.Combine(projectDirectory, "data");
            string positionLimitCMEFilePath = Path.Combine(dataDir, "position-limits-cme-empty-cols.xlsx");
            Workbook xlBook = excel.Workbooks.Open(positionLimitCMEFilePath);
            Worksheet xlSheet = (Worksheet)xlBook.Worksheets[1];
            Range usedRange = xlSheet.UsedRange;
            object[,] data = usedRange.Value2;

            int startOfDataIndex;
            Dictionary<int, string> dtColumns = ExcelHelper.GetHeadersFromExcelSheetData("Contract Name", usedRange, data, out startOfDataIndex);

            Dictionary<int, string> dtExpectedColumns = new Dictionary<int, string>();
            dtExpectedColumns.Add(0, @"Contract Name");
            dtExpectedColumns.Add(1, @"Rule Chapter");
            dtExpectedColumns.Add(2, @"Commodity Code");
            dtExpectedColumns.Add(3, @"Contract Size");
            dtExpectedColumns.Add(6, @"Contract Units");
            dtExpectedColumns.Add(7, @"Type");
            dtExpectedColumns.Add(8, @"Settlement");
            dtExpectedColumns.Add(9, @"Group");
            dtExpectedColumns.Add(10, @"Diminishing Balance Contract");
            dtExpectedColumns.Add(11, @"Reporting Level");
            dtExpectedColumns.Add(12, @"Spot Month Position Comprised of Futures and Deliveries");
            dtExpectedColumns.Add(13, @"Spot Month Aggregate Into Futures Equivalent Leg (1)");
            dtExpectedColumns.Add(14, @"Spot Month Aggregate Into Futures Equivalent Leg (2)");
            dtExpectedColumns.Add(15, @"Spot-Month Aggregate Into Ratio Leg (1)");
            dtExpectedColumns.Add(16, @"Spot-Month Aggregate Into Ratio Leg (2)");
            dtExpectedColumns.Add(17, @"Spot-Month Accountability Level");
            dtExpectedColumns.Add(18, @"Daily Accountability Level (For Daily Contract)");
            dtExpectedColumns.Add(19, @"Initial Spot-Month Limit (In Net Futures Equivalents) Leg (1)/ Leg (2)");
            dtExpectedColumns.Add(23, @"Initial Spot-Month Limit Effective Date");
            dtExpectedColumns.Add(24, @"Spot-Month Limit (In Contract Units) Leg (1) / Leg (2)");
            dtExpectedColumns.Add(25, @"Subsequent Spot-Month Limit(s) (In Net Futures Equivalents)");
            dtExpectedColumns.Add(26, @"Subsequent Spot-Month Limit(s) Effective Date(s)");
            dtExpectedColumns.Add(27, @"Single Month Aggregate Into Futures Equivalent Leg (1)");
            dtExpectedColumns.Add(28, @"Single Month Aggregate Into Futures Equivalent Leg (2)");
            dtExpectedColumns.Add(29, @"Single Month Aggregate Into Ratio Leg (1)");
            dtExpectedColumns.Add(30, @"Single Month Aggregate Into Ratio Leg (2)");
            dtExpectedColumns.Add(31, @"Single Month Accountability Level Leg (1) / Leg (2)");
            dtExpectedColumns.Add(32, @"Single Month Limit (In Net Futures Equivalents) Leg (1) / Leg (2)");
            dtExpectedColumns.Add(33, @"All Month Aggregate Into Futures Equivalent Leg (1)");
            dtExpectedColumns.Add(34, @"All Month Aggregate Into Futures Equivalent Leg (2)");
            dtExpectedColumns.Add(35, @"All Month Aggregate Into Ratio Leg (1)");
            dtExpectedColumns.Add(36, @"All Month Aggregate Into Ratio Leg (2)");
            dtExpectedColumns.Add(37, @"All Month Accountability Level Leg (1) / Leg (2)");
            dtExpectedColumns.Add(42, @"All Month Limit (In Net Futures Equivalents) Leg (1) / Leg (2)");

            xlBook.Close();
            excel.Quit();

            Assert.IsTrue((new DictionaryEquality<int, string>()).Equals(dtColumns, dtExpectedColumns));
        }

        [TestMethod]
        public void GetDataSetFromExcelSheetTest()
        {

        }
    }
}
