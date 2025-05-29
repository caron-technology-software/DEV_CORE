using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Caron.Cradle.Control.LowLevel.Settings
{
    #region Models

    /*typedef struct GeneralCradleSettings_t
    {
	    double kp_position;
	    double ki_position;
	    double kd_position;
    	double kp_velocity;
	    double ki_velocity;
	    double kd_velocity;
	    double feed_forward_velocity_factor;
	    double min_dancer_value;
	    double max_dancer_value;
	    bool encoder_reversed;
	    double delta_cradle_scaling_factor_rewinding;
	    int intervention_delay_dancer_limit;
	    int table_encoder_pulses;
	    double r_table_pulley;
	    int cutter_motion_iter_start_first_slowdown;
        double cutter_motion_first_slowdown_velocity;
	    double cutter_motion_threshold_velocity_to_activate_slowdown;
    } GeneralCradleSettings_t;*/

    public class General
    {
        public double KpPosition { get; set; }
        public double KiPosition { get; set; }
        public double KdPosition { get; set; }
        public double KpVelocity { get; set; }
        public double KiVelocity { get; set; }
        public double KdVelocity { get; set; }
        public double FeedForwardVelocityFactor { get; set; }
        public double MinDancerValue { get; set; }
        public double MaxDancerValue { get; set; }
        public bool EncoderReversed { get; set; }
        public double DeltaCradleScalingFactorRewinding { get; set; }
        public int InterventionDelayDancerLimit { get; set; }
        public int TableEncoderPulses { get; set; }
        public double TableEquivalentRadius { get; set; }
        public int CutterMotionIterStartFirstSlowdown { get; set; }
        public double CutterMotionVelocityFirstSlowdown { get; set; }
        public double CutterMotionThresholdVelocityToActivateSlowdown { get; set; }

        //GPI31
        public int CradleEncoderPulses { get; set; }
        public double CradleEquivalentRadius { get; set; }
        //GPF31

        //GPIx125 2) 
        public float MaxDriverCommandSpeed; 
        //GPFx125

        //GPIx122 2) BIS X
        //public int EnableLimitator { get; set; }
        public int DeltaLimitator { get; set; }
        //GPFx122
    }

    /*typedef struct MotionEncoderSettings_t
    {
        double kp;
        double dancer_reference_target;
    }
    MotionEncoderSettings_t;*/
    public class MotionEncoder
    {
        public double Kp { get; set; }
        public double DancerReferenceTarget { get; set; }
    }

    /*typedef struct MotionDancerSettings_t
    {
	    double dancer_threshold_start;
	    double dancer_reference_target;
	    double dancer_zero;
	    uint16_t dancer_max_iter_zero_position;
	    double kp;
	    double ki;
	    double max_table_velocity;
	    double max_cradle_velocity;
    } MotionDancerSettings_t;*/

    public class MotionDancer
    {
        public double DancerThresholdStart { get; set; }
        public double DancerReferenceTarget { get; set; }
        public double DancerZero { get; set; }
        public UInt16 DancerMaxIterZeroPosition { get; set; }
        public double Kp { get; set; }
        public double Ki { get; set; }
        public double MaxTableVelocity { get; set; }
    }

    /*typedef struct MotionEncoderDancerSettings_t
    {
        double min_scale_factor_value;
        double max_scale_factor_value;
        double kp;
        double kp_rewinding;
        double ki;
        double dancer_reference_target;
        double min_dancer_normalized_value;
        double min_table_velocity;
        double delta_scaling_factor;
    }
    MotionEncoderDancerSettings_t;*/
    public class MotionEncoderDancer
    {
        public double MinScaleFactor { get; set; }
        public double MaxScaleFactor { get; set; }
        public double Kp { get; set; }
        public double KpRewinding { get; set; }
        public double Ki { get; set; }
        public double DancerReferenceTarget { get; set; }
        public double MinDancerNormalizedValue { get; set; }
        public double MinTableVelocity { get; set; }
        public double DeltaCradleScalingFactor { get; set; }
    }
    #endregion

    /*typedef struct MachineSettings_t
    {
        GeneralCradleSettings_t general;
        MotionEncoderSettings_t motion_encoder;
        MotionDancerSettings_t motion_dancer;
        MotionEncoderDancerSettings_t motion_encoder_dancer;
    }
    MachineSettings_t;*/

    public class ControlSettings
    {
        public General General { get; set; } = new General();
        public MotionEncoder MotionEncoder { get; set; } = new MotionEncoder();
        public MotionDancer MotionDancer { get; set; } = new MotionDancer();
        public MotionEncoderDancer MotionEncoderDancer { get; set; } = new MotionEncoderDancer();

        #region Payload
        private static byte[] GetBytes(ControlSettings settings)
        {
            var buffer = new List<byte>();

            var gen = settings.General;
            buffer.AddRange(BitConverter.GetBytes(gen.KpPosition));
            buffer.AddRange(BitConverter.GetBytes(gen.KiPosition));
            buffer.AddRange(BitConverter.GetBytes(gen.KdPosition));
            buffer.AddRange(BitConverter.GetBytes(gen.KpVelocity));
            buffer.AddRange(BitConverter.GetBytes(gen.KiVelocity));
            buffer.AddRange(BitConverter.GetBytes(gen.KdVelocity));
            buffer.AddRange(BitConverter.GetBytes(gen.FeedForwardVelocityFactor));
            buffer.AddRange(BitConverter.GetBytes(gen.MinDancerValue));
            buffer.AddRange(BitConverter.GetBytes(gen.MaxDancerValue));
            buffer.AddRange(BitConverter.GetBytes(gen.EncoderReversed));
            buffer.AddRange(BitConverter.GetBytes(gen.DeltaCradleScalingFactorRewinding));
            buffer.AddRange(BitConverter.GetBytes(gen.InterventionDelayDancerLimit));
            buffer.AddRange(BitConverter.GetBytes(gen.TableEncoderPulses));
            buffer.AddRange(BitConverter.GetBytes(gen.TableEquivalentRadius));
            buffer.AddRange(BitConverter.GetBytes(gen.CutterMotionIterStartFirstSlowdown));
            buffer.AddRange(BitConverter.GetBytes(gen.CutterMotionVelocityFirstSlowdown));
            buffer.AddRange(BitConverter.GetBytes(gen.CutterMotionThresholdVelocityToActivateSlowdown));
            //GPI31
            buffer.AddRange(BitConverter.GetBytes(gen.CradleEncoderPulses));
            buffer.AddRange(BitConverter.GetBytes(gen.CradleEquivalentRadius));
            //GPF31
            //GPIx125 2) 
            buffer.AddRange(BitConverter.GetBytes(gen.MaxDriverCommandSpeed));
            //GPFx125
            //GPIx122 2) BIS X
            //buffer.AddRange(BitConverter.GetBytes(gen.EnableLimitator));
            buffer.AddRange(BitConverter.GetBytes(gen.DeltaLimitator));
            //GPFx122

            var me = settings.MotionEncoder;
            buffer.AddRange(BitConverter.GetBytes(me.Kp));
            buffer.AddRange(BitConverter.GetBytes(me.DancerReferenceTarget));

            var md = settings.MotionDancer;
            buffer.AddRange(BitConverter.GetBytes(md.DancerThresholdStart));
            buffer.AddRange(BitConverter.GetBytes(md.DancerReferenceTarget));
            buffer.AddRange(BitConverter.GetBytes(md.DancerZero));
            buffer.AddRange(BitConverter.GetBytes(md.DancerMaxIterZeroPosition));
            buffer.AddRange(BitConverter.GetBytes(md.Kp));
            buffer.AddRange(BitConverter.GetBytes(md.Ki));
            buffer.AddRange(BitConverter.GetBytes(md.MaxTableVelocity));

            var med = settings.MotionEncoderDancer;
            buffer.AddRange(BitConverter.GetBytes(med.MinScaleFactor));
            buffer.AddRange(BitConverter.GetBytes(med.MaxScaleFactor));
            buffer.AddRange(BitConverter.GetBytes(med.Kp));
            buffer.AddRange(BitConverter.GetBytes(med.KpRewinding));
            buffer.AddRange(BitConverter.GetBytes(med.Ki));
            buffer.AddRange(BitConverter.GetBytes(med.DancerReferenceTarget));
            buffer.AddRange(BitConverter.GetBytes(med.MinDancerNormalizedValue));
            buffer.AddRange(BitConverter.GetBytes(med.MinTableVelocity));
            buffer.AddRange(BitConverter.GetBytes(med.DeltaCradleScalingFactor));

            return buffer.ToArray();
        }

        public static byte[] GetBytes(HighLevel.Settings.LowLevelMotionSettings lowLevelMotionSettings, HighLevel.Settings.FunctionsEnabled rootFunctionEnabled, HighLevel.Settings.MachineParameters machineParameters)
        {
            ControlSettings settings = new ControlSettings();

            var gen = settings.General;
            gen.KpPosition = lowLevelMotionSettings.Axis.KpPosition.Value;
            gen.KiPosition = lowLevelMotionSettings.Axis.KiPosition.Value;
            gen.KdPosition = lowLevelMotionSettings.Axis.KdPosition.Value;
            gen.KpVelocity = lowLevelMotionSettings.Axis.KpVelocity.Value;
            gen.KiVelocity = lowLevelMotionSettings.Axis.KiVelocity.Value;
            gen.KdVelocity = lowLevelMotionSettings.Axis.KdVelocity.Value;
            gen.FeedForwardVelocityFactor = lowLevelMotionSettings.Axis.FeedForwardVelocityFactor.Value;
            gen.MinDancerValue = lowLevelMotionSettings.General.MinDancerValue.Value;
            gen.MaxDancerValue = lowLevelMotionSettings.General.MaxDancerValue.Value;
            gen.EncoderReversed = rootFunctionEnabled.ReverseEncoder.Value;
            gen.DeltaCradleScalingFactorRewinding = lowLevelMotionSettings.General.DeltaCradleScalingFactorRewinding.Value;
            gen.InterventionDelayDancerLimit = machineParameters.InterventionDelayDancerLimit.Value;
            gen.TableEncoderPulses = machineParameters.TableEncoderPulses.Value * 4;

            double r = machineParameters.TablePulleyRadius;
            double n1 = machineParameters.NumberOfPulleyTeethsEncoderSide;
            double n2 = machineParameters.NumberOfPulleyTeethsTransmissionSide;
            gen.TableEquivalentRadius = r * n1 / n2 / 1000.0;

            gen.CutterMotionIterStartFirstSlowdown = machineParameters.CutterMotionIterStartFirstSlowdown;
            gen.CutterMotionVelocityFirstSlowdown = machineParameters.CutterMotionVelocityFirstSlowdown;
            gen.CutterMotionThresholdVelocityToActivateSlowdown = machineParameters.CutterMotionThresholdVelocityToActivateSlowdown;

            //GPI31
            gen.CradleEncoderPulses = machineParameters.CradleEncoderPulses.Value * 4;

            double rx = machineParameters.CradlePulleyRadius;
            //double n1x = machineParameters.NumberOfPulleyTeethsEncoderSide;
            //double n2x = machineParameters.NumberOfPulleyTeethsTransmissionSide;
            //gen.CradleEquivalentRadius = rx * n1x / n2x / 1000.0;
            gen.CradleEquivalentRadius = rx;
            //GPF31

            //GPIx125 2) 
            float FrequencyToControlWord = 80.0f;
            gen.MaxDriverCommandSpeed = (float)machineParameters.InverterFrequency.Value * FrequencyToControlWord;
            //GPFx125

            //GPIx122 2) BIS X
            //if (machineParameters.EnableLimitator.Value)
            //{
            //    gen.EnableLimitator = 1;
            //}
            //else
            //{
            //    gen.EnableLimitator = 0;
            //}
            gen.DeltaLimitator = machineParameters.DeltaLimitator;
            //GPFx122

            var me = settings.MotionEncoder;
            me.Kp = lowLevelMotionSettings.Encoder.KpRewinding.Value;
            me.DancerReferenceTarget = lowLevelMotionSettings.Encoder.DancerReferenceTarget.Value;

            var md = settings.MotionDancer;
            md.DancerThresholdStart = lowLevelMotionSettings.Dancer.DancerThresholdStart.Value;
            md.DancerReferenceTarget = lowLevelMotionSettings.Dancer.DancerReferenceTarget.Value;
            md.DancerZero = lowLevelMotionSettings.Dancer.DancerZero.Value;
            md.DancerMaxIterZeroPosition = (ushort)lowLevelMotionSettings.Dancer.DancerMaxIterZeroPosition.Value;
            md.Kp = lowLevelMotionSettings.Dancer.Kp.Value;
            md.Ki = lowLevelMotionSettings.Dancer.Ki.Value;
            md.MaxTableVelocity = lowLevelMotionSettings.Dancer.MaxTableVelocity.Value;

            var med = settings.MotionEncoderDancer;
            med.MinScaleFactor = lowLevelMotionSettings.EncoderDancer.MinScaleFactor.Value;
            med.MaxScaleFactor = lowLevelMotionSettings.EncoderDancer.MaxScaleFactor.Value;
            med.Kp = lowLevelMotionSettings.EncoderDancer.Kp.Value;
            med.KpRewinding = lowLevelMotionSettings.EncoderDancer.KpRewinding.Value;
            med.Ki = lowLevelMotionSettings.EncoderDancer.Ki.Value;
            med.DancerReferenceTarget = lowLevelMotionSettings.EncoderDancer.DancerReferenceTarget.Value;
            med.MinDancerNormalizedValue = lowLevelMotionSettings.EncoderDancer.MinDancerNormalizedValue.Value;
            med.MinTableVelocity = lowLevelMotionSettings.EncoderDancer.MinTableVelocity.Value;
            med.DeltaCradleScalingFactor = lowLevelMotionSettings.EncoderDancer.DeltaCradleScalingFactor.Value;

            return GetBytes(settings);
        }
        #endregion
    }
}