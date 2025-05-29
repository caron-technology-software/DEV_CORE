using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Machine.Control.LowLevel.MachineController
{
    public class MachineControllerManager
    {
        private const int ReceiveTimeout = 500; //[ms]

        public static bool SendHello()
        {
            return SendCommand(Command.Hello);
        }

        //GPIx5    //GPIx7
        public static bool ShutdownTwincat2()
        {
            return SendCommand(Command.ShutdownTwincat2);
        }
        //GPFx5    //GPIx7

        public static bool SendStartLowLevelControlProcess()
        {
            bool ret = SendCommand(Command.StartLowLevelControlProcess);
            Thread.Sleep(Machine.Constants.Intervals.WaitAfterStartLowLevelControlCommand);

            return ret;
        }

        private static bool SendCommand(Command command)
        {
            try
            {
                using (UdpClient udp = new UdpClient(Constants.Networking.LowLevelControlMachineManagerUdpPort))
                {
                    //GPIx129 se il socket è in uso riutilizza l'indirizzo (porta):
                    //udp.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                    IPEndPoint remoteIpEndPoint = new IPEndPoint(IPAddress.Parse(Constants.Networking.IPAddressLowLevelControl), Constants.Networking.LowLevelControlMachineManagerUdpPort);
                    //GPFx129
                    udp.Client.ReceiveTimeout = ReceiveTimeout;
                    udp.Connect(remoteIpEndPoint);

                    byte cmd = (byte)command;

                    Console.WriteLine("[LowLevelMachineManager] Sending command..");

                    udp.Send(new byte[] { 0x55, cmd, 0xAA }, 3);

                    Console.WriteLine("[LowLevelMachineManager] Receiving command..");

                    byte[] buffer = udp.Receive(ref remoteIpEndPoint);

                    if (buffer.Length == 0)
                    {
                        Console.WriteLine($"[LowLevelMachineManager] ERROR (buffer: {buffer.Length})");
                    }
                    else
                    {
                        Console.WriteLine($"[LowLevelMachineManager] [{DateTime.Now.ToString("HH:mm:ss.fff")}]");
                    }

                    udp.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e); 
                Console.WriteLine("{0} Exception Message.", e.Message);
                //Console.WriteLine("{0} Exception Message.", e.StackTrace);

                return false;
            }

            return true;
        }
    }
}
