using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Machine.Hardware.Motion;

namespace Caron.Cradle.Control.LowLevel
{
    public class MachineInfo
    {
        public volatile UInt32 Cycles;
        public volatile byte MachineState;
        //GPIx164
        public volatile byte EmergencyState;
        public volatile bool HeartBeatState;
        //GPFx164
        public volatile byte CutterState;
        public volatile byte CradleJogState;
        public volatile float Dancebar;

        public MachineInfo()
        {
            //--
        }
    }

    public class MachineIO
    {
        public volatile bool[] DigitalInputs;
        public volatile bool[] DigitalOutputs;
        public volatile bool[] MachineInputs;
        public volatile bool[] VirtualInputs;
        public volatile float[] AnalogInputs;

        public MachineIO()
        {
            DigitalInputs = new bool[Constants.Hardware.IO.NumberOfDigitalInputs];
            DigitalOutputs = new bool[Constants.Hardware.IO.NumberOfDigitalOutputs];
            MachineInputs = new bool[Constants.Hardware.IO.NumberOfMachineInputs];
            VirtualInputs = new bool[Constants.Hardware.IO.NumberOfVirtualInputs];
            AnalogInputs = new float[Constants.Hardware.IO.NumberOfAnalogInputs];
        }
    }

    public class MachineActions
    {
        public volatile bool SpoonUp;
        public volatile bool SpoonDown;
        public volatile bool AlignmentMotorSide;
        public volatile bool AlignmentOperatorSide;
        public volatile bool OverturningLoad;
        public volatile bool OverturningUnload;

        public MachineActions()
        {
            //--
        }
    }

    public class MachineAxes
    {
        public volatile MachineAxis Cradle = new MachineAxis();
        public volatile MachineAxisNoDriver Table = new MachineAxisNoDriver();
        public volatile MachineAxisNoDriverWithScalingFactorStatus TableScalable = new MachineAxisNoDriverWithScalingFactorStatus();
        public volatile MachineAxisNoEncoder Cutter = new MachineAxisNoEncoder();
        public volatile MachineAxisNoDriverAnalogEncoder Dancer = new MachineAxisNoDriverAnalogEncoder();
    }
}
