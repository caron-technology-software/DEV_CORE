using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Caron.Cradle.Control.HighLevel
{
    public enum NullSubState : byte
    {
        Wait,
        WaitExit
    }

    public enum EmergencySubState : byte
    {
        Emergency,
    }

    //GPIx21
    public enum IOConfigSubState : byte
    {
        Running,
        WaitExit
    }
    //GPFx21

    public enum WaitMarchSubState : byte
    {
        WaitUI,
        SendMarch,
        WaitMarch,
        WaitExit
    }

    public enum ManualOperationsSubState : byte
    {
        Running,
        WaitExit
    }

    public enum NormalSubState : byte
    {
        Running,
        WaitExit
    }

    public enum LoadUnloadSubState : byte
    {
        Running,
        WaitExit
    }

    public enum SharpeningSubState : byte
    {
        Waiting,
        RunVersusMotorSide,
        RunVersusOperatorSide,
        Stopped,
        CutOffEnded,
        WaitExit
    }

    public enum CutOffSubState : byte
    {
        Waiting,
        RunVersusOperatorSide,
        RunVersusMotorSide,
        Stopped,
        CutOffEnded,
        WaitExit
    }

    /*
    STATE_CRADLE_JOG_DECELERATION = 0,
	STATE_CRADLE_JOG_JOGGING,
	STATE_CRADLE_JOG_STOPPING,
	STATE_CRADLE_JOG_MOTION_COMPLETED
    */
    public enum CradleJogSubState : byte
    {
        Jogging,
        Material,
        WaitButton,
        Completed,
        WaitExit
    }

    public enum CradleJogManualOperationsSubState : byte
    {
        Jogging,
        Material,
        WaitButton,
        Completed,
        WaitExit
    }

    public enum CradleJogLoadUnloadSubState : byte
    {
        Jogging,
        Material,
        WaitButton,
        Completed,
        WaitExit
    }

    public enum CradleRewindSubState : byte
    {
        Jogging,
        Completed,
        WaitExit
    }


}