using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.Remoting.Contexts;

using ProRob;

using Caron.Cradle.Control.LowLevel;
using Caron.Cradle.Control.DataCollections;

namespace Caron.Cradle.Control
{
    [Synchronization()]
    public partial class LowLevelControlStatusGrabber : IDisposable
    {
        private CancellationTokenSource cancellationTokenSource;
        private CancellationToken cancellationToken;

        private volatile bool isRunning = true;
        public bool IsRunning => isRunning;

        private readonly UdpClient udpClient;
        private IPEndPoint ipEndPoint;

        private readonly object lockerLowLevelControlStatus = new object();
        private readonly LowLevel.ControlStatus lowLevelControlStatus;

        private readonly TimeSeries timeSeries;

        private DateTime firstPacketReceivedTimestamp;
        private DateTime lastPacketReceivedTimestamp;
        //GPIx164
        public TimeSpan TimeSpanFromLastDataPacketReceived => DateTime.UtcNow - lastPacketReceivedTimestamp;
        //GPFx164
        public TimeSpan Uptime => DateTime.UtcNow - firstPacketReceivedTimestamp;
        public float AvaragePacketsForSecond => (float)(PacketsReceived / Uptime.TotalSeconds);

        private long packetsReceived = 0;
        public int PacketsReceived => (int)Interlocked.Read(ref packetsReceived);

        private volatile int communicationErrors = 0;
        public int CommunicationErrors => communicationErrors;

        public LowLevelControlStatusGrabber(LowLevel.ControlStatus lowLevelStatus, TimeSeries timeSeries)
        {
            this.lowLevelControlStatus = lowLevelStatus;
            this.timeSeries = timeSeries;

            //GPIx129 se il socket è in uso riutilizza l'indirizzo (porta):
            udpClient = new UdpClient(Machine.Constants.Networking.LowLevelControlUdpPort);
            udpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);  //GPIx164 6) riutilizza socket anche se già utilizzato!
            ipEndPoint = new IPEndPoint(IPAddress.Parse(Machine.Constants.Networking.IPAddressLowLevelControl), Machine.Constants.Networking.LowLevelControlUdpPort);
            //GPFx129
            udpClient.Connect(ipEndPoint);
            udpClient.Client.ReceiveTimeout = (int)Machine.Constants.Timeouts.LowLevelTimeoutCommunication.TotalMilliseconds;

            cancellationTokenSource = new CancellationTokenSource();
            cancellationToken = cancellationTokenSource.Token;

            Task.Run(() => { TaskLowLevelControlStatusGrabber(cancellationToken); });
        }

        public void Stop()
        {
            isRunning = false;
        }

