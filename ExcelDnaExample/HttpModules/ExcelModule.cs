using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using log4net;
using Nancy;
using Nancy.Extensions;
using Newtonsoft.Json;
using ExcelInterop = NetOffice.ExcelApi;

namespace ExcelDnaExample.HttpModules
{
    public class Excel : ExcelHttpModule
    {
        private static readonly ILog Log = LogManager.
            GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        public Excel()
        {
            Get["/api/methods/listSheets"] = _ => CallExcelAsyncFunc(() =>
            {
                try
                {
                    var list = new List<string>();
                    foreach (var obj in AddIn.Excel.Sheets)
                    {
                        if (obj is ExcelInterop.Worksheet)
                        {
                            var sheet = (ExcelInterop.Worksheet) obj;
                            list.Add(sheet.Name);
                        }
                    }
                    return list;
                }
                catch (Exception ex)
                {
                    Log.Error("EXCEPTION", ex);
                    var r = (Response)ex.Message;
                    r.StatusCode = HttpStatusCode.InternalServerError;
                    return r;
                }
            });

            Post["/api/methods/selectSheet"] = _ => CallExcelAsyncFunc(() =>
            {
                try
                {
                    var json = Request.Body.AsString();
                    var info = JsonConvert.DeserializeObject<SheetInfo>(json);

                    var workbook = info.Workbook != null
                        ? AddIn.Excel.Workbooks[info.Workbook]
                        : AddIn.Excel.ActiveWorkbook;
                    workbook.Activate();

                    var sheet = (ExcelInterop.Worksheet)workbook.Sheets[info.Sheet];
                    sheet.Activate();

                    SetForegroundWindow((IntPtr)AddIn.Excel.Hwnd);

                    return null;
                }
                catch (Exception ex)
                {
                    Log.Error("EXCEPTION", ex);
                    var r = (Response)ex.Message;
                    r.StatusCode = HttpStatusCode.InternalServerError;
                    return r;
                }
            });
        }

        public class SheetInfo
        {
            public string Workbook { get; set; }
            public string Sheet { get; set; }
        }
    }
}
