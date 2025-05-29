using Machine.Common;
using Machine.Settings;
using System.Runtime.Remoting.Contexts;

namespace Caron.Cradle.Control.HighLevel.Settings
{
    [Synchronization()]
    public class MachineParameters : MachineGroupOfSettings
    {
        [UserAccess(UserType.Distributor)]
        public BooleanMachineSetting CradleOperationsWhenOutOfPosition { get; set; } = new BooleanMachineSetting(false);

        [UserAccess(UserType.Distributor)]
        public FloatingMachineSetting CradleJogVelocity { get; set; } = new FloatingMachineSetting(0.1, 0.1, 1.0);

        [UserAccess(UserType.Distributor)]
        public FloatingMachineSetting CradleJogFastVelocity { get; set; } = new FloatingMachineSetting(0.3, 0.1, 1.0);

        [UserAccess(UserType.Distributor)]
        public NumericMachineSetting OverturningEnabledWithMaterialPresence { get; set; } = new NumericMachineSetting(0, 0, 1);

        [UserAccess(UserType.Distributor)]
        public BooleanMachineSetting AutoCenteringEnabled { get; set; } = new BooleanMachineSetting(false);

        [UserAccess(UserType.Distributor)]
        public BooleanMachineSetting AutoCradleOverturningHandling { get; set; } = new BooleanMachineSetting(true);

        [UserAccess(UserType.Distributor)]
        public NumericMachineSetting IntervalMotorSideAutoCentering { get; set; } = new NumericMachineSetting(3500, 0, 20000);

        [UserAccess(UserType.Distributor)]
        public NumericMachineSetting LengthMaterialSupplyOnLoadUnload { get; set; } = new NumericMachineSetting(230, 0, 1000);

        [UserAccess(UserType.Distributor)]
        public BooleanMachineSetting FastRewindLockWithCradleOpen { get; set; } = new BooleanMachineSetting(false);

        [UserAccess(UserType.Distributor)]
        public BooleanMachineSetting EnableExternalInputsOutputs { get; set; } = new BooleanMachineSetting(true);

        [UserAccess(UserType.Distributor)]
        public BooleanMachineSetting EnableCutterCradleLock { get; set; } = new BooleanMachineSetting(true);

        [UserAccess(UserType.Distributor)]
        public BooleanMachineSetting AutoTitanHandling { get; set; } = new BooleanMachineSetting(false);

        [UserAccess(UserType.Distributor)]
        public NumericMachineSetting MinCradleVelocityAutoCentering { get; set; } = new NumericMachineSetting(3, 1, 100);

        [UserAccess(UserType.Distributor)]
        public NumericMachineSetting MinCradleVelocityStopped { get; set; } = new NumericMachineSetting(3, 1, 100);

        [UserAccess(UserType.Distributor)]
        public NumericMachineSetting WaitRelaysAfterRisingEdge { get; set; } = new NumericMachineSetting(100, 0, 1000);

        [UserAccess(UserType.Distributor)]
        public NumericMachineSetting WaitRelaysFallingEdge { get; set; } = new NumericMachineSetting(100, 0, 1000);

        [UserAccess(UserType.Distributor)]
        public NumericMachineSetting MinCutterVelocity { get; set; } = new NumericMachineSetting(35, 10, 100);

        [UserAccess(UserType.Distributor)]
        public FloatingMachineSetting DeltaCradleScalingFactorRewinding { get; set; } = new FloatingMachineSetting(-0.05, -0.15, 0.15);

        [UserAccess(UserType.Distributor)]
        public NumericMachineSetting InterventionDelayDancerLimit { get; set; } = new NumericMachineSetting(800, 1, 15_000);

        //GPI31
        [UserAccess(UserType.Distributor)]
        public NumericMachineSetting CradleEncoderPulses { get; set; } = new NumericMachineSetting(500, 0, 1000);

