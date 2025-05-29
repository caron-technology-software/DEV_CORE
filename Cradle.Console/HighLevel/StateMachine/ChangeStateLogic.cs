using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using ProRob;

namespace Caron.Cradle.Control.HighLevel.StateMachine
{
    public partial class StateMachineManager
    {
        private volatile bool setStateOperationPending = false;
        public bool SetStateOperationInProgress { get => setStateOperationPending; private set => setStateOperationPending = value; }

        public bool CheckStateToStateTransition(ControlState toState)
        {
            ControlState fromState = ControlState;

            return (toState == fromState) ? true : false;
        }

        public void SetState(ControlState state)
        {
            if (CheckStateToStateTransition(state))
            {
                return;
            }

            SetStateOperationInProgress = true;

            ControlState fromState = ControlState;
            ControlState toState = state;

            ProConsole.WriteLine($"[StateMachineManager] TRANSITION from {fromState} to {toState}", ConsoleColor.Cyan);

            //----------------------------------------
            //Cancellation Token Source
            //----------------------------------------
            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();

                //Prove sperimentali evidenziano come 15ms sia il tempo medio di chiusura (sd: +/- 0 ms)
                bool ret = Task.WaitAll(stateTasks.ToArray(), TimeSpan.FromMilliseconds(1000));

                if (ret == false)
                {
                    ProConsole.WriteTitle("[StateMachineManager]: closing task failed");

                    foreach (var thread in stateThreads)
                    {
                        if (thread.IsAlive)
                        {
                            Console.WriteLine("[StateMachineManager] **ABORTING THREAD**");
                            thread.Abort();
                        }
                    }
                }
            }

            //--------------------------------------------
            // Precedent state
            //--------------------------------------------
            ExecutingPreSetCancellationOperations(ControlState);
            ExecutingExitingOperations(ControlState);

            //--------------------------------------------
            // New state
            //--------------------------------------------
            ControlState = state;
            ExecutingEnteringOperations(ControlState);

            SetStateOperationInProgress = false;

            StartStateTasks();
        }


        public void SetStateFromTask(ControlState state)
        {
            if (CheckStateToStateTransition(state))
            {
                return;
            }

            if (SetStateOperationInProgress)
            {
                return;
            }

            Console.WriteLine($"[SetStateFromTask] {ControlState}=>{state}");

            var tasks = Task.Run(() =>
            {
                SetState(state);
            });
        }
    }
}

