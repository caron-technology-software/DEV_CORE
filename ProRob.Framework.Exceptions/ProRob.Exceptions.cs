using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProRob;
using ProRob.Log;

namespace ProRob.Exceptions
{
    public class StackTraceFrame
    {
        public string Filename { get; set; }
        public string Method { get; set; }
        public int FileLineNumber { get; set; }
    }

    public class ExceptionDataLog
    {
        public DateTime DateTime { get; set; }
        public string Source { get; set; }
        public string Message { get; set; }
        public int HResult { get; set; }

        public List<StackTraceFrame> StackTraceFrames { get; set; } = new List<StackTraceFrame>();

        public ExceptionDataLog()
        {
            //--
        }

        public static ExceptionDataLog Compose(Exception e)
        {
            var exceptionDataLog = new ExceptionDataLog()
            {
                DateTime = DateTime.Now,
                Message = e.Message,
                Source = e.Source,
                HResult = e.HResult,
            };

            StackTrace st = new StackTrace(e, true);

            for (int i = 0; i < st.FrameCount; i++)
            {
                StackFrame sf = st.GetFrame(i);

                exceptionDataLog.StackTraceFrames.Add(new StackTraceFrame()
                {
                    Filename = sf.GetFileName(),
                    Method = sf.GetMethod().ToString(),
                    FileLineNumber = sf.GetFileLineNumber()
                });
            }

            return exceptionDataLog;
        }

        public override string ToString()
        {
            return Json.Serialize(this);
        }
    }

    public static class Handlers
    {
        public static string FirstChanceExceptionLoggerHandler(System.Runtime.ExceptionServices.FirstChanceExceptionEventArgs e)
        {
            try
            {
                if (e is null || e.Exception is null || string.IsNullOrEmpty(e.Exception.Source) || e.Exception.Source == "System")
                {
                    return null;
                }

                var exception = ExceptionDataLog.Compose(e.Exception);

                return Json.Serialize(exception);
            }
            catch
            {
                return null;
            }
        }
    }
}
