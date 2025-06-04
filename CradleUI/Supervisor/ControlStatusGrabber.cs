using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

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
    public class ControlStatusGrabber
    {
        private readonly object _lock = new();

        private Control.ControlStatus controlStatus;
        public Caron.Cradle.Control.ControlStatus ControlStatus
        {
            get
            {
                lock (_lock)
                {
                    return controlStatus;
                }
            }
        }

        private readonly Counter controlStatusReceived;
        private readonly Counter controlStatusErrors;
        private readonly EventsManager eventsManager;
        private readonly CancellationToken cancellationToken;

        private TimeSpan LastSystemControlUpdateDuration { get; set; } = TimeSpan.MinValue;
        private static readonly Stopwatch stopWatch = new Stopwatch();

        public ControlStatusGrabber(
            Counter controlStatusReceived,
            Counter controlStatusErrors,
            EventsManager eventsManager,
            CancellationToken cancellationToken)
        {
            this.controlStatus = new Control.ControlStatus();
            this.controlStatusReceived = controlStatusReceived;
            this.controlStatusErrors = controlStatusErrors;
            this.eventsManager = eventsManager;
            this.cancellationToken = cancellationToken;
        }

        public void Start()
        {
            Task.Run(() => TaskHighLevelControlStatusGrabber(cancellationToken));
        }

        private void TaskHighLevelControlStatusGrabber(CancellationToken cancellationToken)
        {
            ProConsole.WriteLine("[ENTERING] TaskHighLevelControlStatusGrabber", ConsoleColor.Green);

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    stopWatch.Restart();

                    var cs = Communicator.GetEncodedData<Control.ControlStatus>("control_status/encoded");

                    if (cs is null)
                    {
                        controlStatusErrors.Increment();
                    }
                    else
                    {
                        controlStatusReceived.Increment();

                        lock (_lock)
                        {
                            controlStatus = cs;
                        }

                        eventsManager.Check(cs);

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
