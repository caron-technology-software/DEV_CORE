using System;

namespace Caron.Cradle.Control.HighLevel
{
    public enum JogState : byte
    {
        Stopped = 0,
        AcwMode,
        CwMode,
    }

    public enum WorkingMode : byte
    {
        Encoder = 0,
        EncoderDancebar,
        Dancebar,
    }
}