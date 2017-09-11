using System.Collections.Concurrent;
using System.Collections.Generic;
using ExcelDna.Integration;
using ExcelDnaExample.Services;
using Newtonsoft.Json;
using Excel = NetOffice.ExcelApi;

namespace ExcelDnaExample
{
    public class AddinServiceHost : IAddinServiceHost
    {
        public string GetSheetNames()
        {
            var o = CallExcelAsyncFunc(() =>
            {
                var excel = new Excel.Application(null, ExcelDnaUtil.Application);
                using (var workbook = excel.ActiveWorkbook)
                using (var sheets = workbook.Sheets)
                {
                    var list = new List<string>();

                    foreach (var obj in sheets)
                    {
                        if (!(obj is Excel.Worksheet sheet)) continue;
                        list.Add(sheet.Name);
                    }

                    return list;
                }
            });

            if (o is List<string> l)
            {
                return JsonConvert.SerializeObject(l);
            }

            return null;
        }

        public void SelectSheet(string name)
        {
            CallExcelAsyncProc((n) =>
            {
                var excel = new Excel.Application(null, ExcelDnaUtil.Application);
                using (var workbook = excel.ActiveWorkbook)
                using (var sheets = workbook.Sheets)
                {
                    var o = sheets[n];
                    if (o is Excel.Worksheet sheet)
                    {
                        sheet.Select();
                    }
                }
            }, name);
        }

        #region ExcelAsyncUtil.QueueAsMacro with BlockingCollection

        private delegate object ExcelAsyncFuncDelegate();
        private delegate object ExcelAsyncFuncWithObjectDelegate(object obj);
        private delegate void ExcelAsyncProcDelegate();
        private delegate void ExcelAsyncProcWithObjectDelegate(object obj);

        private static object CallExcelAsyncFunc(ExcelAsyncFuncWithObjectDelegate f, object obj)
        {
            var inQ = new BlockingCollection<object>();
            ExcelAsyncUtil.QueueAsMacro(objQ => ((BlockingCollection<object>)objQ).Add(f(obj)), inQ);
            return inQ.Take();
        }

        private static object CallExcelAsyncFunc(ExcelAsyncFuncDelegate f)
        {
            var inQ = new BlockingCollection<object>();
            ExcelAsyncUtil.QueueAsMacro(objQ => ((BlockingCollection<object>)objQ).Add(f()), inQ);
            return inQ.Take();
        }

        private static object CallExcelAsyncProc(ExcelAsyncProcWithObjectDelegate f, object obj)
        {
            var inQ = new BlockingCollection<object>();
            ExcelAsyncUtil.QueueAsMacro(objQ => { f(obj); ((BlockingCollection<object>)objQ).Add(null); }, inQ);
            return inQ.Take();
        }

        private static object CallExcelAsyncProc(ExcelAsyncProcDelegate f)
        {
            var inQ = new BlockingCollection<object>();
            ExcelAsyncUtil.QueueAsMacro(objQ => { f(); ((BlockingCollection<object>)objQ).Add(null); }, inQ);
            return inQ.Take();
        }

        #endregion
    }
}
