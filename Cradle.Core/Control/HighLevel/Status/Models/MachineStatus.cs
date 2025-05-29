using System;
using System.Runtime.Remoting.Contexts;

using Caron.Cradle.Control.HighLevel.Settings;

namespace Caron.Cradle.Control.HighLevel
{
    [Synchronization()]
    public partial class MachineStatus
    {
        public Control.HighLevel.ControlState HighLevelControlState { get; set; }
        public object HighLevelControlSubState { get; set; }
        public Control.LowLevel.ControlState LowLevelControlState { get; set; }
        public JogState JogState { get; set; }
        public bool CutOffEnabled { get; set; }
        public bool InMarch { get; set; }
        public bool MachineStopped { get; set; }
        public bool CradleInSync { get; set; }
        public bool CradleSyncStatusBeforeSharpeningEnabled { get; set; }
        public bool ForceDisableCradleInSync { get; set; }
        public bool PromiseToDisableCradleInSync { get; set; }
        public bool PromiseToEnableCradleInSync { get; set; }
        public bool PromiseToEnableCradleInSyncAfterClick { get; set; }
        public bool SharpeningEnabled { get; set; }

        //GPI25 per accedervi usa:"HighLevel.Status.PhotocelMaterialPresenceFiltered"
        public bool PhotocelMaterialPresenceFiltered { get; set; }
        //GPF25

        //MMIx02
        public bool PhotocelRollPresenceFiltered { get; set; }
        //MMFx02

        public MachineStatus()
        {
            HighLevelControlState = Control.HighLevel.ControlState.Null;
            JogState = JogState.Stopped;
            SharpeningEnabled = false;

            CradleInSync = false;
            CradleSyncStatusBeforeSharpeningEnabled = false;
            ForceDisableCradleInSync = false;
            PromiseToDisableCradleInSync = false;
            PromiseToEnableCradleInSync = false;
            PromiseToEnableCradleInSyncAfterClick = false;//MMIx02
        }
    }
}
