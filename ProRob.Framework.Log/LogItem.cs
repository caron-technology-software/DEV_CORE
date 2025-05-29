using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LiteDB;

namespace ProRob.Log
{
    public class LogId
    {
        private static Guid guidSession = Guid.Empty;

        public Guid GuidSession { get; set; }
        public int Counter { get; set; }

        public static void SetGuidSession(Guid guid)
        {
            guidSession = guid;
        }

        public LogId()
        {
            GuidSession = GuidSession;
        }
    }

    public class LogItem
    {
        [BsonId]
        public LogId LogId { get; set; }
        public LogType LogType { get; set; }
        public DateTime Timestamp { get; set; }
        public string Log { get; set; }

        public LogItem()
        {
            LogId = new LogId();
        }

        public LogItem(string log, LogType logType) : this()
        {
            LogType = logType;
            Timestamp = DateTime.Now;
            Log = log;
        }

        public LogItem(string log) : this()
        {
            LogType = LogType;
            Timestamp = DateTime.Now;
            Log = log;
        }
    }
}
