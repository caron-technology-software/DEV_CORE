using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Machine.Utility
{
    class UdpLogger
    {
        static void Main(string[] args)
        {
            bool isRunning = true;

            //GPIx129 se il socket è in uso riutilizza l'indirizzo (porta):
            UdpClient udp = new UdpClient(Constants.Networking.LowLevelControlLoggerUdpPort);
            //udp.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

            IPEndPoint remoteIpEndPoint = new IPEndPoint(IPAddress.Parse(Constants.Networking.IPAddressLowLevelControl), Constants.Networking.LowLevelControlLoggerUdpPort);
            //GPFx129

            udp.Connect(remoteIpEndPoint);

            while (isRunning)
            {
                byte[] buffer = udp.Receive(ref remoteIpEndPoint);

                if (buffer.Length == 0)
                {
                    continue;
                }

                buffer = buffer.TakeWhile(x => x != 0).ToArray();
                string message = Encoding.ASCII.GetString(buffer);

                Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss.fff")}] {message}");
            }

            udp.Close();
        }
    }
}
