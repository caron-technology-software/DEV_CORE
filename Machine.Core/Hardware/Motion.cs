using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Machine.Hardware.Motion
{
    public class MachineAxis
    {
        public volatile float Position;
        public volatile float Velocity;
        public volatile byte Status;
        public volatile float PositionError;
        public volatile float MaxPositionError;
        public volatile short DriverCommandSpeed;
        public volatile float ProportionalAction;
        public volatile float IntegrativeAction;
        public volatile float DerivativeAction;
        public volatile float FeedForwardAction;
        public volatile float CradleMotorSpeedCommand;

        public MachineAxis()
        {
            //--
        }
    }

    public class MachineAxisNoDriverWithScalingFactorStatus
    {
        public volatile float Position;
        public volatile float Velocity;
        public volatile float ScalingFactor;

        public MachineAxisNoDriverWithScalingFactorStatus()
        {
            //--
        }
    }

    public class MachineAxisNoDriver
    {
        public volatile float Position;
        public volatile float Velocity;
        public MachineAxisNoDriver()
        {
            //--
        }
    }

    public class MachineAxisNoEncoder
    {
        public volatile byte Status;
        public MachineAxisNoEncoder()
        {
            //--
        }
    }

    public class MachineAxisNoDriverAnalogEncoder
    {
        public volatile float NormalizedValue;
        public MachineAxisNoDriverAnalogEncoder()
        {
            //--
        }
    }
}
