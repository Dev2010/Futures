using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CMEPositionLimitFileParserTest
{
    [TestClass]
    public class DictionaryEqualityTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            Dictionary<int, string> d1 = new Dictionary<int, string>();
            d1.Add(1, "abc");
            d1.Add(20, "dfg");

            Dictionary<int, string> d2 = new Dictionary<int, string>();
            d1.Add(1, "abc");
            d1.Add(20, "dfg");


        }
    }
}
