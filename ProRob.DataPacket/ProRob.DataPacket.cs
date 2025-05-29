using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ProRob.Communication
{
    public class DataPacket
    {
        private const byte StartHeader = 0xAA;
        private const byte StopHeader = 0x55;

        // StartHeader + Command + SubCommand + PayloadLength
        private const byte HeadSize = 1 + 1 + 1 + 4;

        // Checksum + StopHeader
        private const byte TailSize = 2 + 1;

        private List<byte> payload = new List<byte>(1024);

        public byte Command { get; private set; }
        public byte SubCommand { get; private set; }
        public uint PayloadLength { get => (uint)payload.Count; }

        public DataPacket(byte command, byte subCommand)
        {
            Command = command;
            SubCommand = subCommand;
        }

        public DataPacket(byte command, bool subCommand)
        {
            Command = command;
            SubCommand = subCommand ? (byte)0x01 : (byte)0x00;
        }

        public DataPacket(byte command)
        {
            Command = command;
            SubCommand = 0x00;
        }

        public void AddDataToPayload(object obj)
        {
            payload.AddRange(GetBytes(obj));
        }

        public void AddDataToPayload(int obj)
        {
            payload.AddRange(GetBytes(obj));
        }

        public void AddDataToPayload(byte[] bytes)
        {
            payload.AddRange(bytes);
        }

        public void AddDataToPayload(float[] array)
        {
            payload.AddRange(array.SelectMany(x => BitConverter.GetBytes(x)).ToArray());
        }

        public byte[] Create()
        {
            UInt16 crc = (UInt16)payload.Sum(x => x);

            return Combine(
                new byte[] { StartHeader, Command, SubCommand },
                BitConverter.GetBytes(PayloadLength),
                payload.ToArray(),
                BitConverter.GetBytes(crc),
                new byte[] { StopHeader });
        }

        private static byte[] Combine(params byte[][] arrays)
        {
            byte[] ret = new byte[arrays.Sum(x => x.Length)];
            int offset = 0;

            foreach (byte[] data in arrays)
            {
                Buffer.BlockCopy(data, 0, ret, offset, data.Length);
                offset += data.Length;
            }

            return ret;
        }

        public byte[] GetBytes(object obj)
        {
            int size = Marshal.SizeOf(obj);
            byte[] arr = new byte[size];

            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(obj, ptr, true);
            Marshal.Copy(ptr, arr, 0, size);
            Marshal.FreeHGlobal(ptr);

            return arr;
        }
    }
}
