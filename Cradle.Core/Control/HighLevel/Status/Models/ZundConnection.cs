using System;

namespace Caron.Cradle.Control.HighLevel
{
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
