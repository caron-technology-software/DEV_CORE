using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.Remoting.Contexts;
using System.Threading;
using System.Threading.Tasks;

using ProRob;

namespace Caron.Cradle.Control.HighLevel.StateMachine
{
    [Synchronization()]
    public partial class StateMachineManager : IDisposable
    {
        private MachineController Cradle { get; }

        //-----------------------------
        // Control State
        //-----------------------------
        private volatile ControlState controlState = ControlState.Null;
        public ControlState ControlState { get => controlState; private set => controlState = value; }

        //-----------------------------
        // SubStates
        //-----------------------------
        #region SubStates
        private volatile NullSubState nullSubState;
        public NullSubState NullSubState { get => nullSubState; internal set => nullSubState = value; }

        private volatile EmergencySubState emergencySubstate;
        public EmergencySubState EmergencySubstate { get => emergencySubstate; internal set => emergencySubstate = value; }

        //GPIx21
        private volatile IOConfigSubState ioConfigSubState;
        public IOConfigSubState IOConfigSubState { get => ioConfigSubState; internal set => ioConfigSubState = value; }
        //GPFx21

        private volatile WaitMarchSubState waitMarchSubState;
        public WaitMarchSubState WaitMarchSubState { get => waitMarchSubState; internal set => waitMarchSubState = value; }

        private volatile LoadUnloadSubState loadUnloadSubState;
        public LoadUnloadSubState LoadUnloadSubState { get => loadUnloadSubState; internal set => loadUnloadSubState = value; }

        private volatile NormalSubState normalSubState;
        public NormalSubState NormalSubState { get => normalSubState; internal set => normalSubState = value; }

        private volatile ManualOperationsSubState manualOperationsSubState;
        public ManualOperationsSubState ManualOperationsSubState { get => manualOperationsSubState; internal set => manualOperationsSubState = value; }

        private volatile SharpeningSubState sharpeningSubState;
        public SharpeningSubState SharpeningSubState { get => sharpeningSubState; internal set => sharpeningSubState = value; }

        private volatile CutOffSubState cutOffSubState;
        public CutOffSubState CutOffSubState { get => cutOffSubState; internal set => cutOffSubState = value; }

        private volatile CradleJogSubState cradleJogSubState;
        public CradleJogSubState CradleJogSubState { get => cradleJogSubState; internal set => cradleJogSubState = value; }

        private volatile CradleJogLoadUnloadSubState cradleJogLoadUnloadState;
        public CradleJogLoadUnloadSubState CradleJogLoadUnloadSubState { get => cradleJogLoadUnloadState; internal set => cradleJogLoadUnloadState = value; }

        private volatile CradleJogManualOperationsSubState cradleJogManualOperationsSubState;
        public CradleJogManualOperationsSubState CradleJogManualOperationsSubState { get => cradleJogManualOperationsSubState; internal set => cradleJogManualOperationsSubState = value; }

        private volatile CradleRewindSubState cradleRewindSubState;
        public CradleRewindSubState CradleRewindSubState { get => cradleRewindSubState; internal set => cradleRewindSubState = value; }
        #endregion

        //-----------------------------
        // Tasks
        //-----------------------------
        private readonly ConcurrentBag<Task> stateTasks = new ConcurrentBag<Task>();
        private readonly ConcurrentBag<Thread> stateThreads = new ConcurrentBag<Thread>();

        private CancellationTokenSource cancellationTokenSource = null;
        private CancellationToken cancellationToken;

        //-----------------------------
        // ChangeUiPagePermission
        //-----------------------------
        private volatile bool changeUIPagePermission = false;
        public bool ChangeUIPagePermission { get => changeUIPagePermission; private set => changeUIPagePermission = value; }

        public StateMachineManager(MachineController cradle)
        {
            this.Cradle = cradle;

            //-----------------------------
            // INITIAL STATE
            //-----------------------------
            ProConsole.WriteLine("[LAUNCHING INITIAL STATE]", ConsoleColor.Yellow);
            SetState(ControlState.WaitMarch);
        }

        internal void AddThread(Thread thread)
        {
            if (stateThreads != null)
            {
                stateThreads.Add(thread);
            }
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
                if (cancellationTokenSource != null)
                {
                    cancellationTokenSource.Cancel();
                }

                Thread.Sleep(Machine.Constants.Intervals.TaskDispose);

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
