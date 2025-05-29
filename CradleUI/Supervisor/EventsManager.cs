using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Machine.Events;

using Caron.Cradle.Control.HighLevel;
using Caron.Cradle.Control.HighLevel.Settings;
using Caron.Cradle.Control.LowLevel;

namespace Caron.Cradle.UI
{
    public class EventsManager
    {
        //----------------------------------
        // EventInvoker
        //----------------------------------
        private EventInvoker<bool> cradleSyncInvoker = new EventInvoker<bool>(TimeSpan.FromMilliseconds(10));
        private EventInvoker<bool> machineStoppedChangeInvoker = new EventInvoker<bool>(TimeSpan.FromMilliseconds(10));
        private EventInvoker<bool> cutOffEnabledInvoker = new EventInvoker<bool>(TimeSpan.FromMilliseconds(10));
        private EventInvoker<WorkingMode> workingModeChangeInvoker = new EventInvoker<WorkingMode>(TimeSpan.FromMilliseconds(10));
        private EventInvoker<byte> lowLevelChangeStateInvoker = new EventInvoker<byte>(TimeSpan.FromMilliseconds(50));
        private EventInvoker<byte> highLevelChangeStateInvoker = new EventInvoker<byte>(TimeSpan.FromMilliseconds(50));
        private EventInvoker<HighLevelSettings> rootSettingsInvoker = new EventInvoker<HighLevelSettings>(TimeSpan.FromMilliseconds(500));
        private EventInvoker<WorkingContext> workingContextInvoker = new EventInvoker<WorkingContext>(TimeSpan.FromMilliseconds(150));
        private EventInvoker<WorkingsSettings> workingsSettingsInvoker = new EventInvoker<WorkingsSettings>(TimeSpan.FromMilliseconds(150));
        private EventInvoker<WorkingParameters> workingParametersInvoker = new EventInvoker<WorkingParameters>(TimeSpan.FromMilliseconds(150));
        private EventInvoker<Guid> guidCurrentWorkingParameterSetInvoker = new EventInvoker<Guid>(TimeSpan.FromMilliseconds(150));

        private EventInvoker<bool> titanLimitChangeInvoker = new EventInvoker<bool>(TimeSpan.FromMilliseconds(10));
        private EventInvoker<bool> cradleLimitChangeInvoker = new EventInvoker<bool>(TimeSpan.FromMilliseconds(10));
        private EventInvoker<bool> alignmentLimitChangeInvoker = new EventInvoker<bool>(TimeSpan.FromMilliseconds(10));
        private EventInvoker<bool> workingProgressInvoker = new EventInvoker<bool>(TimeSpan.FromMilliseconds(10));
        private EventInvoker<bool> marchEnabledInvoker = new EventInvoker<bool>(TimeSpan.FromMilliseconds(10));

        //----------------------------------
        // EventHandler
        //----------------------------------
        public event EventHandler SystemLocalizationChanged;

        public event EventHandler CradleSyncChanged;

        public event EventHandler MarchEnabledChanged;

        public event EventHandler CutOffEnabledChanged;
        public event EventHandler MachineStoppedChanged;
        public event EventHandler WorkingModeChanged;

        public event EventHandler LowLevelMachineStateChanged;
        public event EventHandler HighLevelMachineStateChanged;

        public event EventHandler WorkingContextInvoker;
        public event EventHandler RootSettingsChanged;
        public event EventHandler WorkingsSettingsChanged;
        public event EventHandler WorkingParametersChanged;
        public event EventHandler GuidCurrentWorkingParameterSetChanged;

        public event EventHandler TitanLimitChanged;
        public event EventHandler OverturningLimitChanged;
        public event EventHandler AligmentLimitChanged;
        public event EventHandler WorkingProgressChanged;

        public EventsManager()
        {
            //--
        }

        private void OnValueChanged(EventHandler o, EventArgs e)
        {
            o?.Invoke(this, e);
        }

        private void OnUpdated(EventHandler o, EventArgs e)
        {
            o?.Invoke(this, e);
        }

        private void InvokeEvent(EventHandler eventHandler)
        {
            if (eventHandler != null)
            {
                eventHandler.Invoke(this, new EventArgs());
            }
        }

