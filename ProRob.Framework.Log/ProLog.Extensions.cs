using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProRob.Log
{
    public static class ProLoggerExtension
    {
        public static string ToHtmlPageString(this IEnumerable<LogItem> items, string title = "Log")
        {
            var sb = new StringBuilder();

            sb.AppendLine($"<html>\n<body>\n<pre>");
            sb.AppendLine($"<h1>{title}</h1>");

            foreach (var item in items)
            {
                sb.AppendLine($"<font color=\"{GetColor(item)}\">[#{item.LogId.Counter:000} {item.Timestamp.ToString("HH:mm:ss.fff")}]</font> {item.Log}");
            }

            sb.AppendLine($"</pre>");
            sb.AppendLine("<script>\nwindow.scrollTo(0, document.body.scrollHeight);\n</script>");
            sb.AppendLine("</body>\n</html>");

            return sb.ToString();
        }

        private static string GetColor(LogItem item)
        {
            switch (item.LogType)
            {
                case LogType.ConsoleOutput:
                    return "blue";

                case LogType.Exception:
                    return "orange";

                case LogType.IrreversibleException:
                    return "red";

                default:
                    return "blue";
            }
        }

        public static object GetCurrentSessionStats(this ProLogLiteDB proLogger)
        {
            var sessionLogs = proLogger.GetCurrentSessionLogs();

            var stats = sessionLogs.GroupBy(
                logs => logs.LogType,
                logs => logs,
                (type, logs) => new
                {
                    Type = type,
                    NumberOfElements = logs.Count()
                });

            return stats;
        }

        public static IEnumerable<Guid> GetSessions(this ProLogLiteDB proLogger)
        {
            var sessions = proLogger.Collection.Query()
                .Select(x => x.LogId.GuidSession)
                .ToEnumerable()
                .Distinct();

            return sessions;
        }

        public static IEnumerable<object> GetSessionsStats(this ProLogLiteDB proLogger)
        {
            var sessionsLogs = proLogger.Collection.Query().ToEnumerable();

            var stats = sessionsLogs
                .Where(x => x.LogType == LogType.ConsoleOutput)
                .GroupBy(
                    logs => logs.LogId.GuidSession,
                    logs => logs,
                    (session, logs) => new
                    {
                        Date = logs.Min(x => x.Timestamp),
                        Session = session,
                        SessionLength = logs.Max(x => x.Timestamp) - logs.Min(x => x.Timestamp),
                    })
                .OrderBy(x => x.Date);

            return stats;
        }
    }
}
