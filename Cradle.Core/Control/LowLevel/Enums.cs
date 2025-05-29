using System.Reflection;

namespace Caron.Cradle.Control.LowLevel
{
    public enum AxisStatus : byte
    {
        Disabled = 0,
        TrajectoryMode,
        ReferenceMode,
        PositionMode,
        VelocityMode
    }

    public enum Encoders : byte
    {
        Cradle = 0,
        Table
    }

    public enum AnalogInput : byte
    {
        Dancer = 0,
    }

    public enum DigitalInput : byte
    {
        FuseCheckMotors = 0,
        TitanLimit = 1,
        PhotocellOperatorSide = 2,
        PhotocellMotorSide = 3,
        PhotocellMaterialPresence = 4,
        LimitCutterOperatorSide = 5,
        LimitCutterMotorSide = 6,
        LimitDancer = 7,
        LimitAlignmentOperatorSide = 8,
        LimitAlignmentMotorSide = 9,
        LimitOverturningOperatorSideLoad = 10,
        LimitOverturningOperatorSideUnload = 11,
        LimitSpoonUp = 12,
        LimitSpoonDown = 13,
        LimitOverturningMotorSideLoad = 14,
        LimitOverturningMotorSideUnload = 15,
        ZundEnable = 16,
        ZundCutOff = 17,
        //GPIx101 da inserire nei DigitalInputsToggles altrimenti va in crash l'applicativo:
        PhotocellRollPresence = 18,
        //GPFx101
    }

    public enum DigitalOutput : byte
    {
        MotorOverturningOpSideLoad = 0,
        MotorOverturningOpSideUnload = 1,
        MotorAlignmentOpSide = 2,
        MotorAlignmentMtSide = 3,
        TitanUp = 4,
        TitanDown = 5,
        OutFree01 = 6,
        OutFree02 = 7,
        MotorSpoonUp = 8,
        MotorSpoonDown = 9,
        MotorOverturningMtSideLoad = 10,
        MotorOverturningMtSideUnload = 11,
        MarchEnabled = 12,
        AxisCradleToCutterExchange = 13,
        OutFree03 = 14,
        OutFree04 = 15,
        ZundError = 16,
        ZundStatus = 17,
        CradleCutterLock = 18
    }

    public enum VirtualInput : byte
    {
        WaitAction = 0,
        SpoonUp = 1,
        SpoonDown = 2,
        CradleAlignmentMotorSide = 3,
        CradleAlignmentOperatorSide = 4,
        CradleOverturningUp = 5,
        CradleOverturningDown = 6,
        TitanUp = 7,
        TitanDown = 8,
        //GPIx21
        EnableIoConfig = 9,
        //GPFx21
    }

    public enum MachineInput : byte
    {
        MarchEnabled = 0,
        DriverFault = 1,
    }
}