using System;
using System.Linq;
using System.Text;
using System.Threading;

using ProRob;

using Machine.UI.Communication;

using Caron.Cradle.Control;
using ProRob.Threading;
using System.Threading.Tasks;

namespace Caron.Cradle.UI
{
    public partial class Supervisor : IDisposable
    {
        private CancellationTokenSource cancellationTokenSource;
        private CancellationToken cancellationToken;

        private volatile bool isRunning = false;
        public bool IsRunning { get => isRunning; private set => isRunning = value; }

        private volatile bool deinitializationCompleted = false;
        public bool DeinitializationCompleted { get => deinitializationCompleted; private set => deinitializationCompleted = value; }

        private Counter controlStatusReceived = new Counter();
        private Counter controlStatusErrors = new Counter();

        public int ControlStatusReceived => controlStatusReceived.CurrentCount;
        public int ControlStatusErrors => controlStatusErrors.CurrentCount;

        public UI UI { get; set; } = new UI();
        public UIFunctions UIFunctions { get; set; }
        public Control.ControlStatus Control => ControlStatusGrabber.ControlStatus;

        private ControlStatusGrabber ControlStatusGrabber { get; set; }

        public CradleHelper CradleHelper { get; set; }

        public EventsManager Events { get; set; } = new EventsManager();

        public void Stop()
        {
            isRunning = false;
        }

        public Supervisor()
        {
            cancellationTokenSource = new CancellationTokenSource();
            cancellationToken = cancellationTokenSource.Token;

            UI.State = StateUI.Dashboard;
            UI.PrecedentState = StateUI.Null;

            CradleHelper = new CradleHelper(this);
            UIFunctions = new UIFunctions(this);

            //-----------------------------------------------------------------------
            // Inizializzazione e burn in prima di inizializzare IsRunning=true;
            //-----------------------------------------------------------------------
            #region Initialization
            ProConsole.WriteLine("[Supervisor] Initialization..", ConsoleColor.Cyan);

            Communicator.TrySendHttpGetRequest("control_status");
            Communicator.TrySendHttpGetRequest("control_status/encoded");

            while (!Communicator.TrySendHttpGetRequest("control_status"))
            {
                Console.WriteLine("Waiting complete initialization of Cradle.Control..");
            }

            while (!Communicator.TrySendHttpGetRequest("control_status/encoded"))
            {
                Console.WriteLine("Waiting complete initialization (encoded) of Cradle.Control..");
            }

            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(10);
                Communicator.TrySendHttpGetRequest("control_status");
            }

            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(10);
                Communicator.GetEncodedData<Control.ControlStatus>("control_status/encoded");
            }

            Thread.Sleep(500);

            #endregion

            ProConsole.WriteLine("[Supervisor] Initializing Control Status Grabber..", ConsoleColor.Cyan);
            ControlStatusGrabber = new ControlStatusGrabber(controlStatusReceived, controlStatusErrors, Events, cancellationToken);
            ControlStatusGrabber.Start();

            Thread.Sleep(1000);

            IsRunning = true;

            ProConsole.WriteLine("[Supervisor] Running..", ConsoleColor.Cyan);
        }

        public void ShutdownUI()
        {
            Console.WriteLine($"ShutdownUI()");

            IsRunning = false;

            cancellationTokenSource.Cancel();

            Thread.Sleep(Machine.UI.Constants.Intervals.WaitUIShutdown);

            try
            {
                UI.Forms.Transparent.Dispose();

                UI.Forms.UserSettings.Dispose();
                UI.Forms.ManualOperations.Dispose();
                UI.Forms.RootSettings.Dispose();
                UI.Forms.WorkingsSettings.Dispose();
                UI.Forms.LoadUnload.Dispose();
                UI.Forms.Licenses.Dispose();
                UI.Forms.Messages.Dispose();

                UI.Forms.Dashboard.Dispose();

                UI.Forms.Actions.Dispose();
                UI.Forms.Menu.Dispose();
                UI.Forms.TopBar.Dispose();

                UI.Forms.Main.Dispose();

                UI.Forms.BroswerInterface.Dispose();
            }
            catch
            {
                //--
            }

            DeinitializationCompleted = true;

            Console.WriteLine($"Deinitialization completed");
        }

        #region IDisposable
        public void Dispose()
        {
            ShutdownUI();

            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (cancellationTokenSource != null)
                {
                    cancellationTokenSource.Cancel();
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