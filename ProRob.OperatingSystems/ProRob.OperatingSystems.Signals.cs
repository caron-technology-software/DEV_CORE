using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using Mono.Unix;

//---------------------------------------------------------------------------------------------------------
// REFERENCE: https://stackoverflow.com/questions/6546509/detect-when-console-application-is-closing-killed
//---------------------------------------------------------------------------------------------------------

namespace ProRob.OperatingSystems.Signals
{
    public interface IExitSignal
    {
        event EventHandler Exit;
    }

    public sealed class ExitSignal : IExitSignal
    {
        private static ExitSignal instance;

        private static readonly Object locker = new Object();

        private IExitSignal exitSignal;

        public event EventHandler Exit
        {
            add => exitSignal.Exit += value;
            remove => exitSignal.Exit -= value;
        }

        static ExitSignal()
        {
            var exitSignal = new ExitSignal();
        }

        private ExitSignal()
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Unix:
                    exitSignal = new UnixExitSignal();
                    break;

                case PlatformID.Win32NT:
                    exitSignal = new WindowsExitSignal();
                    break;

                case PlatformID.MacOSX:
                    throw new NotImplementedException();
            }
        }

        public static ExitSignal Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (locker)
                    {
                        if (instance == null)
                        {
                            instance = new ExitSignal();
                        }
                    }
                }

                return instance;
            }
        }
    }

    internal class UnixExitSignal : IExitSignal
    {
        public event EventHandler Exit;

        UnixSignal[] signals = new UnixSignal[]
        {
            new UnixSignal(Mono.Unix.Native.Signum.SIGTERM),
            new UnixSignal(Mono.Unix.Native.Signum.SIGINT),
            new UnixSignal(Mono.Unix.Native.Signum.SIGUSR1)
        };

        public UnixExitSignal()
        {
            Task.Factory.StartNew(() =>
            {
                // blocking call to wait for any kill signal
                int index = UnixSignal.WaitAny(signals, -1);

                if (Exit != null)
                {
                    Exit(null, EventArgs.Empty);
                }
            });
        }
    }

    internal class WindowsExitSignal : IExitSignal
    {
        public event EventHandler Exit;

        [DllImport("Kernel32")]
        private static extern bool SetConsoleCtrlHandler(HandlerRoutine Handler, bool Add);

        // A delegate type to be used as the handler routine for SetConsoleCtrlHandler.
        private delegate bool HandlerRoutine(CtrlTypes CtrlType);

        // An enumerated type for the control messages sent to the handler routine.
        private enum CtrlTypes
        {
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT,
            CTRL_CLOSE_EVENT,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT
        }

        /// <summary>
        /// Need this as a member variable to avoid it being garbage collected.
        /// </summary>
        private HandlerRoutine m_hr;

        public WindowsExitSignal()
        {
            m_hr = new HandlerRoutine(ConsoleCtrlCheck);

            SetConsoleCtrlHandler(m_hr, true);

        }

        /// <summary>
        /// Handle the ctrl types
        /// </summary>
        /// <param name="ctrlType"></param>
        /// <returns></returns>
        private bool ConsoleCtrlCheck(CtrlTypes ctrlType)
        {
            Console.WriteLine($"CtrlTypes: {ctrlType}");

            switch (ctrlType)
            {
                case CtrlTypes.CTRL_C_EVENT:
                case CtrlTypes.CTRL_BREAK_EVENT:
                case CtrlTypes.CTRL_CLOSE_EVENT:
                case CtrlTypes.CTRL_LOGOFF_EVENT:
                case CtrlTypes.CTRL_SHUTDOWN_EVENT:
                    if (Exit != null)
                    {
                        Exit(this, EventArgs.Empty);
                    }
                    break;
                default:
                    break;
            }

            return true;
        }
    }
}
