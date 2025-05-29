using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caron.Cradle.Control.LowLevel
{
    public enum Command : byte
    {
        Null = 0,
        Hello = 1,

        HeartbeatEnable = 5,
        HearbeatPing = 6,
        HeartbeatDisable = 7,

        SetControlState = 10,
        EnableMarch = 11,

        ResetVirtualInputs = 20,
        WaitAction = 21,

        SpoonUp = 22,
        SpoonDown = 23,
        AlignmentMotorSide = 24,
        AlignmentOperatorSide = 25,
        CradleOverturningUp = 26,
        CradleOverturningDown = 27,
        TitanUp = 28,
        TitanDown = 29,

        //GPIx21
        DigitalOutput = 30,
        EnableIOSettings = 31,
        //GPFx21

        StopAllActions = 35,

        ZundStatus = 40,
        ZundError = 41,
        CradleCutterLock = 42,

        SetCradleScalingFactor = 50,
        SetStraightRoller = 51,

        /*
        CMD_AXIS_CRADLE_ENABLE = 100,
        CMD_AXIS_CRADLE_DISABLE = 101,
        CMD_AXIS_CRADLE_MOTION_CONSTANT_VELOCITY = 102,

        CMD_AXIS_CUTTER_ENABLE = 110,
        CMD_AXIS_CUTTER_DISABLE = 111,
        CMD_AXIS_CUTTER_MOTION_CONSTANT_VELOCITY = 112
        */

        CutVersusMotorSide = 200,
        CutVersusOperatorSide = 201,
        CutterStop = 202,

        CradleJog = 210,

        SetMachineSettings = 220,

        ExitControl = 230,
        Emergency = 250,

        Ack = 255,
    }
}