using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProRob.Log
{
    [Flags]
    public enum LogType
    {
        //None = 0,
        ConsoleOutput = 1,
        Exception = 2,
        IrreversibleException = 4,
        All = LogType.ConsoleOutput | LogType.Exception | LogType.IrreversibleException
    }
}
