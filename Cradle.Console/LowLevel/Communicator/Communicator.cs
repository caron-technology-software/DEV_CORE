using System;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

using ProRob.Communication;

namespace Caron.Cradle.Control.LowLevel.Communication
{
    public partial class Communicator : IDisposable
    {
        //GPIx164
        public volatile static TcpClient tcpClientX;
        //GPFx164
        private TcpClient tcpClient;
        private NetworkStream stream;

        private readonly object lockerStream = new object();
        private readonly object lockerSend = new object();

#if !TEST
        private byte[] receiveBuffer;
#endif
        public string Server { get; private set; }
        public int Port { get; private set; }
        public bool Connected { get; private set; } = false;

        private readonly Control.LowLevel.ControlStatus lowLevelStatus;

        //nuovo comunicator aggiungi qui:           //GPIx164
        //GPIx164
        public Communicator(string server, int port, Control.LowLevel.ControlStatus lowLevel, Control.HighLevel.ControlStatus highLevel, ref bool test)
        {
#if TEST
            Console.WriteLine("\n************************");
            Console.WriteLine("*   INTERNAL TESTING   *");
            Console.WriteLine("************************\n");
#else
            Server = server;
            Port = port;

            while (Connected == false)
            {
                try
                {
                    //GPIx164 tcpClientX:
                    if (tcpClientX == null)
                    {
                        tcpClient = new TcpClient(Server, Port);
                    }
                    else
                    {
                        tcpClient = tcpClientX;
                    }
                    //////tcpClient = new TcpClient(Server, Port);
                    //GPFx164

                    stream = tcpClient.GetStream();
                    stream.ReadTimeout = (int)Machine.Constants.Timeouts.HighLevelControlCommunication.TotalMilliseconds;
                    stream.WriteTimeout = (int)Machine.Constants.Timeouts.HighLevelControlCommunication.TotalMilliseconds;

                    Connected = true;
                }
                catch
                {
                    Console.WriteLine($"[Communicator] Connection failed..");
                    test = true;
                }
            }

            if (Connected)
            {
                tcpClientX = tcpClient;
            }

            Console.WriteLine($"[Communicator] Connected with low level control [{Server}:{Port}]");

            receiveBuffer = new byte[short.MaxValue];
#endif
            //----------------------------------------------------

            this.lowLevelStatus = lowLevel;
            //HighLevel = highLevel;

            //Attesa caricamento low level control
            //Thread.Sleep(LowLevelControlWait);

            //while (SendHello() == false)
            //{
            //    Thread.Sleep(HelloCommandWait);
            //}
        }


        public void Close()
        {
            Console.WriteLine("Closing LowLevelCommunicator..");
            tcpClient.Close();
        }

        //GPFx164

        public Communicator(string server, int port, Control.LowLevel.ControlStatus lowLevelStatus)
        {
#if TEST
            Console.WriteLine("\n************************");
            Console.WriteLine("*   INTERNAL TESTING   *");
            Console.WriteLine("************************\n");
#else
            Server = server;
            Port = port;

            while (Connected == false)
            {
                try
                {
                    //GPIx164 tcpClientX:
                    if (tcpClientX == null)
                    {
                        tcpClient = new TcpClient(Server, Port);
                    }
                    else
                    {
                        tcpClient = tcpClientX;
                    }
                    //////tcpClient = new TcpClient(Server, Port);
                    //GPFx164

                    stream = tcpClient.GetStream();
                    stream.ReadTimeout = (int)Machine.Constants.Timeouts.HighLevelControlCommunication.TotalMilliseconds;
                    stream.WriteTimeout = (int)Machine.Constants.Timeouts.HighLevelControlCommunication.TotalMilliseconds;

                    Connected = true;
                }
                catch
                {
                    Console.WriteLine($"Connection failed..");
                }
            }

            Console.WriteLine($"Connected with low level control [{Server}:{Port}]");

            receiveBuffer = new byte[short.MaxValue];
#endif
            //--------------

            this.lowLevelStatus = lowLevelStatus;
        }

#if !TEST
        internal bool TrySendDataPacket(byte[] dataPacket)
        {
            lock (lockerSend)
            {
                try
                {
                    int nBytes = 0;

                    lock (lockerStream)
                    {
                        stream.Write(dataPacket, 0, dataPacket.Length);

                        nBytes = stream.Read(receiveBuffer, 0, receiveBuffer.Length);
                    }

                    //Console.WriteLine($"TrySendDataPacket {dataPacket.Length} {nBytes}");

                    ////GPIx164  //per sicurezza incremento comando a basso livello a 20 millisecondi per permettere esecuzione comando precedente senza problemi:
                    //Thread.Sleep(1);  //dato che non uso coda al basso livello metto un piccolo sleep perchè il basso livello riceva l'acknoledge
                    Thread.Sleep(5);  //cambiata a cinque secondi da 20. (in futuro aumentiamo se ci sono problemi!)
                    ////GPFx164

                    return (nBytes > 0) ? true : false;
                }
                catch
                {
                    return false;
                }
            }
        }
#else
        public bool TrySendDataPacket(byte[] dataPacket)
        {
            return true;
        }
#endif
        public bool SendCommand(Command command)
        {
            var dataPacket = new DataPacket((byte)command);
            TrySendDataPacket(dataPacket.Create());
            return true;
        }

        public bool SendCommand(Command command, bool value)
        {
            var dataPacket = new DataPacket((byte)command, value ? (byte)1 : (byte)0);
            TrySendDataPacket(dataPacket.Create());
            return true;
        }

        public bool SendCommand(Command command, float value)
        {
            var dataPacket = new DataPacket((byte)command);
            dataPacket.AddDataToPayload(value);
            TrySendDataPacket(dataPacket.Create());
            return true;
        }

        public bool SendCommand(Command command, byte value)
        {
            var dataPacket = new DataPacket((byte)command, value);
            TrySendDataPacket(dataPacket.Create());
            return true;
        }

        #region IDisposable
        public void Dispose()
        {
            Console.WriteLine("Disposing LowLevelCommunicator..");
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (stream != null)
                {
                    stream.Dispose();
                    stream = null;
                }

                if (tcpClient != null)
                {
                    tcpClient.Dispose();
                    tcpClient = null;
                }
            }
        }
        #endregion
    }
}