        private void TaskLowLevelControlStatusGrabber(CancellationToken cancellationToken)
        {
            ProConsole.WriteLine("[ENTERING] LowLevelControlStatusGrabber (TaskGrabber)", ConsoleColor.Green);

#if !TEST
            //int ciclo = 0;
            while (!cancellationToken.IsCancellationRequested)
            {
                if (isRunning)
                {
                    lock (lockerLowLevelControlStatus)
                    {
                        try
                        {
                            #region Low Level Data Packet Receive
                            byte[] buffer = udpClient.Receive(ref ipEndPoint);
                            if (buffer.Length == 0)
                            {
                                continue;
                            }

                            //Console.WriteLine($"[TaskLowLevelControlStatusGrabber] cycle time receive udp packet: {DateTime.UtcNow} - ciclo:{ciclo}");
                            //Console.WriteLine($"[TaskLowLevelControlStatusGrabber] cycle time receive udp packet: {DateTimeOffset.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt")} - ciclo:{ciclo}");
                            //ciclo++;

                            if (packetsReceived == 0)
                            {
                                firstPacketReceivedTimestamp = DateTime.UtcNow;
                            }

                            lastPacketReceivedTimestamp = DateTime.UtcNow;

                            Interlocked.Increment(ref this.packetsReceived);
                            #endregion

                            #region Composing Low Level Status
                            //-------------------------------------------------------
                            // Composing Low Level Status
                            //-------------------------------------------------------
                            int startIndex = 0;

                            //Info
                            lowLevelControlStatus.Info.Cycles = BitConverter.ToUInt32(buffer, startIndex);
                            startIndex += sizeof(UInt32);

                            lowLevelControlStatus.Info.MachineState = buffer[startIndex];
                            startIndex += sizeof(byte);

                            //GPIx164
                            lowLevelControlStatus.Info.EmergencyState = buffer[startIndex];
                            startIndex += sizeof(byte);
                            //GPFx164

                            lowLevelControlStatus.Info.CutterState = buffer[startIndex];
                            startIndex += sizeof(byte);

                            lowLevelControlStatus.Info.CradleJogState = buffer[startIndex];
                            startIndex += sizeof(byte);

                            lowLevelControlStatus.Info.Dancebar = BitConverter.ToSingle(buffer, startIndex);
                            startIndex += sizeof(float);

                            //Digital Inputs
                            for (int i = 0; i < Constants.Hardware.IO.NumberOfDigitalInputs; i++)
                            {
                                lowLevelControlStatus.IO.DigitalInputs[i] = BitConverter.ToBoolean(buffer, startIndex + i);
                            }
                            startIndex += Constants.Hardware.IO.NumberOfDigitalInputs;

                            //Digital Outputs
                            for (int i = 0; i < Constants.Hardware.IO.NumberOfDigitalOutputs; i++)
                            {
                                lowLevelControlStatus.IO.DigitalOutputs[i] = BitConverter.ToBoolean(buffer, startIndex + i);
                            }
                            startIndex += Constants.Hardware.IO.NumberOfDigitalOutputs;

                            //Machine Inputs
                            for (int i = 0; i < Constants.Hardware.IO.NumberOfMachineInputs; i++)
                            {
                                lowLevelControlStatus.IO.MachineInputs[i] = BitConverter.ToBoolean(buffer, startIndex + i);
                            }
                            startIndex += Constants.Hardware.IO.NumberOfMachineInputs;

                            //Virtual Inputs
                            for (int i = 0; i < Constants.Hardware.IO.NumberOfVirtualInputs; i++)
                            {
                                lowLevelControlStatus.IO.VirtualInputs[i] = BitConverter.ToBoolean(buffer, startIndex + i);
                            }
                            startIndex += Constants.Hardware.IO.NumberOfVirtualInputs;

                            //Analog Inputs
                            for (int i = 0; i < Constants.Hardware.IO.NumberOfAnalogInputs; i++)
                            {
                                lowLevelControlStatus.IO.AnalogInputs[i] = BitConverter.ToSingle(buffer, startIndex + (i * sizeof(Single)));
                            }
                            startIndex += sizeof(Single) * Constants.Hardware.IO.NumberOfAnalogInputs;

                            //Actions
                            lowLevelControlStatus.Actions.SpoonUp = BitConverter.ToBoolean(buffer, startIndex);
                            startIndex += sizeof(bool);

                            lowLevelControlStatus.Actions.SpoonDown = BitConverter.ToBoolean(buffer, startIndex);
                            startIndex += sizeof(bool);

                            lowLevelControlStatus.Actions.AlignmentMotorSide = BitConverter.ToBoolean(buffer, startIndex);
                            startIndex += sizeof(bool);

                            lowLevelControlStatus.Actions.AlignmentOperatorSide = BitConverter.ToBoolean(buffer, startIndex);
                            startIndex += sizeof(bool);

                            lowLevelControlStatus.Actions.OverturningLoad = BitConverter.ToBoolean(buffer, startIndex);
                            startIndex += sizeof(bool);

                            lowLevelControlStatus.Actions.OverturningUnload = BitConverter.ToBoolean(buffer, startIndex);
                            startIndex += sizeof(bool);

                            //Axes: Cradle
                            lowLevelControlStatus.Axes.Cradle.Position = BitConverter.ToSingle(buffer, startIndex);
                            startIndex += sizeof(Single);

                            lowLevelControlStatus.Axes.Cradle.Velocity = BitConverter.ToSingle(buffer, startIndex);
                            startIndex += sizeof(Single);

                            lowLevelControlStatus.Axes.Cradle.Status = buffer[startIndex];
                            startIndex += sizeof(byte);

                            lowLevelControlStatus.Axes.Cradle.PositionError = BitConverter.ToSingle(buffer, startIndex);
                            startIndex += sizeof(Single);

                            lowLevelControlStatus.Axes.Cradle.MaxPositionError = BitConverter.ToSingle(buffer, startIndex);
                            startIndex += sizeof(Single);

                            lowLevelControlStatus.Axes.Cradle.DriverCommandSpeed = BitConverter.ToInt16(buffer, startIndex);
                            startIndex += sizeof(Int16);

                            lowLevelControlStatus.Axes.Cradle.ProportionalAction = BitConverter.ToSingle(buffer, startIndex);
                            startIndex += sizeof(Single);

                            lowLevelControlStatus.Axes.Cradle.IntegrativeAction = BitConverter.ToSingle(buffer, startIndex);
                            startIndex += sizeof(Single);

                            lowLevelControlStatus.Axes.Cradle.DerivativeAction = BitConverter.ToSingle(buffer, startIndex);
                            startIndex += sizeof(Single);

                            lowLevelControlStatus.Axes.Cradle.FeedForwardAction = BitConverter.ToSingle(buffer, startIndex);
                            startIndex += sizeof(Single);

                            //Axes: Table
                            lowLevelControlStatus.Axes.Table.Position = BitConverter.ToSingle(buffer, startIndex);
                            startIndex += sizeof(Single);

                            lowLevelControlStatus.Axes.Table.Velocity = BitConverter.ToSingle(buffer, startIndex);
                            startIndex += sizeof(Single);

                            //Axes: Table Scalable
                            lowLevelControlStatus.Axes.TableScalable.Position = BitConverter.ToSingle(buffer, startIndex);
                            startIndex += sizeof(Single);

                            lowLevelControlStatus.Axes.TableScalable.Velocity = BitConverter.ToSingle(buffer, startIndex);
                            startIndex += sizeof(Single);

                            lowLevelControlStatus.Axes.TableScalable.ScalingFactor = BitConverter.ToSingle(buffer, startIndex);
                            startIndex += sizeof(Single);

                            //Axes: Cutter
                            lowLevelControlStatus.Axes.Cutter.Status = buffer[startIndex];
                            startIndex += sizeof(byte);

                            //Axes: Dancer
                            lowLevelControlStatus.Axes.Dancer.NormalizedValue = BitConverter.ToSingle(buffer, startIndex);
                            startIndex += sizeof(Single);
                            #endregion

                            #region Cradle Data Collections updates
                            TimeSeriesUpdater();
                            #endregion

                            Interlocked.Exchange(ref this.communicationErrors, 0);
                        }
                        catch
                        {
                            Interlocked.Increment(ref this.communicationErrors);
                        }
                    }
                }
            }
#else
            firstPacketReceivedTimestamp = DateTime.UtcNow;
            lowLevelControlStatus.IO.MachineInputs[(byte)MachineInput.MarchEnabled] = true;

            while (!cancellationToken.IsCancellationRequested)
            {
                lastPacketReceivedTimestamp = DateTime.UtcNow;

                lock (lockerLowLevelControlStatus)
                {
                    lowLevelControlStatus.Info.Cycles = 0 + (uint)(lastPacketReceivedTimestamp.Millisecond);

                    lowLevelControlStatus.Axes.Cradle.Position = 1 + (float)Math.Sin(2 * Math.PI * 0.1 * Uptime.TotalSeconds);
                    lowLevelControlStatus.Axes.Cradle.PositionError = 0.01f * (float)Math.Sin(2 * Math.PI * 1.0 * Uptime.TotalSeconds);
                    lowLevelControlStatus.Axes.Cradle.Velocity = (float)Math.Sin(2 * Math.PI * 0.2 * Uptime.TotalSeconds);
                    lowLevelControlStatus.Axes.Table.Position = (float)Math.Sin(2 * Math.PI * 0.3 * Uptime.TotalSeconds);
                    lowLevelControlStatus.Axes.Table.Velocity = (float)Math.Sin(2 * Math.PI * 0.4 * Uptime.TotalSeconds);
                    lowLevelControlStatus.IO.AnalogInputs[0] = (float)Math.Sin(2 * Math.PI * 0.5 * Uptime.TotalSeconds);

                    TimeSeriesUpdater();
                }
                
                Interlocked.Increment(ref this.packetsReceived);

                Thread.Sleep(Machine.Constants.Intervals.HighLevelControlCycle);
            }
#endif
            ProConsole.WriteLine("[EXITING] LowLevelStatusGrabber (TaskGrabber)", ConsoleColor.Red);
        }

        internal void TimeSeriesUpdater()
        {
            timeSeries.LowLevelCycleTicksBag.AddData(lastPacketReceivedTimestamp, lastPacketReceivedTimestamp.Ticks);
            timeSeries.LowLevelControlStatusBag.AddData(lastPacketReceivedTimestamp, lowLevelControlStatus);
        }

        public void ResetCommunicationErrors()
        {
            communicationErrors = 0;
        }

        #region IDisposable
        public void Dispose()
        {

            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                isRunning = false;

                if (cancellationTokenSource != null)
                {
                    cancellationTokenSource.Cancel();
                }

                if (udpClient != null)
                {
                    udpClient.Dispose();
                }

                if (cancellationTokenSource != null)
                {
                    cancellationTokenSource.Dispose();
                    cancellationTokenSource = null;
                }
            }
        }
        #endregion
    }
}
