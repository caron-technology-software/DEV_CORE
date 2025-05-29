using System;
using System.Threading;

using ProRob;

namespace Caron.Cradle.Control.HighLevel.StateMachine
{
    public static class StateMachineHelper
    {
        public const int IterToWaitToExecuteAction = 10;

        [ThreadStatic]
        static int numberOfCalls = 0;
        public static void PrintYouShouldNotSeeMeMessage(uint cycles)
        {
            if ((cycles % IterToWaitToExecuteAction) == 0 || numberOfCalls == 0)
            {
                Console.WriteLine($"================================================> [YOU SHOULD NOT SEE ME] ({numberOfCalls})");
            }

            numberOfCalls++;
        }

        public static void RegisterCurrentThread(StateMachineManager stateMachine, Thread thread)
        {
            try
            {
                //MMIx10 ProConsole.WriteLine($"PRE Wait RegisterCurrentThread {DateTime.Now}", ConsoleColor.DarkYellow);

                while (stateMachine is null)
                {
                    Thread.Sleep(Machine.Constants.Intervals.HighLevelControlCycle);
                }

                //MMIx10 ProConsole.WriteLine($"POST Wait {DateTime.Now}");

                stateMachine.AddThread(thread);

                ProConsole.WriteLine($"POST RegisterCurrentThread {DateTime.Now}", ConsoleColor.DarkYellow);
            }
            catch
            {
                ProConsole.WriteLine($"EXCEPTION RegisterCurrentThread", ConsoleColor.DarkYellow);
            }
        }
    }
}

