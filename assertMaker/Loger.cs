using System;
using System.IO;

namespace assertMaker
{
    class Loger
    {
        public static void ErrorLogging(Exception ex, string strPath)
        {
            if (!File.Exists(strPath))
            {
                File.Create(strPath).Dispose();
            }
            using (StreamWriter sw = File.AppendText(strPath))
            {
                sw.WriteLine("=============Error Logging ===========");
                sw.WriteLine("===========Start============= " + DateTime.Now);
                sw.WriteLine("Error Message: " + ex.Message);
                sw.WriteLine("Stack Trace: " + ex.StackTrace);
                sw.WriteLine("===========End============= " + DateTime.Now);
                sw.WriteLine("\n");
            }
        }
        public static void AssertionLogging(string log, string strPath)
        {
            if (!File.Exists(strPath))
            {
                File.Create(strPath).Dispose();
            }
            using (StreamWriter sw = File.AppendText(strPath))
            {
                sw.WriteLine("=============Assertion Logging ===========");
                sw.WriteLine("===========Start============= " + DateTime.Now);
                sw.WriteLine(log);
                sw.WriteLine("===========End============= " + DateTime.Now);
                sw.WriteLine("\n");
            }
        }
    }
}
