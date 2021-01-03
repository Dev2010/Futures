using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utils;

namespace UtilsTest
{
    [TestClass]
    public class DictionaryEqualityTest
    {
        [TestMethod]
        public void DictEquality()
        {
            Dictionary<int, string> d1 = new Dictionary<int, string>();
            d1.Add(1, "abc");
            d1.Add(20, "dfg");

            Dictionary<int, string> d2 = new Dictionary<int, string>();
            d2.Add(1, "abc");
            d2.Add(20, "dfg");

            Assert.IsTrue((new DictionaryEquality<int, string>()).Equals(d1, d2));

            d2.Clear();

            Assert.IsFalse((new DictionaryEquality<int, string>()).Equals(d1, d2));

            d2.Add(1, "abc");
            d2.Add(2, "dfg");

            Assert.IsFalse((new DictionaryEquality<int, string>()).Equals(d1, d2));

            d2.Clear();

            d2.Add(1, "abcd");
            d2.Add(20, "dfg");

            Assert.IsFalse((new DictionaryEquality<int, string>()).Equals(d1, d2));

            d2.Clear();

            d2.Add(20, "dfg");
            d2.Add(1, "abc");

            Assert.IsTrue((new DictionaryEquality<int, string>()).Equals(d1, d2));
        }
    }
}
