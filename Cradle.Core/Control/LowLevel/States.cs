using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Caron.Cradle.Control.LowLevel
{
    public enum ControlState : byte
    {
        DevMode = 0,
        Emergency,
        WaitMarch,
        WaitCommand,
        MotionEncoder,
        MotionEncoderDancer,
        MotionDancer,
        CutOff,
        CradleJog,
    }

    public enum CradleJogState : byte
    {
        WaitingCommand = 0,
        Deceleration,
        Jogging,
        MotionCompleted
    }

    public enum CutterState : byte
    {
        WaitingCommand = 0,
        InMotionVersusMotorSide,
        InMotionVersusOperatorSide,
        WaitZeroVelocity,
        SafetyReturnToOperatorSide,
        Stopped,
        MotionCompleted,
    }
}