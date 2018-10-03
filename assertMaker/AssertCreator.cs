using System;
using System.Collections.Generic;

namespace assertMaker
{
    class AssertCreator
    {
        public static string CreateAssertion(List<object> _queryList, List<object> _columnNamesFromQuery)
        {
            int size = _queryList.Count;
            string format = "yyyy-MM-dd HH:mm:ss.fff";
            string completeAssertion = null;
            for (int i = 0; i < size; i++)
            {
                Type currentIteration = _queryList[i].GetType();
                if (currentIteration.Name == "SqlInt32")
                    completeAssertion += "Assert.AreEqual(" + _queryList[i] + ", oDtb(0)." + _columnNamesFromQuery[i] + ")" + "\n";
                else if (currentIteration.Name == "SqlString")
                    completeAssertion += "Assert.AreEqual(\"" + _queryList[i] + "\", oDtb(0)." + _columnNamesFromQuery[i] + ")" + "\n";
                else if (currentIteration.Name == "SqlDateTime")
                    completeAssertion += "Assert.AreEqual(Date.Parse(\"" + DateTime.Parse(_queryList[i].ToString()).ToString(format) + "\"), oDtb(0)." + _columnNamesFromQuery[i] + ")" + "\n";
                else if (currentIteration.Name == "SqlDecimal")
                    completeAssertion += "Assert.AreEqual(Decimal.Parse(\"" + _queryList[i] + "\"), oDtb(0)." + _columnNamesFromQuery[i] + ")" + "\n";
                else
                    completeAssertion += "Assert.AreEqual(" + _queryList[i] + ", oDtb(0)." + _columnNamesFromQuery[i] + ")" + "\n";
            }
            return completeAssertion;
        }

        public static string CreateAssertion2(List<object> _queryList)
        {
            int size = _queryList.Count;
            string format = "yyyy-MM-dd HH:mm:ss.fff";
            string completeAssertion = null;
            for (int i = 0; i < size; i++)
            {
                Type currentIteration = _queryList[i].GetType();
                if (currentIteration.Name == "SqlInt32")
                    completeAssertion += "Assert.AreEqual(" + _queryList[i] + ", oDtb.Rows(0).ItemArray(" + i + "))" + "\n";
                else if (currentIteration.Name == "SqlString")
                    completeAssertion += "Assert.AreEqual(\"" + _queryList[i] + "\", oDtb.Rows(0).ItemArray(" + i + "))" + "\n";
                else if (currentIteration.Name == "SqlDateTime")
                    completeAssertion += "Assert.AreEqual(Date.Parse(\"" + DateTime.Parse(_queryList[i].ToString()).ToString(format) + "\"), oDtb.Rows(0).ItemArray(" + i + "))" + "\n";
                else if (currentIteration.Name == "SqlDecimal")
                    completeAssertion += "Assert.AreEqual(Decimal.Parse(\"" + _queryList[i] + "\"), oDtb.Rows(0).ItemArray(" + i + "))" + "\n";
                else
                    completeAssertion += "Assert.AreEqual(" + _queryList[i] + ", oDtb.Rows(0).ItemArray(" + i + "))" + "\n";
            }
            return completeAssertion;
        }
    }
}