        public void InvokeSystemLocalizationEvent()
        {
            InvokeEvent(SystemLocalizationChanged);
        }

        public void Check(Control.ControlStatus control)
        {
            if (control is null)
            {
                return;
            }

            //-------------------------------------------------------
            //Limits
            //-------------------------------------------------------
            bool titanLimit = control.LowLevel.IO.DigitalInputs[(byte)DigitalInput.TitanLimit];

            bool cl1 = control.LowLevel.IO.DigitalInputs[(byte)DigitalInput.LimitOverturningMotorSideLoad];
            bool cl2 = control.LowLevel.IO.DigitalInputs[(byte)DigitalInput.LimitOverturningOperatorSideLoad];
            bool cl3 = control.LowLevel.IO.DigitalInputs[(byte)DigitalInput.LimitOverturningMotorSideUnload];
            bool cl4 = control.LowLevel.IO.DigitalInputs[(byte)DigitalInput.LimitOverturningOperatorSideUnload];
            bool cradleLimit = cl1 || cl2 || cl3 || cl4;

            bool al1 = control.LowLevel.IO.DigitalInputs[(byte)DigitalInput.LimitAlignmentMotorSide];
            bool al2 = control.LowLevel.IO.DigitalInputs[(byte)DigitalInput.LimitAlignmentOperatorSide];
            bool alignmentLimit = al1 || al2;

            //-------------------------------------------------------
            //UI Update Events
            //-------------------------------------------------------
            lowLevelChangeStateInvoker.InvokeEventIfObjectChanged(control.LowLevel.Info.MachineState, LowLevelMachineStateChanged);
            highLevelChangeStateInvoker.InvokeEventIfObjectChanged(control.LowLevel.Info.MachineState, HighLevelMachineStateChanged);
            workingModeChangeInvoker.InvokeEventIfObjectChanged(control.HighLevel.WorkingContext.Parameters.WorkingMode, WorkingModeChanged);
            machineStoppedChangeInvoker.InvokeEventIfObjectChanged(control.HighLevel.Status.MachineStopped, MachineStoppedChanged);
            cutOffEnabledInvoker.InvokeEventIfObjectChanged(control.HighLevel.Status.CutOffEnabled, CutOffEnabledChanged);
            rootSettingsInvoker.InvokeEventIfObjectChanged(control.HighLevel.Settings.HighLevel, RootSettingsChanged);
            workingContextInvoker.InvokeEventIfObjectChanged(control.HighLevel.WorkingContext, WorkingContextInvoker);

            workingsSettingsInvoker.InvokeEventIfObjectChanged(control.HighLevel.WorkingsSettings, WorkingsSettingsChanged);
            workingParametersInvoker.InvokeEventIfObjectChanged(control.HighLevel.WorkingContext.Parameters, WorkingParametersChanged);
            guidCurrentWorkingParameterSetInvoker.InvokeEventIfObjectChanged(control.HighLevel.WorkingContext.CurrentGuidWorkingParameterSet, GuidCurrentWorkingParameterSetChanged);

            cradleSyncInvoker.InvokeEventIfObjectChanged(control.HighLevel.Status.CradleInSync, CradleSyncChanged);

            alignmentLimitChangeInvoker.InvokeEventIfObjectChanged(alignmentLimit, AligmentLimitChanged);
            cradleLimitChangeInvoker.InvokeEventIfObjectChanged(cradleLimit, OverturningLimitChanged);
            titanLimitChangeInvoker.InvokeEventIfObjectChanged(titanLimit, TitanLimitChanged);

            workingProgressInvoker.InvokeEventIfObjectChanged(control.HighLevel.WorkingStatus.InProgress, WorkingProgressChanged);
            marchEnabledInvoker.InvokeEventIfObjectChanged(control.LowLevel.IO.MachineInputs[(byte)MachineInput.MarchEnabled], MarchEnabledChanged);


            // EXAMPLE: multiple events
            /*if (spreadTaskChangedInvoker.IsObjectChanged || machineSettingsChangedInvoker.IsObjectChanged || autoProgrammingDataChangedInvoker.IsObjectChanged)
            {
                OnUpdated(PictureAutoProgrammingUpdate, new EventArgs());
            }*/

        }
    }
}
