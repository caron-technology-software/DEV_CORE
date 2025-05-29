using System;
using System.Runtime.Remoting.Contexts;

namespace Caron.Cradle.Control.HighLevel
{
    [Synchronization()]
    public class ZundConnection
    {
        public bool Status { get; set; } = false;
        public bool Error { get; set; } = false;
        public bool CradleCutterLock { get; set; } = false;
        public ZundConnection()
        {
            //--
        }
    }
}
