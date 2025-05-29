using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

using ProRob.Collections;

namespace ProRob
{
    public static class ProConsole
    {
        private const int SwHide = 0x00;
        private const int SwShow = 0x05;
        private const int SwMinimize = 0x06;
        private const int SwMaximize = 0x03;

        private const int AttachParentProcess = -1;

        private const int BufferLength = 1_000;

        private static readonly object locker = new object();
        private static CircularBuffer<string> Log { get; set; } = new CircularBuffer<string>(BufferLength);

        [DllImport("kernel32.dll")]
        static extern bool AttachConsole(int dwProcessId);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        public static IEnumerable<string> GetLog(int numberOfLastItems)
        {
            lock (locker)
            {
                int elementsToSkip = Log.Size - numberOfLastItems;

                if (elementsToSkip <= 0)
                {
                    return Log;
                }
                else
                {
                    return Log.Skip(elementsToSkip);
                }
            }
        }

        public static IEnumerable<string> GetLog()
        {
            lock (locker)
            {
                return Log;
            }
        }

        public static void AddToLog(string text)
        {
            lock (locker)
            {
                Log.PushBack(text);
            }
        }

        public static void AttachConsole()
        {
            AttachConsole(AttachParentProcess);
        }

        public static void ShowConsoleWindow()
        {
            var handle = GetConsoleWindow();
            ShowWindow(handle, SwShow);
        }

        public static void HideConsoleWindow()
        {
            var handle = GetConsoleWindow();
            ShowWindow(handle, SwHide);
        }

        public static void MinimizeConsoleWindow()
        {
            var handle = GetConsoleWindow();
            ShowWindow(handle, SwMinimize);
        }

        public static void MaximizeConsoleWindow()
        {
            var handle = GetConsoleWindow();
            ShowWindow(handle, SwMaximize);
        }

        static ProConsole()
        {
            //--
        }

        public static void WriteTitle(string message, ConsoleColor consoleColor = ConsoleColor.Gray)
        {
            var n = message.Length;

            var line = string.Concat(Enumerable.Repeat("-", n + 2 + 2));

            lock (locker)
            {
                ProConsole.WriteLine(line, consoleColor);
                ProConsole.WriteLine($"| {message} |", consoleColor);
                ProConsole.WriteLine(line, consoleColor);
            }
        }

        public static void Write(string message, ConsoleColor consoleColor = ConsoleColor.Gray)
        {
            lock (locker)
            {
                Console.ForegroundColor = consoleColor;
                Console.Write(message);
                Console.ResetColor();
            }
        }

        public static void WriteLine(string message, ConsoleColor consoleColor = ConsoleColor.Gray)
        {
            Write(message + Environment.NewLine, consoleColor);
        }

        public static void Write(object obj, ConsoleColor consoleColor = ConsoleColor.Gray)
        {
            Write(obj.ToString(), consoleColor);
        }

        public static void WriteLine(object obj, ConsoleColor consoleColor = ConsoleColor.Gray)
        {
            WriteLine(obj.ToString(), consoleColor);
        }

        public static void WriteLine()
        {
            WriteLine(String.Empty);
        }

        public static void PressKeyToContinue(string message)
        {
            lock (locker)
            {
                WriteLine(Environment.NewLine + message);
            }

            Console.ReadKey(true);
        }

        public static void PressKeyToContinue()
        {
            PressKeyToContinue("Press a key to continue..");
        }
    }
}

