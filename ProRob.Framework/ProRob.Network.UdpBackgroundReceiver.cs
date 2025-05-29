using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProRob
{
    public class UdpBackgroundReceiver
    {
        private class DataPacketIPEndPoint
        {
            public byte[] DataPacket { get; set; }
            public IPEndPoint IPEndPoint { get; set; }
        }

        private const int ReceiveTimeout = 100; //[ms]

        private UdpClient socket;
        private readonly object socketLocker = new object();

        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private Task task;

        public int MessagesReceived { get; private set; } = 0;
        //public int ResponsesSent { get; private set; } = 0;
        public int Errors { get; private set; } = 0;
        public DateTime LastMessageReceived { get; private set; } = DateTime.MinValue;

        public int Port { get; private set; }

        public Action<byte[]> OnDataReceive;

        public UdpBackgroundReceiver(int port)
        {
            try 
            { 
                Port = port;

                ////GPIE01    se il socket è in uso riutilizza l'indirizzo: 
                socket = new UdpClient();
                socket.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

                IPEndPoint endPoint01 = new IPEndPoint(IPAddress.Any, Port);
                socket.ExclusiveAddressUse = false;
                socket.Client.Bind(endPoint01);
                socket.Client.ReceiveTimeout = ReceiveTimeout;                

                //socket = new UdpClient(new IPEndPoint(IPAddress.Any, Port));
                //socket.Client.ReceiveTimeout = ReceiveTimeout;
                ////socket.ExclusiveAddressUse = false;
                ////GPFE01
            }
            //GPIE01
            catch (Exception ex)
            {
                Console.WriteLine("{0} Exception caught.", ex);
                Console.WriteLine("{0} Exception Message.", ex.Message);
                //Console.WriteLine("Error reused socket UDP on port 5002!"); 
                Thread.Sleep(40000);
                //throw new Exception("Error reused socket UDP on port 5002!");
                throw ex;
            }
            //GPFE01
        }

        public void Start()
        {
            task = Task.Run(() =>
            {
                TaskReceiverHandler(cancellationTokenSource.Token);
            });

            Console.WriteLine("[UdpBackgroundReceiver] Start()");
        }

        public void Stop()
        {
            cancellationTokenSource.Cancel();

            Task.WaitAll(task);

            socket.Close();
            socket.Dispose();

            Console.WriteLine("[UdpBackgroundReceiver] Stop()");
        }

        public bool Write()
        {
            //////lock (socketLocker)
            //////{
            //////    socket.Send(response, response.Length, dataPacketIPEndPoint.IPEndPoint);
            //////}

            //////ResponsesSent++;

            throw new NotImplementedException();
        }

        private void TaskReceiverHandler(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var remoteEndPoint = new IPEndPoint(IPAddress.Any, Port);

                try
                {
                    byte[] dataPacket;
                    lock (socketLocker)
                    {
                        dataPacket = socket.Receive(ref remoteEndPoint);
                    }

                    if (dataPacket.Length > 0)
                    {
                        //var dataPacketIPEndPoint = new DataPacketIPEndPoint()
                        //{
                        //    IPEndPoint = new IPEndPoint(remoteEndPoint.Address, remoteEndPoint.Port),
                        //    DataPacket = dataPacket,
                        //};

                        OnDataReceive(dataPacket);

                        LastMessageReceived = DateTime.Now;
                        MessagesReceived++;
                    }
                }
                catch (Exception ex)
                {
                    switch (ex.HResult)
                    {
                        //Error Code: Timeout
                        case -2147467259:
                            break;

                        default:
                            Errors++;
                            break;
                    }
                }
            }
        }

    }
}
