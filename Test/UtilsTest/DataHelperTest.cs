using System;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utils;

namespace UtilsTest
{
    [TestClass]
    public class DataHelperTest
    {
        [TestMethod]
        public void TestEquals()
        {
            DataTable t1 = new DataTable();
            t1.Columns.Add("Id", typeof(int));
            t1.Columns.Add("Account", typeof(string));
            t1.Columns.Add("Ticker", typeof(string));
            t1.Columns.Add("Price", typeof(decimal));
            t1.Columns.Add("TradeDate", typeof(DateTime));

            // Step 3: here we add rows.
            t1.Rows.Add(25, "ABC", "GOOG", 1253.25, "1/2/2020");
            t1.Rows.Add(26, "EFG", "MSFT", 202.569, "1/3/2020");
            t1.Rows.Add(27, "AHU", "A", 12.58, "1/4/2020");
            t1.Rows.Add(29, "AJI", "SHOP", 600.56, "1/5/2020");

            DataTable t2 = new DataTable();
            t2.Columns.Add("Id", typeof(int));
            t2.Columns.Add("Account", typeof(string));
            t2.Columns.Add("Ticker", typeof(string));
            t2.Columns.Add("Price", typeof(decimal));
            t2.Columns.Add("TradeDate", typeof(DateTime));

            // Step 3: here we add rows.
            t2.Rows.Add(25, "ABC", "GOOG", 1253.25, "1/2/2020");
            t2.Rows.Add(26, "EFG", "MSFT", 202.569, "1/3/2020");
            t2.Rows.Add(27, "AHU", "A", 12.58, "1/4/2020");
            t2.Rows.Add(29, "AJI", "SHOP", 600.56, "1/5/2020");

            Assert.IsTrue(DataHelper.Equals(t1, t2));

        }

        [TestMethod]
        public void TestEqualsFail()
        {
            DataTable t1 = new DataTable();
            t1.Columns.Add("Id", typeof(int));
            t1.Columns.Add("Account", typeof(string));
            t1.Columns.Add("Ticker", typeof(string));
            t1.Columns.Add("Price", typeof(decimal));
            t1.Columns.Add("TradeDate", typeof(DateTime));

            // Step 3: here we add rows.
            t1.Rows.Add(25, "ABC", "GOOG", 1253.25, "1/2/2020");
            t1.Rows.Add(26, "EFG", "MSFT", 202.569, "1/3/2020");
            t1.Rows.Add(27, "AHU", "A", 12.58, "1/4/2020");
            t1.Rows.Add(29, "AJI", "SHOP", 600.57, "1/5/2020");

            DataTable t2 = new DataTable();
            t2.Columns.Add("Id", typeof(int));
            t2.Columns.Add("Account", typeof(string));
            t2.Columns.Add("Ticker", typeof(string));
            t2.Columns.Add("Price", typeof(decimal));
            t2.Columns.Add("TradeDate", typeof(DateTime));

            // Step 3: here we add rows.
            t2.Rows.Add(25, "ABC", "GOOG", 1253.25, "1/2/2020");
            t2.Rows.Add(26, "EFG", "MSFT", 202.569, "1/3/2020");
            t2.Rows.Add(27, "AHU", "A", 12.58, "1/4/2020");
            t2.Rows.Add(29, "AJI", "SHOP", 600.56, "1/5/2020");

            Assert.IsFalse(DataHelper.Equals(t1, t2));

        }
    }
}
