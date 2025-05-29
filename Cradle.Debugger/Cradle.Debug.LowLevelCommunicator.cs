using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

using ProRob.Communication;

public enum Command : byte
{
    Hello = 1,
    ScalingFactor = 50,
    Ack = 255,
}

public class Communicator
{
    private static bool TrySendDataPacket(byte[] dataPacket)
    {
        var tcpClient = new TcpClient("10.0.0.1", 5000);
        byte[] receiveBuffer = new byte[UInt16.MaxValue];
        var stream = tcpClient.GetStream();

        try
        {
            int nBytes = 0;
            {
                stream.Write(dataPacket, 0, dataPacket.Length);

                nBytes = stream.Read(receiveBuffer, 0, receiveBuffer.Length);
            }

            stream.Close();
            tcpClient.Close();

            return (nBytes > 0) ? true : false;
        }
        catch
        {
            stream.Close();
            tcpClient.Close();

            return false;
        }
    }

    public static bool SendHelloMessage()
    {
        var dataPacket = new DataPacket((byte)Command.Hello);
        TrySendDataPacket(dataPacket.Create());

        return true;
    }

    public static bool SetScalingFactor(float scalingFactor)
    {
        var dataPacket = new DataPacket((byte)Command.ScalingFactor);
        dataPacket.AddDataToPayload(scalingFactor);
        TrySendDataPacket(dataPacket.Create());

        return true;
    }
}
