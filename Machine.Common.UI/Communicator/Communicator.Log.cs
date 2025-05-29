using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using ProRob.Log;

namespace Machine.UI.Communication
{
    public partial class Communicator
    {
        public static void AddLowLevelLog(LogItem logItem)
        {
            AddLog("low_level", logItem);
        }

        public static void AddHighLevelLog(LogItem logItem)
        {
            AddLog("high_level", logItem);
        }

        public static void AddUILog(LogItem logItem)
        {
            AddLog("ui", logItem);
        }

        private static void AddLog(string process, LogItem logItem)
        {
            if (string.IsNullOrEmpty(logItem.Log))
            {
                return;
            }

            Task.Run(() =>
            {
                try
                {
                    SendHttpPostRequest($"logs/{process}/add", logItem);
                }
                catch
                {
                    Errors++;
                }
            });
        }
    }
}