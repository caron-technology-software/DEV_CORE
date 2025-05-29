using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.Remoting.Contexts;

using ProRob;
using ProRob.Extensions.Object;
using ProRob.Threading;

using Machine.Events;
using Machine.UI.Communication;

using Caron.Cradle.Control;
using Caron.Cradle.Control.LowLevel;
using Caron.Cradle.Control.HighLevel;

namespace Caron.Cradle.UI
{
    [Synchronization()]
    public class ControlStatusGrabber
    {
        private Control.ControlStatus controlStatus;

        private Counter controlStatusReceived;
        private Counter controlStatusErrors;
        private EventsManager eventsManager;

        private TimeSpan LastSystemControlUpdateDuration { get; set; } = TimeSpan.MinValue;
        private static readonly Stopwatch stopWatch = new Stopwatch();

        private CancellationToken cancellationToken;

        public ControlStatusGrabber(Counter controlStatusReceived, Counter controlStatusErrors, EventsManager eventsManager, CancellationToken cancellationToken)
        {
            this.controlStatus = new Control.ControlStatus();
            this.controlStatusReceived = controlStatusReceived;
            this.controlStatusErrors = controlStatusErrors;
            this.eventsManager = eventsManager;
            this.cancellationToken = cancellationToken;
        }

        public Caron.Cradle.Control.ControlStatus ControlStatus => controlStatus;

        public void Start()
        {
            Task.Run(() => { TaskHighLevelControlStatusGrabber(cancellationToken); });
        }

        private void TaskHighLevelControlStatusGrabber(CancellationToken cancellationToken)
        {
            ProConsole.WriteLine("[ENTERING] TaskHighLevelControlStatusGrabber", ConsoleColor.Green);

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    stopWatch.Restart();

                    //-------------------------------------------------------
                    //System Control Status Update
                    //-------------------------------------------------------                 
                    var cs = Communicator.GetEncodedData<Control.ControlStatus>("control_status/encoded");

                    if (cs is null)
                    {
                        controlStatusErrors.Increment();
                    }
                    else
                    {
                        controlStatusReceived.Increment();

                        //In caso di errori i valori dello stato saranno esattamente quelli del ciclo precedente
                        lock (controlStatus)
                        {
                            controlStatus = cs;
                        }

                        //-------------------------------------------------------
                        //Events Manager
                        //-------------------------------------------------------
                        eventsManager.Check(controlStatus);

                        stopWatch.Stop();

                        LastSystemControlUpdateDuration = stopWatch.Elapsed;

                        if (LastSystemControlUpdateDuration < Machine.UI.Constants.Intervals.UpdateControlStatus)
                        {
                            Thread.Sleep(Machine.UI.Constants.Intervals.UpdateControlStatus - LastSystemControlUpdateDuration);
                        }
                    }
                }
                catch (Exception e)
                {
                    ProConsole.WriteLine($"[EXCEPTION] TaskHighLevelControlStatusGrabber: {e.Message} - {e.Source} - Errors: {controlStatusErrors.CurrentCount}", ConsoleColor.Green);
                }
            }

            ProConsole.WriteLine($"[EXITING] TaskHighLevelControlStatusGrabber ({controlStatusErrors})", ConsoleColor.Red);
        }
    }
}
