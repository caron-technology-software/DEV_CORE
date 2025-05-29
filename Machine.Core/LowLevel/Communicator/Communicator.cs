using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

using ProRob.Communication;

namespace Machine.Control.LowLevel
{
    public class Communicator
    {
        public static bool SendShutdownCommand()
        {
            var dataPacket = new DataPacket((byte)LowLevel.MachineCommand.Emergency);
            return TrySendDataPacket(Constants.Networking.IPAddressLowLevelControl, Constants.Networking.LowLevelControlTcpPort, dataPacket.Create());
        }

        private static bool TrySendDataPacket(string hostname, int port, byte[] dataPacket)
        {
            TcpClient tcpClient = null;
            NetworkStream stream = null;

            try
            {
                tcpClient = new TcpClient(hostname, port);
                stream = tcpClient.GetStream();

                byte[] receiveBuffer = new byte[UInt16.MaxValue];

                stream.Write(dataPacket, 0, dataPacket.Length);

                int nBytes = stream.Read(receiveBuffer, 0, receiveBuffer.Length);

                stream.Close();
                tcpClient.Close();

                return (nBytes > 0) ? true : false;
            }
            catch
            {
                stream?.Close();
                tcpClient?.Close();

                return false;
            }
        }
    }
}
