using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Machine.Settings
{
    public abstract class BaseMachineSetting
    {
        //--
    }

    public abstract class MachineGroupOfSettings
    {
        //--
    }
    public class BooleanMachineSetting : BaseMachineSetting
    {
        private volatile bool internalValue;
        public bool Value { get => internalValue; set => internalValue = value; }

        public bool DefaultValue { get; set; }

        public BooleanMachineSetting()
        {
            //--
        }

        public BooleanMachineSetting(bool value, bool defaultValue)
        {
            Value = value;
            DefaultValue = defaultValue;
        }

        public BooleanMachineSetting(int value, int defaultValue)
        {
            Value = value > 0;
            DefaultValue = defaultValue > 0;
        }

        public BooleanMachineSetting(bool defaultValue)
        {
            DefaultValue = defaultValue;
        }

        public BooleanMachineSetting(int defaultValue)
        {
            DefaultValue = defaultValue > 0;
        }

        public static implicit operator bool(BooleanMachineSetting setting)
        {
            return setting.Value;
        }

        public static implicit operator string(BooleanMachineSetting setting)
        {
            return setting.Value.ToString();
        }
    }

    public class DebugBooleanMachineSetting : BaseMachineSetting
    {
        private volatile bool internalValue;
        public bool Value
        {
            get
            {
                return internalValue;
            }

            set
            {
                internalValue = value;
            }
        }

        public bool DefaultValue { get; set; }

        public DebugBooleanMachineSetting()
        {
            //--
        }

        public DebugBooleanMachineSetting(bool value, bool defaultValue)
        {
            Value = value;
            DefaultValue = defaultValue;
        }

        public DebugBooleanMachineSetting(int value, int defaultValue)
        {
            Value = value > 0;
            DefaultValue = defaultValue > 0;
        }

        public DebugBooleanMachineSetting(bool defaultValue)
        {
            DefaultValue = defaultValue;
        }

        public DebugBooleanMachineSetting(int defaultValue)
        {
            DefaultValue = defaultValue > 0;
        }

        public static implicit operator bool(DebugBooleanMachineSetting setting)
        {
            return setting.Value;
        }

        public static implicit operator string(DebugBooleanMachineSetting setting)
        {
            return setting.Value.ToString();
        }
    }
    public class FloatingMachineSetting : BaseMachineSetting
    {
        [JsonIgnore]
        public float? MinValue { get; set; }
        [JsonIgnore]
        public float? MaxValue { get; set; }

        private volatile float internalValue;
        public float Value { get => internalValue; set => internalValue = value; }
        public float DefaultValue { get; set; }

        public FloatingMachineSetting()
        {
            //--
        }

        public FloatingMachineSetting(float value, float defaultValue, float minValue, float maxValue)
               : this((double)value, (double)defaultValue, (double)minValue, (double)maxValue)
        {
            //--
        }

        public FloatingMachineSetting(double value, double defaultValue, double minValue, double maxValue)
        {
            MinValue = (float)minValue;
            MaxValue = (float)maxValue;
            DefaultValue = (float)defaultValue;
            Value = (float)value;

            CheckValue();
        }

        [JsonConstructor]
        public FloatingMachineSetting(double value, double defaultValue)
        {
            DefaultValue = (float)defaultValue;
            Value = (float)value;

            CheckValue();
        }

        public FloatingMachineSetting(double defaultValue, double minValue, double maxValue)
        {
            MinValue = (float)minValue;
            MaxValue = (float)maxValue;
            DefaultValue = (float)defaultValue;
            Value = (float)defaultValue;
        }

        public FloatingMachineSetting(double value)
        {
            Value = (float)value;

            CheckValue();
        }

        public void CheckValue()
        {
            if (MinValue != null && Value < MinValue)
            {
                Value = (int)MinValue;
            }
            else if (MaxValue != null && Value > MaxValue)
            {
                Value = (int)MaxValue;
            }
        }

        public static implicit operator double(FloatingMachineSetting setting)
        {
            return (double)setting.Value;
        }

        public static implicit operator float(FloatingMachineSetting setting)
        {
            return (float)setting.Value;
        }

        public static implicit operator string(FloatingMachineSetting setting)
        {
            return setting.Value.ToString("0.000");
        }

        public override string ToString()
        {
            return Value.ToString("0.000");
        }
    }

    public class NumericMachineSetting : BaseMachineSetting
    {
        [JsonIgnore]
        public int? MinValue { get; set; }
        [JsonIgnore]
        public int? MaxValue { get; set; }

        private volatile int internalValue = 0;
        public int Value { get => internalValue; set => internalValue = value; }
        public int DefaultValue { get; set; }

        public NumericMachineSetting()
        {
            // --
        }

        public NumericMachineSetting(int value)
        {
            Value = value;

            CheckValue();
        }

        public NumericMachineSetting(int value, int defaultValue, int minValue, int maxValue)
        {
            DefaultValue = defaultValue;
            MinValue = minValue;
            MaxValue = maxValue;

            Value = value;

            CheckValue();
        }

        public NumericMachineSetting(int defaultValue, int minValue, int maxValue)
        {
            DefaultValue = defaultValue;
            MinValue = minValue;
            MaxValue = maxValue;

            Value = defaultValue;

            CheckValue();
        }

        public void CheckValue()
        {
            if (MinValue != null && Value < MinValue)
            {
                Value = (int)MinValue;
            }
            else if (MaxValue != null && Value > MaxValue)
            {
                Value = (int)MaxValue;
            }
        }

        public static implicit operator int(NumericMachineSetting setting)
        {
            return setting.Value;
        }

        public static implicit operator string(NumericMachineSetting setting)
        {
            return setting.Value.ToString();
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }

    public class NumericOddMachineSetting : BaseMachineSetting
    {
        [JsonIgnore]
        public int? MinValue { get; set; }
        [JsonIgnore]
        public int? MaxValue { get; set; }

        private volatile int internalValue;
        public int Value
        {
            get => internalValue;
            set
            {
                if (value < MinValue)
                {
                    internalValue = (int)MinValue;
                }
                else if (value > MaxValue)
                {
                    internalValue = (int)MinValue;
                }
                else
                {
                    internalValue = Math.Sign(value) * (Math.Abs(value) + (value % 2));
                }
            }
        }

        public int DefaultValue { get; set; }

        public NumericOddMachineSetting()
        {
            //--
        }

        public NumericOddMachineSetting(int defaultValue, int minValue, int maxValue)
        {
            Value = defaultValue;
            DefaultValue = defaultValue;
            MinValue = minValue;
            MaxValue = maxValue;
        }

        public NumericOddMachineSetting(int value, int defaultValue, int minValue, int maxValue)
        {
            DefaultValue = defaultValue;
            MinValue = minValue;
            MaxValue = maxValue;

            Value = value;
        }

        public static implicit operator int(NumericOddMachineSetting setting)
        {
            return setting.Value;
        }

        public static implicit operator string(NumericOddMachineSetting setting)
        {
            return setting.Value.ToString();
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }

    public class NumericEvenMachineSetting : BaseMachineSetting
    {
        [JsonIgnore]
        public int? MinValue { get; set; }
        [JsonIgnore]
        public int? MaxValue { get; set; }

        private volatile int internalValue;
        public int Value
        {
            get => internalValue;
            set
            {
                if (value < MinValue)
                {
                    internalValue = (int)MinValue;
                }
                else if (value > MaxValue)
                {
                    internalValue = (int)MinValue;
                }
                else
                {
                    internalValue = Math.Sign(value) * (Math.Abs(value) - (value % 2));
                }
            }
        }

        public int DefaultValue { get; set; }

        public NumericEvenMachineSetting()
        {
            //--
        }

        public NumericEvenMachineSetting(int defaultValue, int minValue, int maxValue)
        {
            Value = defaultValue;
            DefaultValue = defaultValue;
            MinValue = minValue;
            MaxValue = maxValue;
        }

        public static implicit operator int(NumericEvenMachineSetting setting)
        {
            return setting.Value;
        }

        public static implicit operator string(NumericEvenMachineSetting setting)
        {
            return setting.Value.ToString();
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
