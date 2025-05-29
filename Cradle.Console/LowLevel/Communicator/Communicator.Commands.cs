using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using ProRob;
using ProRob.Communication;

using Caron.Cradle.Control.LowLevel.Settings;
using Caron.Cradle.Control.HighLevel.Settings;

namespace Caron.Cradle.Control.LowLevel.Communication
{
    public partial class Communicator : IDisposable
    {
        public bool SendHelloMessage()
        {
            var dataPacket = new DataPacket((byte)Command.Hello);
            TrySendDataPacket(dataPacket.Create());
            return true;
        }

        //GPIx21
        public bool SetDigitalOutput(int index, bool value)
        {
            Console.WriteLine($"SetDigitalOutput({index},{value})");

            var dataPacket = new DataPacket((byte)Command.DigitalOutput, (byte)index);
            dataPacket.AddDataToPayload(value ? (int)1 : (int)0);
            return TrySendDataPacket(dataPacket.Create());
        }

        public bool SetDigitalOutput(DigitalOutput digitalOutput, bool value)
        {
            return SetDigitalOutput((byte)digitalOutput, value);
        }

        public bool SetEnableIOSettings(bool enable)  //EnableIOSettings
        {
            Console.WriteLine($"SetEnableIOSettings({enable})");

            var dataPacket = new DataPacket((byte)Command.EnableIOSettings, enable);
            return TrySendDataPacket(dataPacket.Create());
        }
        //GPFx21

        public bool SetStraightRoller(bool value)
        {
            value = !value;
            return SendCommand(Command.SetStraightRoller, value);
        }

        public bool SetScalingFactor(double scalingFactor)
        {
            return SetScalingFactor((float)scalingFactor);
        }

        public bool SetScalingFactor(float scalingFactor)
        {
            var dataPacket = new DataPacket((byte)Command.SetCradleScalingFactor);
            dataPacket.AddDataToPayload(scalingFactor);
            TrySendDataPacket(dataPacket.Create());
            return true;
        }

        public bool SetLowLevelControlState(LowLevel.ControlState state)
        {
            var dataPacket = new DataPacket((byte)Command.SetControlState, (byte)state);
            TrySendDataPacket(dataPacket.Create());
            return true;
        }

        public bool SetMachineLowLevelSettings(LowLevelMotionSettings lowLevelMotionSettings, FunctionsEnabled functionsEnabled, MachineParameters machineParameters)
        {
            var dataPacket = new DataPacket((byte)Command.SetMachineSettings);
            dataPacket.AddDataToPayload(Settings.ControlSettings.GetBytes(lowLevelMotionSettings, functionsEnabled, machineParameters));
            TrySendDataPacket(dataPacket.Create());
            return true;
        }

        #region Jog
        public bool StartJog(float velocity)
        {
            var dataPacket = new DataPacket((byte)Command.CradleJog, 0x01);
            dataPacket.AddDataToPayload(velocity);
            TrySendDataPacket(dataPacket.Create());
            return true;
        }

        public bool StopJog()
        {
            var dataPacket = new DataPacket((byte)Command.CradleJog, 0x00);
            TrySendDataPacket(dataPacket.Create());
            return true;
        }
        #endregion

        #region Actions

        //Migrated to Cradle.Devices.ElectricDrives

        #endregion
    }
}
