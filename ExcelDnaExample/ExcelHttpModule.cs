using System.Collections.Concurrent;
using ExcelDna.Integration;
using Nancy;

namespace ExcelDnaExample
{
    /// <summary>
    /// Base class for NancyModules with Excel support methods.
    /// </summary>
    public abstract class ExcelHttpModule : NancyModule
    {
        protected delegate object ExcelAsyncFuncWithObjectDelegate(object obj);
        protected delegate object ExcelAsyncFuncDelegate();
        protected delegate void ExcelAsyncProcWithObjectDelegate(object obj);
        protected delegate void ExcelAsyncProcDelegate();

        protected object CallExcelAsyncFunc(ExcelAsyncFuncWithObjectDelegate f, object obj)
        {
            var inQ = new BlockingCollection<object>();
            ExcelAsyncUtil.QueueAsMacro(objQ => ((BlockingCollection<object>)objQ).Add(f(obj)), inQ);
            return inQ.Take();
        }

        protected object CallExcelAsyncFunc(ExcelAsyncFuncDelegate f)
        {
            var inQ = new BlockingCollection<object>();
            ExcelAsyncUtil.QueueAsMacro(objQ => ((BlockingCollection<object>)objQ).Add(f()), inQ);
            return inQ.Take();
        }

        protected object CallExcelAsyncProc(ExcelAsyncProcWithObjectDelegate f, object obj)
        {
            var inQ = new BlockingCollection<object>();
            ExcelAsyncUtil.QueueAsMacro(objQ => { f(obj); ((BlockingCollection<object>)objQ).Add(null); }, inQ);
            return inQ.Take();
        }

        protected object CallExcelAsyncProc(ExcelAsyncProcDelegate f)
        {
            var inQ = new BlockingCollection<object>();
            ExcelAsyncUtil.QueueAsMacro(objQ => { f(); ((BlockingCollection<object>)objQ).Add(null); }, inQ);
            return inQ.Take();
        }
    }
}
