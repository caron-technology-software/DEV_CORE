#define DEBUG_PRINT

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProRob
{

    public interface IProNetIOTBackgroundTaskProvider
    {
        void BackgroundTask(CancellationToken cancellationToken);
    }

    public class ProTask
    {
        private List<TaskSupervisor> tasks = new List<TaskSupervisor>();


        private class TaskSupervisor
        {
            internal IProNetIOTBackgroundTaskProvider obj;
            private Task task;

            private CancellationTokenSource cancellationTokenSource;
            private CancellationToken cancellationToken;

            public TaskStatus TaskStatus { get => task.Status; }
            public string Name { get; private set; }


            public TaskSupervisor(IProNetIOTBackgroundTaskProvider obj, string name)
            {
                this.obj = obj;
                this.Name = name;
            }

            public void StartTask()
            {
                if (task is null || task.Status != System.Threading.Tasks.TaskStatus.Running)
                {
                    cancellationTokenSource = new CancellationTokenSource();
                    cancellationToken = cancellationTokenSource.Token;

                    task = Task.Run(() => { obj.BackgroundTask(cancellationToken); });
                }
#if DEBUG_PRINT
                else
                {
                    Console.WriteLine($"[WARNING] Task {Name} is running");
                }
#endif
            }

            public void StopTask()
            {
                cancellationTokenSource.Cancel();
            }
        }

        public void AddTask(IProNetIOTBackgroundTaskProvider obj, string name = "")
        {
            tasks.Add(new TaskSupervisor(obj, name));
        }

        public void CheckTasksAndRestart()
        {
            foreach (var t in tasks)
            {
                if (t.TaskStatus == TaskStatus.RanToCompletion)
                {

#if DEBUG_PRINT
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"Detected task {t.Name} in not running mode..");
                    Console.ResetColor();
#endif
                    t.StartTask();
                }
            }
        }

        public Task CheckTasksInBackground(TimeSpan timespan)
        {
            return Task.Run(() =>
            {
                while (true)
                {
                    CheckTasksAndRestart();
                    Thread.Sleep(timespan);
                }
            });
        }

        public bool CheckTaskAndRestart(IProNetIOTBackgroundTaskProvider obj)
        {
            foreach (var t in tasks)
            {
                if (t.obj == obj)
                {
                    if (t.TaskStatus == TaskStatus.RanToCompletion)
                    {
                        t.StartTask();
                        return true;
                    }
                    return false;
                }
            }

            return false;
        }

        public void PrintTasksStatus()
        {
            int i = 0;
            foreach (var t in tasks)
            {
                Console.WriteLine($"#{++i} Task: {t.Name} -> {t.TaskStatus}");
            }
            Console.WriteLine("");
        }

        public void StartTasks()
        {
            foreach (var t in tasks)
            {
                t.StartTask();
            }
        }

        public void StopTasks()
        {
            foreach (var t in tasks)
            {
                t.StopTask();
            }
        }

        public bool StartTask(IProNetIOTBackgroundTaskProvider obj)
        {
            foreach (var t in tasks)
            {
                if (t.obj == obj)
                {
                    t.StartTask();
                    return true;
                }
            }

            return false;
        }

        public bool StopTask(IProNetIOTBackgroundTaskProvider obj)
        {
            foreach (var t in tasks)
            {
                if (t.obj == obj)
                {
                    t.StopTask();
                    Console.WriteLine($"Stopped task {t.Name}");
                    return true;
                }
            }

            return false;
        }

        public TaskStatus GetTaskStatus(IProNetIOTBackgroundTaskProvider obj)
        {
            foreach (var t in tasks)
            {
                if (t.obj == obj)
                {
                    return t.TaskStatus;
                }
            }

            return default(TaskStatus);
        }

    }
}
