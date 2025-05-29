using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProRob.Log
{
    public class LogSession
    {
        public Guid SessionGuid { get; set; }
        public int NumberOfElements { get; set; }
        public DateTime StartSession { get; set; }
        public DateTime StopSession { get; set; }
        public TimeSpan Duration => StopSession - StartSession;
        public LogSession()
        {
            //--
        }
    }
}
