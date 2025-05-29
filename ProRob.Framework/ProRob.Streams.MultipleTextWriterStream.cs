using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Threading;

namespace ProRob
{
    public class MultipleTextWriterStreams : TextWriter
    {
        private readonly List<TextWriter> textWriters = new List<TextWriter>();
        private readonly object locker = new object();

        public MultipleTextWriterStreams()
        {

        }

        public void Add(TextWriter writer)
        {
            textWriters.Add(writer);
        }

        public override void Write(char value)
        {
            foreach (var writer in textWriters)
            {
                lock (locker)
                {
                    writer.Write(value);
                }
            }
        }

        public override void Write(string value)
        {
            foreach (var writer in textWriters)
            {
                lock (locker)
                {
                    writer.Write(value);
                }
            }
        }

        public override void Flush()
        {
            foreach (var writer in textWriters)
            {
                lock (locker)
                {
                    writer.Flush();
                }
            }
        }

        public override void Close()
        {
            foreach (var writer in textWriters)
            {
                lock (locker)
                {
                    writer.Close();
                    writer.Dispose();
                }

            }

            textWriters.Clear();

            ProConsole.WriteLine("[CLOSING] MultipleTextWriterStreams", ConsoleColor.Red);
        }

        public override Encoding Encoding
        {
            get { return Encoding.Unicode; }
        }
    }
}