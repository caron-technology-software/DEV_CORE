using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Machine.Common;
using Machine.Settings;

namespace Caron.Cradle.Control.HighLevel.Settings
{
    public class LowLevelMotionSettings : MachineGroupOfSettings
    {
        [UserAccess(UserType.Distributor, UserType.Manufacturer)]
        public GeneralCradleSettings General { get; set; } = new GeneralCradleSettings();

        [UserAccess(UserType.Distributor, UserType.Manufacturer)]
        public AxisSettings Axis { get; set; } = new AxisSettings();

        [UserAccess(UserType.Distributor, UserType.Manufacturer)]
        public MotionEncoderSettings Encoder { get; set; } = new MotionEncoderSettings();

        [UserAccess(UserType.Distributor, UserType.Manufacturer)]
        public MotionDancerSettings Dancer { get; set; } = new MotionDancerSettings();

        [UserAccess(UserType.Distributor, UserType.Manufacturer)]
        public MotionEncoderDancerSettings EncoderDancer { get; set; } = new MotionEncoderDancerSettings();
    }

    public class GeneralCradleSettings : MachineGroupOfSettings
    {
        [UserAccess(UserType.Distributor, UserType.Manufacturer)]
        public FloatingMachineSetting MinDancerValue { get; set; } = new FloatingMachineSetting(0, 0.0, 10.0);

        [UserAccess(UserType.Distributor, UserType.Manufacturer)]
        public FloatingMachineSetting MaxDancerValue { get; set; } = new FloatingMachineSetting(0, 0.0, 10.0);

        [UserAccess(UserType.Distributor, UserType.Manufacturer)]
        public FloatingMachineSetting DeltaCradleScalingFactorRewinding { get; set; } = new FloatingMachineSetting(-0.05, -0.15, 0.15);
    }

    public class AxisSettings : MachineGroupOfSettings
    {
        [UserAccess(UserType.Distributor, UserType.Manufacturer)]
        public FloatingMachineSetting KpPosition { get; set; } = new FloatingMachineSetting(55, 0.0, 250.0);

        [UserAccess(UserType.Distributor, UserType.Manufacturer)]
        public FloatingMachineSetting KiPosition { get; set; } = new FloatingMachineSetting(5, 0.0, 250.0);

        [UserAccess(UserType.Distributor, UserType.Manufacturer)]
        public FloatingMachineSetting KdPosition { get; set; } = new FloatingMachineSetting(0.0001, 0.0, 250.0);

        [UserAccess(UserType.Distributor, UserType.Manufacturer)]
        public FloatingMachineSetting KpVelocity { get; set; } = new FloatingMachineSetting(10, 0.0, 250.0);

        [UserAccess(UserType.Distributor, UserType.Manufacturer)]
        public FloatingMachineSetting KiVelocity { get; set; } = new FloatingMachineSetting(5, 0.0, 250.0);

        [UserAccess(UserType.Distributor, UserType.Manufacturer)]
        public FloatingMachineSetting KdVelocity { get; set; } = new FloatingMachineSetting(0.0001, 0.0, 250.0);

        [UserAccess(UserType.Distributor, UserType.Manufacturer)]
        public FloatingMachineSetting FeedForwardVelocityFactor { get; set; } = new FloatingMachineSetting(1.0, -1.5, 1.5);
    }

    public class MotionDancerSettings : MachineGroupOfSettings
    {
        [UserAccess(UserType.Distributor, UserType.Manufacturer)]
        public FloatingMachineSetting DancerThresholdStart { get; set; } = new FloatingMachineSetting(0.01, 0.01, 1.0);

        [UserAccess(UserType.Distributor, UserType.Manufacturer)]
        public FloatingMachineSetting DancerReferenceTarget { get; set; } = new FloatingMachineSetting(0.04, 0.01, 1.0);

        [UserAccess(UserType.Distributor, UserType.Manufacturer)]
        public FloatingMachineSetting DancerZero { get; set; } = new FloatingMachineSetting(0.025, 0.001, 0.25);

        [UserAccess(UserType.Distributor, UserType.Manufacturer)]
        public NumericMachineSetting DancerMaxIterZeroPosition { get; set; } = new NumericMachineSetting(25, 0, 500);

        [UserAccess(UserType.Distributor, UserType.Manufacturer)]
        public FloatingMachineSetting Kp { get; set; } = new FloatingMachineSetting(0.15, 0, 15);

        [UserAccess(UserType.Distributor, UserType.Manufacturer)]
        public FloatingMachineSetting Ki { get; set; } = new FloatingMachineSetting(0.35, 0, 15);

        [UserAccess(UserType.Distributor, UserType.Manufacturer)]
        public FloatingMachineSetting MaxTableVelocity { get; set; } = new FloatingMachineSetting(450, 0, 2000);
    }

    public class MotionEncoderSettings : MachineGroupOfSettings
    {
        [UserAccess(UserType.Distributor, UserType.Manufacturer)]
        public FloatingMachineSetting KpRewinding { get; set; } = new FloatingMachineSetting(1.0, 0, 15);

        [UserAccess(UserType.Distributor, UserType.Manufacturer)]
        public FloatingMachineSetting DancerReferenceTarget { get; set; } = new FloatingMachineSetting(0.25, 0.001, 1.0);
    }

    public class MotionEncoderDancerSettings : MachineGroupOfSettings
    {
        [UserAccess(UserType.Distributor, UserType.Manufacturer)]
        public FloatingMachineSetting MinScaleFactor { get; set; } = new FloatingMachineSetting(0.1, 0, 2.0);

        [UserAccess(UserType.Distributor, UserType.Manufacturer)]
        public FloatingMachineSetting MaxScaleFactor { get; set; } = new FloatingMachineSetting(2.0, 0.1, 10.0);

        [UserAccess(UserType.Distributor, UserType.Manufacturer)]
        public FloatingMachineSetting Kp { get; set; } = new FloatingMachineSetting(1.5, 0, 15);

        [UserAccess(UserType.Distributor, UserType.Manufacturer)]
        public FloatingMachineSetting KpRewinding { get; set; } = new FloatingMachineSetting(3.5, 0, 15);

        [UserAccess(UserType.Distributor, UserType.Manufacturer)]
        public FloatingMachineSetting Ki { get; set; } = new FloatingMachineSetting(0.01, 0, 15);

        [UserAccess(UserType.Distributor, UserType.Manufacturer)]
        public FloatingMachineSetting DancerReferenceTarget { get; set; } = new FloatingMachineSetting(0.07, 0.001, 1.0);

        [UserAccess(UserType.Distributor, UserType.Manufacturer)]
        public FloatingMachineSetting MinDancerNormalizedValue { get; set; } = new FloatingMachineSetting(0.001, 0.0001, 1.0);

        [UserAccess(UserType.Distributor, UserType.Manufacturer)]
        public FloatingMachineSetting MinTableVelocity { get; set; } = new FloatingMachineSetting(0.05, 0, 10);

        [UserAccess(UserType.Distributor, UserType.Manufacturer)]
        public FloatingMachineSetting DeltaCradleScalingFactor { get; set; } = new FloatingMachineSetting(-0.05, -0.30, 0.30);
    }
}
