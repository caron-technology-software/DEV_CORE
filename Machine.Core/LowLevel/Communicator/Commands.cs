using System.Linq;

namespace Machine.Control.LowLevel
{
    public enum MachineCommand : byte
    {
        Null = 0,
        Hello = 1,

        ExitControl = 230,
        Emergency = 250,

        Ack = 255,
    }
}