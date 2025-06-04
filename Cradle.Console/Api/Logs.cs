using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;

using ProRob;
using ProRob.WebApi;
using ProRob.Log;

using Machine.DataCollections;

using Caron.Cradle.Control.DataCollections;
using Caron.Cradle.Control.HighLevel;
using System.Threading;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Caron.Cradle.Control.Api
{
    [ApiController]
    [Route("logs")]
    public class LogsController : CradleApiController
    {
        public LogsController()
        {
            Thread.CurrentThread.Priority = ThreadPriority.Lowest;
        }

        //GPIx6
        [HttpGet]
        //////[Route("control")]
        [Route("control")]
        //////public IHttpActionResult LogControl()
        //public IEnumerable<string> LogControl()
        public IEnumerable<string> LogControl(string DateX = null)
        {

            Console.WriteLine($"API LOG Inner DateX: {DateX})");

            if (string.IsNullOrEmpty(DateX))
            {
                //DateX = DateTime.Now.Date.ToString();
                DateX = DateTime.Now.ToString("yyyyMMdd");
            }

            string logName;          //string fileName;
            string logFolderPath;
            string LogFullPath;      //string localFilePath;
            int fileSize;

            #region Stop Logging
            Caron.Cradle.Control.MachineControllerApplication.ProLogTextWriterX?.Close();
            Caron.Cradle.Control.MachineControllerApplication.HighLevelLogX?.Stop();
            Caron.Cradle.Control.MachineControllerApplication.LowLevelLogX?.Stop();
            Caron.Cradle.Control.MachineControllerApplication.UiLogX?.Stop();
            Caron.Cradle.Control.MachineControllerApplication.ProLogTextWriterX?.Dispose();
            Action<string> OnWriteMethod1 = new Action<string>(Caron.Cradle.Control.MachineControllerApplication.DelegateOnWriteMethod);
            Caron.Cradle.Control.MachineControllerApplication.ConsoleRedirectWriterX.OnWrite -= OnWriteMethod1;
            Caron.Cradle.Control.MachineControllerApplication.HighLevelLogX = null;
            Caron.Cradle.Control.MachineControllerApplication.LowLevelLogX = null;
            Caron.Cradle.Control.MachineControllerApplication.UiLogX = null;
            Caron.Cradle.Control.MachineControllerApplication.ProLogTextWriterX = null;
            Caron.Cradle.Control.MachineControllerApplication.ConsoleRedirectWriterX = null;
            #endregion

            logFolderPath = Constants.Path.LogsFolder;
            logName = Constants.Path.Log.ControlLogFilename;
            //////LogFullPath = Path.Combine(logFolderPath, $"{DateTime.Now.ToString("yyyyMMdd")}_{logName}");
            LogFullPath = Path.Combine(logFolderPath, $"{DateX}_{logName}");

            //HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            //response.Content = new StreamContent(new FileStream(LogFullPath, FileMode.Open, FileAccess.Read));
            //response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            //response.Content.Headers.ContentDisposition.FileName = logName;
            ////response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            //response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/plain");

            //var logs = File.ReadAllLines(LogFullPath);
            string[] logs;
            if (System.IO.File.Exists(LogFullPath))
            {
                logs = System.IO.File.ReadAllLines(LogFullPath);
            }
            else
            {
                //in caso di errori nel parametro (formato yyyy-MM-dd):
                logs = new String[1] { "Parameter  ?DateX=  error (right format yyyy-MM-dd)" };
            }
            //GPFx6

            #region Start Logging
            Task.Run(() =>
            {
                int IntervalUpdate = 1000; //[ms]
                    Thread.Sleep(IntervalUpdate);

                Caron.Cradle.Control.MachineControllerApplication.HighLevelLogX = new ProLogLiteTXT(Constants.Path.LogsFolder, Constants.Path.Log.ControlLogFilename);
                Caron.Cradle.Control.MachineControllerApplication.Cradle.Log.LowLevel = Caron.Cradle.Control.MachineControllerApplication.HighLevelLogX;
                Caron.Cradle.Control.MachineControllerApplication.ProLogTextWriterX = new ProLogTextWriter(Caron.Cradle.Control.MachineControllerApplication.HighLevelLogX);
                Caron.Cradle.Control.MachineControllerApplication.ConsoleRedirectWriterX = new ConsoleRedirectWriter();
                    //Action<string> OnWriteMethod1 = new Action<string>(Caron.Cradle.Control.MachineControllerApplication.DelegateOnWriteMethod);
                    OnWriteMethod1 = new Action<string>(Caron.Cradle.Control.MachineControllerApplication.DelegateOnWriteMethod);
                Caron.Cradle.Control.MachineControllerApplication.ConsoleRedirectWriterX.OnWrite += OnWriteMethod1;
                Caron.Cradle.Control.MachineControllerApplication.UiLogX = new ProLogLiteTXT(Constants.Path.LogsFolder, Constants.Path.Log.UILogFilename);
                Caron.Cradle.Control.MachineControllerApplication.Cradle.Log.UI = Caron.Cradle.Control.MachineControllerApplication.UiLogX;
                Caron.Cradle.Control.MachineControllerApplication.LowLevelLogX = new ProLogLiteTXT(Constants.Path.LogsFolder, Constants.Path.Log.LowLevelControlLogFilename, addNewLine: true);
                Caron.Cradle.Control.MachineControllerApplication.Cradle.Log.LowLevel = Caron.Cradle.Control.MachineControllerApplication.LowLevelLogX;
            });
            #endregion

            //return response;
            return logs;
        }

        //////sostituisco Spreader con Cradle:
        #region Esportazione Log in files ZIP
        [HttpGet]
        [Route("stop_loggingX")]
        public void StopLoggingX()
        {
            ProConsole.WriteLine($"[API] StopLogging", ConsoleColor.Yellow);

            Caron.Cradle.Control.MachineControllerApplication.ProLogTextWriterX?.Close();
            Caron.Cradle.Control.MachineControllerApplication.HighLevelLogX?.Stop();
            Caron.Cradle.Control.MachineControllerApplication.LowLevelLogX?.Stop();
            Caron.Cradle.Control.MachineControllerApplication.UiLogX?.Stop();
            Caron.Cradle.Control.MachineControllerApplication.ProLogTextWriterX?.Dispose();

            //////distruggo le instanze con il garbage collector:
            //Caron.Cradle.Control.MachineControllerApplication.ConsoleRedirectWriterX.OnWrite -= delegate (string value)
            //{
            //    Caron.Cradle.Control.MachineControllerApplication.ProLogTextWriterX?.Write(value);
            //};
            Action<string> OnWriteMethod1 = new Action<string>(Caron.Cradle.Control.MachineControllerApplication.DelegateOnWriteMethod);
            //Action<string> OnWriteMethod2 = Caron.Cradle.Control.MachineControllerApplication.DelegateOnWriteMethod;
            Caron.Cradle.Control.MachineControllerApplication.ConsoleRedirectWriterX.OnWrite -= OnWriteMethod1;
            Caron.Cradle.Control.MachineControllerApplication.HighLevelLogX = null;
            Caron.Cradle.Control.MachineControllerApplication.LowLevelLogX = null;
            Caron.Cradle.Control.MachineControllerApplication.UiLogX = null;
            Caron.Cradle.Control.MachineControllerApplication.ProLogTextWriterX = null;

            //////mancava distruggere la classe che aveva l'evento += !!!:
            Caron.Cradle.Control.MachineControllerApplication.ConsoleRedirectWriterX = null;

        }

        [HttpGet]
        [Route("restart_loggingX")]
        public void RestartLoggingX()
        {
            ProConsole.WriteLine($"[API] RestartLogging", ConsoleColor.Yellow);

            Caron.Cradle.Control.MachineControllerApplication.HighLevelLogX = new ProLogLiteTXT(Constants.Path.LogsFolder, Constants.Path.Log.ControlLogFilename);
            Caron.Cradle.Control.MachineControllerApplication.Cradle.Log.LowLevel = Caron.Cradle.Control.MachineControllerApplication.HighLevelLogX;
            Caron.Cradle.Control.MachineControllerApplication.ProLogTextWriterX = new ProLogTextWriter(Caron.Cradle.Control.MachineControllerApplication.HighLevelLogX);
            Caron.Cradle.Control.MachineControllerApplication.ConsoleRedirectWriterX = new ConsoleRedirectWriter();
            //Caron.Cradle.Control.MachineControllerApplication.ConsoleRedirectWriterX.OnWrite += delegate (string value)
            //{
            //    Caron.Cradle.Control.MachineControllerApplication.ProLogTextWriterX?.Write(value);
            //};
            Action<string> OnWriteMethod1 = new Action<string>(Caron.Cradle.Control.MachineControllerApplication.DelegateOnWriteMethod);
            Caron.Cradle.Control.MachineControllerApplication.ConsoleRedirectWriterX.OnWrite += OnWriteMethod1;
            Caron.Cradle.Control.MachineControllerApplication.UiLogX = new ProLogLiteTXT(Constants.Path.LogsFolder, Constants.Path.Log.UILogFilename);
            Caron.Cradle.Control.MachineControllerApplication.Cradle.Log.UI = Caron.Cradle.Control.MachineControllerApplication.UiLogX;
            Caron.Cradle.Control.MachineControllerApplication.LowLevelLogX = new ProLogLiteTXT(Constants.Path.LogsFolder, Constants.Path.Log.LowLevelControlLogFilename, addNewLine: true);
            Caron.Cradle.Control.MachineControllerApplication.Cradle.Log.LowLevel = Caron.Cradle.Control.MachineControllerApplication.LowLevelLogX;
        }
        #endregion

        //private static ProLogLiteDB GetProLogger(string processLog)
        private static ProLogLiteTXT GetProLogger(string processLog)
        {
            switch (processLog)
            {
                case "low_level":
                    return MachineController.Log.LowLevel;

                case "high_level":
                    return MachineController.Log.HighLevel;

                case "ui":
                    return MachineController.Log.UI;
            }

            return null;
        }

        [HttpPost]
        [Route("{processLog}/add")]
        public void AddControlLog(string processLog, [FromBody] LogItem logItem)
        {
            //GetProLogger(processLog).AddLog(logItem);
            GetProLogger(processLog).AddLog(logItem.Log);
        }

    }

    /*                      //////controlli per lettura log da db ligthdb eliminati perchè c'è log di testo:
    [HttpGet]
    [Route("{processLog}/stats")]
    public object GetStats(string processLog)
    {
        //return GetProLogger(processLog).GetCurrentSessionStats();
        return null;
    }

    [HttpGet]
    [Route("{processLog}/session")]
    public IEnumerable<LogItem> GetCurrentSessionLogs(string processLog)
    {
        //ProConsole.WriteLine($"[API] GetCurrentSessionLogs", ConsoleColor.Yellow);

        return GetProLogger(processLog).GetCurrentSessionLogs();
    }

    [HttpGet]
    [Route("{processLog}/session/{logType}/html")]
    public HttpResponseMessage GetCurrentSessionLogsInHtmlFormat(string processLog, LogType logType = LogType.All)
    {
        //ProConsole.WriteLine($"[API] GetCurrentSessionLogsInHtmlFormat", ConsoleColor.Yellow);

        var logs = GetProLogger(processLog).GetCurrentSessionLogs(logType);

        if (logs is null || logs.Count() == 0)
        {
            return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent);
        }

        string title = $"[Log] {logType} - {processLog.ToUpper()} - {DateTime.Now.ToLongTimeString()} - {logs.FirstOrDefault().LogId.GuidSession}";

        var content = logs.ToHtmlPageString(title);

        var response = new HttpResponseMessage();
        response.Content = new StringContent(content);
        response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
        return response;
    }

    [HttpGet]
    [Route("{processLog}/last_session/{logType}/html")]
    public HttpResponseMessage GetLastSessionLogsInHtmlFormat(string processLog, LogType logType = LogType.All)
    {
        //ProConsole.WriteLine($"[API] GetCurrentSessionLogsInHtmlFormat", ConsoleColor.Yellow);

        var sessions = GetSessions(processLog);
        var sessionGuid = sessions.ElementAt(sessions.Count() - 1 - 1).SessionGuid;

        var logs = GetSessionLogs(processLog, sessionGuid, logType);

        if (logs is null || logs.Count() == 0)
        {
            return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent);
        }

        string title = $"[Log] {logType} - {processLog.ToUpper()} - {DateTime.Now.ToLongTimeString()} - {logs.FirstOrDefault().LogId.GuidSession}";
        var content = logs.ToHtmlPageString(title);

        var response = new HttpResponseMessage();
        response.Content = new StringContent(content);
        response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
        return response;
    }

    [HttpGet]
    [Route("{processLog}/{guidSession}/{logType}/html")]
    public HttpResponseMessage GetSessionLogsInHtmlFormat(string processLog, Guid guidSession, LogType logType = LogType.All)
    {
        //ProConsole.WriteLine($"[API] GetSessionLogsInHtmlFormat", ConsoleColor.Yellow);

        var logs = GetProLogger(processLog).GetSessionLogs(guidSession, logType);

        if (logs is null || logs.Count() == 0)
        {
            return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent);
        }

        string title = $"[Log] {logType} - {processLog.ToUpper()} - {DateTime.Now.ToLongTimeString()} - {logs.FirstOrDefault().LogId.GuidSession}";

        var content = logs.ToHtmlPageString(title);

        var response = new HttpResponseMessage();
        response.Content = new StringContent(content);
        response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
        return response;
    }

    [HttpGet]
    [Route("{processLog}/session")]
    public IEnumerable<LogItem> GetSessionLogs(string processLog, Guid guidSession, LogType logType)
    {
        //ProConsole.WriteLine($"[API] GetSessionLogs", ConsoleColor.Yellow);


        //var logs = GetProLogger(processLog).Collection
        //    .Query()
        //    .Where(x => x.LogId.GuidSession == guidSession)
        //    .Where(x => x.LogType == logType)
        //    .OrderBy(x => x.LogId.Counter)
        //    .ToEnumerable();


        //return logs;
        return null;
    }

    [HttpGet]
    [Route("{processLog}/sessions")]
    public IEnumerable<LogSession> GetSessions(string processLog)
    {
        //ProConsole.WriteLine($"[API] GetSessions", ConsoleColor.Yellow);


        //var log = GetProLogger(processLog);
        //var sessions = log.Collection.Query().Select(x => x.LogId.GuidSession).ToEnumerable().Distinct();

        //var results = new List<LogSession>();

        //foreach (var session in sessions)
        //{
        //    try
        //    {
        //        //var first = log.Collection
        //        //    .Query()
        //        //    .Where(x => x.LogId.GuidSession == session)
        //        //    .Where(x => x.LogId.Counter == 1).First();

        //        results.Add(new LogSession()
        //        {
        //            SessionGuid = session,
        //            //StartSession = first.Timestamp
        //        }); ;
        //    }
        //    catch
        //    {
        //        //--
        //    }
        //}

        //results.OrderBy(x => x.StartSession);


        //return results;
        return null;
    }

    [HttpGet]
    [Route("{processLog}/sessions/stats")]
    public object GetSessionsStats(string processLog)
    {
        //ProConsole.WriteLine($"[API] GetSessionsStats", ConsoleColor.Yellow);


        //var logs = GetProLogger(processLog).Collection.Query();

        //var stats = logs
        //    .ToEnumerable()
        //    .Where(x => x.LogType == LogType.ConsoleOutput)
        //    .GroupBy(
        //        logs => logs.LogId.GuidSession,
        //        logs => logs,
        //        (session, logs) => new
        //        {
        //            Date = logs.Min(x => x.Timestamp),
        //            Session = session,
        //            SessionLength = logs.Max(x => x.Timestamp) - logs.Min(x => x.Timestamp),
        //        })
        //    .OrderBy(x => x.Date);


        //return stats;
        return null;
    }
    */
}