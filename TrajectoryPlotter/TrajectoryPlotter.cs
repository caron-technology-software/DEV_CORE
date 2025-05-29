using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Newtonsoft.Json;

using ProRob;
using ProRob.Motion;
using ProRob.Motion.Models;

namespace TrajectoryPlotter
{
    class TrajectoryPlotter
    {
        static void Main(string[] args)
        {
            Console.WriteLine("[TrajectoryPlotter]");

            IPAddress localAddr = IPAddress.Parse("127.0.0.1");

            TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 11000);

            while (true)
            {
                server.Start();

                using (TcpClient client = server.AcceptTcpClient())
                {
                    client.ReceiveBufferSize = (int)1e6;

                    byte[] buffer = new byte[(int)1e6];

                    var stream = client.GetStream();

                    Trajectory trajectory = null;

                    var dataPacket = new List<byte>();

                    while (trajectory is null)
                    {
                        var nBytes = stream.Read(buffer, 0, client.Available);
                        Console.WriteLine($"Received {nBytes} bytes");
                        dataPacket.AddRange(buffer.Take(nBytes).ToArray());

                        string json = Encoding.UTF8.GetString(dataPacket.ToArray());

                        try
                        {
                            trajectory = JsonConvert.DeserializeObject<Trajectory>(json);
                        }
                        catch
                        {
                            Thread.Sleep(100);
                        }
                    }
                    Console.WriteLine($"{DateTime.Now}");

                    if (trajectory != null)
                    {
                        Console.WriteLine($"Number of points: {trajectory.Q.Count()}");
                        ProMatlabHelper.PlotTrajectory(trajectory);
                    }
                }

                server.Stop();

            }
        }
    }
}
