using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProRob.Log
{
    public class ProLogTextWriter : TextWriter
    {
        private readonly IProLog log;
        public ProLogTextWriter(IProLog log)
        {
            this.log = log;
        }

        public void Write(string value, LogType logType = LogType.ConsoleOutput)
        {
            log.AddLog(value, logType);
        }

        public override void Write(string value)
        {
            log.AddLog(value);
        }

        public override Encoding Encoding
        {
            get { return Encoding.Unicode; }
        }
    }
}
