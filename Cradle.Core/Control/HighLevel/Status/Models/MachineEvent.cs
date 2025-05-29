using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caron.Cradle.Control.HighLevel
{
    public enum MachineEventType : byte
    {
        MachinePowerUp,
        MachinePowerDown,

        EnterLoadUnload,
        ExitLoadUnload,

        StartCutOff,
        StopCutOff,

        EnterSync,
        ExitSync,

        StartMovementInSync,
        StopMovementInSync
    }

    public class MachineEventsStatistics
    {
        public double TotalPowerOnTime { get; set; }
        public double TotalSyncTime { get; set; }
        public double TotalCutTime { get; set; }
        public double TotalLoadUnloadTime { get; set; }
        public double TotalMovementsInSyncTime { get; set; }

        public MachineEventsStatistics()
        {
            //--
        }
    }

    public class MachineEvent
    {
        private static Guid guidSession = Guid.Empty;

        public static void SetGuidSession(Guid guid)
        {
            guidSession = guid;
        }

        public Guid GuidSession { get; set; }
        public DateTime Timestamp { get; set; }
        public MachineEventType EventType { get; set; }

        public MachineEvent()
        {
            GuidSession = guidSession;
        }

        public MachineEvent(MachineEventType eventType) : this()
        {
            Timestamp = DateTime.Now;
            EventType = eventType;
        }
    }
}
