using System;

namespace Machine.Control.LowLevel.MachineController
{
    public enum Command
    {
        Hello = 0,
        //CheckLowLevelControlProcess=10,
        StartLowLevelControlProcess = 20,
        //KillLowLevelControlProcess=30,

        //GPIx5    //GPIx7
        ShutdownTwincat2 = 40
        //GPFx5    //GPFx7
    }
}