        [UserAccess(UserType.Distributor)]
        public FloatingMachineSetting CradlePulleyRadius { get; set; } = new FloatingMachineSetting(0.02080, 0.00001, 1.00001);
        //GPF31

        //GPIx125 2) 
        [UserAccess(UserType.Distributor)]
        public NumericMachineSetting InverterFrequency { get; set; } = new NumericMachineSetting(87, 1, 400);
        //GPFx125

        //GPIx122 2) BIS X
        //[UserAccess(UserType.Distributor)]
        //public BooleanMachineSetting EnableLimitator { get; set; } = new BooleanMachineSetting(true, true);

        [UserAccess(UserType.Distributor)]
        public NumericMachineSetting DeltaLimitator { get; set; } = new NumericMachineSetting(0, 0, 10_0000);
        //GPFx122

        [UserAccess(UserType.Distributor)]
        public NumericMachineSetting TableEncoderPulses { get; set; } = new NumericMachineSetting(3000, 500, 250_000);

        [UserAccess(UserType.Distributor)]
        public FloatingMachineSetting TablePulleyRadius { get; set; } = new FloatingMachineSetting(50, 1, 500);

        [UserAccess(UserType.Distributor)]
        public NumericMachineSetting NumberOfPulleyTeethsEncoderSide { get; set; } = new NumericMachineSetting(1, 1, 500);

        [UserAccess(UserType.Distributor)]
        public NumericMachineSetting NumberOfPulleyTeethsTransmissionSide { get; set; } = new NumericMachineSetting(1, 1, 500);

        [UserAccess(UserType.Distributor)]
        public FloatingMachineSetting CradleSpeedPreFeed { get; set; } = new FloatingMachineSetting(0.035, 0.001, 0.25);

        [UserAccess(UserType.Distributor)]
        public FloatingMachineSetting CradleSpeedPositionDancerBar { get; set; } = new FloatingMachineSetting(0.03, 0.001, 0.25);

        [UserAccess(UserType.Distributor)]
        public FloatingMachineSetting PositionDancerLoadWithPreFeed { get; set; } = new FloatingMachineSetting(0.07, 0.05, 0.25);

        [UserAccess(UserType.Distributor)]
        public FloatingMachineSetting PositionDancerUnloadWithPreFeed { get; set; } = new FloatingMachineSetting(0.03, 0.01, 0.04);

        [UserAccess(UserType.Distributor)]
        public NumericMachineSetting WaitBetweenLoadAndUnloadBancebar { get; set; } = new NumericMachineSetting(500, 0, 2500);

        [UserAccess(UserType.Distributor, UserType.Manufacturer)]
        public NumericMachineSetting CutterMotionIterStartFirstSlowdown { get; set; } = new NumericMachineSetting(1200, 100, 10000);

        [UserAccess(UserType.Distributor, UserType.Manufacturer)]
        public FloatingMachineSetting CutterMotionVelocityFirstSlowdown { get; set; } = new FloatingMachineSetting(0.5, 0.05, 1.0);

        [UserAccess(UserType.Distributor, UserType.Manufacturer)]
        public FloatingMachineSetting CutterMotionThresholdVelocityToActivateSlowdown { get; set; } = new FloatingMachineSetting(0.6, 0.05, 1.0);

        [UserAccess(UserType.Distributor, UserType.Manufacturer)]
        public FloatingMachineSetting CheckUntilPhotocellMaterialPresence { get; set; } = new FloatingMachineSetting(250, 10, 2000);

        //MMIx02
        [UserAccess(UserType.Distributor, UserType.Manufacturer)]
        public FloatingMachineSetting CheckUntilPhotocelRollPresence { get; set; } = new FloatingMachineSetting(250, 10, 2000);
        //MMFx02

        //MMIx03
        [UserAccess(UserType.Distributor, UserType.Manufacturer)]
        public BooleanMachineSetting AutoCenteringWithoutPhotocellAllineament { get; set; } = new BooleanMachineSetting(false);
        //MMFx03

        public MachineParameters()
        {
            //--
        }
    }
}
