using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Machine.UI.Controls.Extensions;

namespace Machine.UI.Controls
{
    public partial class MachinePropertyNumericEditBox : UserControl
    {
        //GPIx258
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //GPIx258
        public bool IsImperialYard { get; set; } = false;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool NeedYard { get; set; } = false;
        //GPFx258
        public class MachinePropertyBoxEvent : EventArgs
        {
            public float PropertyValue { get; set; }
            public MachinePropertyBoxEvent(float propertyValue)
            {
                PropertyValue = propertyValue;
            }
        }

        public event EventHandler<MachinePropertyBoxEvent> ValueChanged;

        private void OnValueChanged(MachinePropertyBoxEvent e)
        {
            if (ValueChangedEventEnabled)
            {
                ValueChanged?.Invoke(this, e);
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool ValueChangedEventEnabled { get; set; } = false;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string UnitMeasure { get; set; } = String.Empty;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public float? MinValue { get; set; } = null;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public float? MaxValue { get; set; } = null;

        public void SetValueWithoutEvent(float value)
        {
            if (propertyValue.NearlyEquals(value))
            {
                return;
            }

            internalSetPropertyValue(value);
        }


        private string propertyName = String.Empty;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string PropertyName
        {
            get
            {
                return propertyName;
            }
            set
            {
                if (propertyName != value)
                {
                    propertyName = value;
                    slPropertyName.Text = propertyName;
                }
            }
        }

        private float propertyValue = float.MinValue;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public float PropertyValue
        {
            get
            {
                return propertyValue;
            }

            set
            {
                if (propertyValue.NearlyEquals(value))
                {
                    return;
                }

                internalSetPropertyValue(value);
                OnValueChanged(new MachinePropertyBoxEvent(value));
            }
        }

        public MachinePropertyNumericEditBox()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
        }

        public MachinePropertyNumericEditBox(string propertyName)
        {
            InitializeComponent();

            PropertyName = propertyName;
        }

        private void SpreaderEditBox_Load(object sender, EventArgs e)
        {
            slPropertyName.BorderStyle = BorderStyle.None;
            slPropertyValue.BorderStyle = BorderStyle.FixedSingle;
            //GPIx258
            slPropertyValueYard.BorderStyle = BorderStyle.FixedSingle;
            //GPFx258
            TabStop = false;
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            BorderStyle = BorderStyle.None;
            Refresh();
        }

        private void internalSetPropertyValue(float value)
        {
            propertyValue = value;
            slPropertyValue.Text = propertyValue.ToString();
            slPropertyValue.Refresh();
            //GPIx258 -> da convertire stringa yard
            decimal propertyValueInInch = (decimal)(propertyValue / 25.4f);
            decimal propertyValueInInYard = Math.Truncate((decimal)((propertyValue / 25.4f) / 36f));
            decimal propertyValueInInchRemaining = (decimal)(propertyValue / 25.4f) - (Math.Truncate((decimal)((propertyValue / 25.4f) / 36f)) * 36m);
            decimal propertyValueInInchRemainingIntPart = Math.Truncate(propertyValueInInchRemaining);
            decimal propertyValueInInchRemainingDecimalPart = propertyValueInInchRemaining - Math.Truncate(propertyValueInInchRemaining);

            propertyValueInInch = Math.Round(propertyValueInInch, 5, MidpointRounding.AwayFromZero);
            string str01 = $"{propertyValueInInYard:0}yd{propertyValueInInchRemainingIntPart:0}\"{propertyValueInInchRemainingDecimalPart:.000}";
            str01 = str01.Replace(".", "").Replace(",","");
            slPropertyValueYard.Text = str01;
            //slPropertyValueYard.Text = $"{propertyValueInInch:0.000} inch";
            slPropertyValueYard.Refresh();
            //GPFx258
        }
        private void ShowKeyboard()
        {
            //GPIx258
            if (!IsImperialYard)
            {
                using (var keyb = new TouchNumericKeyboard(PropertyName, PropertyValue))
                {
                    keyb.KeyCommaEnabled = false;
                    keyb.KeyPlusMinusEnabled = false;

                    if (this.MaxValue != null)
                    {
                        keyb.MaxValue = MaxValue;
                    }

                    if (this.MinValue != null)
                    {
                        keyb.MinValue = MinValue;
                    }

                    keyb.UnitMeasure = UnitMeasure;

                    keyb.ShowDialog();

                    PropertyValue = keyb.Value;
                }
            }
            else
            {
                //int yard = 1;
                //decimal valueInMm = (decimal)(yard * 36f * 25.4f);
                decimal valueInMm = 0;
                float propertyValue = PropertyValue;
                decimal valueInInch = (decimal)(propertyValue / 25.4f);
                decimal valueInYard =  Math.Truncate((decimal)((propertyValue / 25.4f) / 36f));
                decimal valueInInchRemaining = (decimal)(propertyValue / 25.4f) - (Math.Truncate((decimal)((propertyValue / 25.4f) / 36f)) * 36m );
                valueInInchRemaining = Math.Round(valueInInchRemaining, 5, MidpointRounding.AwayFromZero);
                decimal valueInInchRemainingIntPart = Math.Truncate(valueInInchRemaining);
                decimal valueInInchIntPart = Math.Truncate(valueInInch);
                decimal valueInInchRemainingDecimalPart = valueInInchRemaining - Math.Truncate(valueInInchRemaining);

                if (NeedYard)
                {
                    //using (var keyb = new TouchNumericKeyboard(PropertyName, PropertyValue))
                    using (var keyb = new TouchNumericKeyboard(PropertyName, (float)valueInYard))
                    {
                        keyb.KeyCommaEnabled = false;
                        keyb.KeyPlusMinusEnabled = false;

                        if (this.MaxValue != null)
                        {
                            float maxValue = (float)MaxValue;
                            decimal valueInYardMax = Math.Truncate((decimal)((maxValue / 25.4f) / 36f));
                            keyb.MaxValue = (float)valueInYardMax;
                        }

                        if (this.MinValue != null)
                        {
                            float minValue = (float)MinValue;
                            decimal valueInYardMin = Math.Truncate((decimal)((minValue / 25.4f) / 36f));
                            keyb.MinValue = (float)valueInYardMin;
                        }

                        keyb.UnitMeasure = "Yard";

                        DialogResult dialogResult;
                        dialogResult = keyb.ShowDialog();


                        //PropertyValue = keyb.Value;
                        valueInMm = (decimal)(keyb.Value * 36f * 25.4f);
                    }
                }

                float chooseValue01 = 0;
                if (NeedYard)
                {
                    chooseValue01 = (float)valueInInchRemainingIntPart;
                }
                else
                {
                    chooseValue01 = (float)valueInInchIntPart;
                }
                if (true)
                {
                    //using (var keyb = new TouchNumericKeyboard(PropertyName, PropertyValue))
                    using (var keyb = new TouchNumericKeyboard(PropertyName, chooseValue01))
                    {
                        keyb.KeyCommaEnabled = false;
                        keyb.KeyPlusMinusEnabled = false;

                        if (this.MaxValue != null)
                        {
                            float maxValue = (float)MaxValue;
                            decimal valueInInchMax = Math.Truncate((decimal)((maxValue / 25.4f)));
                            if (valueInInchMax < 0)
                            {
                                keyb.MaxValue = 0f;
                            }
                            else
                            {
                                keyb.MaxValue = (float)valueInInchMax;
                            }
                        }

                        if (this.MinValue != null)
                        {
                            float minValue = (float)MinValue;
                            decimal valueInInchMin = Math.Truncate((decimal)((minValue / 25.4f)));
                            if (valueInInchMin > 0)
                            {
                                keyb.MinValue = 0f;
                            }
                            else
                            {
                                keyb.MinValue = (float)valueInInchMin;
                            }
                        }

                        keyb.UnitMeasure = "Inch";

                        keyb.ShowDialog();

                        //PropertyValue = keyb.Value;
                        valueInMm = valueInMm + (decimal)(keyb.Value * 25.4f);
                    }
                }

                if (true)
                {
                    //using (var keyb = new TouchNumericKeyboard(PropertyName, PropertyValue))
                    using (var keyb = new TouchNumericKeyboard(PropertyName, (float)valueInInchRemainingDecimalPart))
                    {
                        keyb.KeyCommaEnabled = true;
                        keyb.KeyPlusMinusEnabled = false;

                        if (true)
                        {
                            keyb.MaxValue = 1f;
                        }

                        if (true)
                        {
                            keyb.MinValue = 0f;
                        }

                        keyb.UnitMeasure = "Dec. Inch";

                        keyb.ShowDialog();


                        //PropertyValue = keyb.Value;
                        valueInMm = valueInMm + (decimal)(keyb.Value * 25.4f);

                        if (this.MaxValue != null)
                        {
                            float maxValue = (float)MaxValue;
                            decimal valueInMmMax = (decimal)maxValue;
                            if (valueInMm > valueInMmMax)
                            {
                                valueInMm = valueInMmMax;
                            }
                        }
                        if (this.MinValue != null)
                        {
                            float minValue = (float)MinValue;
                            decimal valueInMmMax = (decimal)minValue;
                            if (valueInMm < valueInMmMax)
                            {
                                valueInMm = valueInMmMax;
                            }
                        }

                        valueInMm = Math.Round(valueInMm, 0, MidpointRounding.AwayFromZero);
                        PropertyValue = (float)valueInMm;
                    }
                }

            }
            //GPFx258
        }

        private void slPropertyValue_Click(object sender, EventArgs e)
        {
            ShowKeyboard();
        }

        private void slPropertyValue_DoubleClick(object sender, EventArgs e)
        {
            ShowKeyboard();
        }

        private void slPropertyName_Click(object sender, EventArgs e)
        {
            ShowKeyboard();
        }

        private void slPropertyName_DoubleClick(object sender, EventArgs e)
        {
            ShowKeyboard();
        }

        private void SpreaderPropertyEditBox_Click(object sender, EventArgs e)
        {
            ShowKeyboard();
        }

        private void SpreaderPropertyEditBox_DoubleClick(object sender, EventArgs e)
        {
            ShowKeyboard();
        }
    }
}
