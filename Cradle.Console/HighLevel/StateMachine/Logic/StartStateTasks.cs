using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using ProRob.Extensions.Collections.Concurrent;

namespace Caron.Cradle.Control.HighLevel.StateMachine
{
    public partial class StateMachineManager
    {
        private void StartTasksInitialization()
        {
            cancellationTokenSource = new CancellationTokenSource();
            cancellationToken = cancellationTokenSource.Token;

            stateTasks.Clear();
            stateThreads.Clear();

            Thread.Sleep(10);
        }

        private void StartStateTasks()
        {
            Console.WriteLine($"StartStateTasks({ControlState})");

            StartTasksInitialization();

            switch (ControlState)
            {
                //GPIx21
                case ControlState.IOConfig:
                    {
                        stateTasks.Add(Task.Run(() =>
                        {
                            Cradle.TaskStateIOConfig(cancellationToken);
                        }));
                    }
                    break;
                //GPFx21

                case ControlState.WaitMarch:
                    {
                        stateTasks.Add(Task.Run(() =>
                        {
                            Cradle.TaskStateWaitMarch(cancellationToken);
                        }));
                    }
                    break;

                case ControlState.Normal:
                    {
                        stateTasks.Add(Task.Run(() =>
                        {
                            Cradle.TaskStateNormal(cancellationToken);
                        }));
                    }
                    break;

                case ControlState.ManualOperations:
                    {
                        stateTasks.Add(Task.Run(() =>
                        {
                            Cradle.TaskStateManualOperations(cancellationToken);
                        }));
                    }
                    break;

                case ControlState.LoadUnload:
                    {
                        stateTasks.Add(Task.Run(() =>
                        {
                            Cradle.TaskStateLoadUnload(cancellationToken);
                        }));
                    }
                    break;

                case ControlState.Null:
                    {
                        stateTasks.Add(Task.Run(() =>
                        {
                            Cradle.TaskStateNull(cancellationToken);
                        }));
                    }
                    break;

                case ControlState.CutOff:
                    {
                        stateTasks.Add(Task.Run(() =>
                        {
                            Cradle.TaskStateCutOff(cancellationToken);
                        }));
                    }
                    break;

                case ControlState.Sharpening:
                    {
                        stateTasks.Add(Task.Run(() =>
                        {
                            Cradle.TaskStateSharpening(cancellationToken);
                        }));
                    }
                    break;

                case ControlState.CradleJog:
                    {
                        stateTasks.Add(Task.Run(() =>
                        {
                            Cradle.TaskStateCradleJog(cancellationToken);
                        }));
                    }
                    break;

                case ControlState.CradleJogLoadUnload:
                    {
                        stateTasks.Add(Task.Run(() =>
                        {
                            Cradle.TaskCradleJogLoadUnload(cancellationToken);
                        }));
                    }
                    break;

                case ControlState.CradleJogManualOperations:
                    {
                        stateTasks.Add(Task.Run(() =>
                        {
                            Cradle.TaskStateCradleJogManualOperations(cancellationToken);
                        }));
                    }
                    break;

                case ControlState.CradleRewind:
                    {
                        stateTasks.Add(Task.Run(() =>
                        {
                            Cradle.TaskStateCradleRewind(cancellationToken);
                        }));
                    }
                    break;
            }

            Thread.Sleep(10);
        }
    }
}
