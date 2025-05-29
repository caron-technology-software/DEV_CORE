using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProRob
{
    public class ConsoleRedirectWriter : RedirectWriter
    {
        TextWriter consoleTextWriter; //keeps Visual Studio console in scope.

        public ConsoleRedirectWriter()
        {
            consoleTextWriter = Console.Out;
            this.OnWrite += delegate (string text) { consoleTextWriter.Write(text); };
            Console.SetOut(this);
        }

        public void Release()
        {
            Console.SetOut(consoleTextWriter);
        }
    }
}
