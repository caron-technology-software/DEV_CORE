using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProRob.Log
{
    public interface IProLog
    {
        void AddLog(string value, LogType logType = LogType.ConsoleOutput);
        void AddLog(string value);
    }
}
