using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Machine.UI.Controls
{
    public partial class MachinePropertyNumericEditBoxHorizontal : UserControl
    {
        //GPIx258
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] public bool IsImperialYard { get; set; } = false;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] public bool NeedYard { get; set; } = false;
        //GPFx258
        private static readonly object locker = new object();
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] public bool DisableEditVariable { get; set; } = false;

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
                propertyName = value;
                slPropertyName.Text = propertyName;
            }
        }

        private float propertyValue = 0.0f;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public float PropertyValue
        {
            get
            {
                return propertyValue;
            }

            set
            {
                lock (locker)
                {
                    if (propertyValue != value || DoNotExecuteCheckIfValueIsChanged)
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
                        str01 = str01.Replace(".", "").Replace(",", "");
                        slPropertyValueYard.Text = str01;
                        //slPropertyValueYard.Text = $"{propertyValueInInch:0.000} inch";
                        slPropertyValueYard.Refresh();
                        //GPFx258

                        if (ValueChangedEventEnabled)
                        {
                            OnValueChanged(new ValueEventArgs<float>(value));
                        }
                    }
                }
            }
        }

        public event EventHandler ValueChanged;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] public bool ValueChangedEventEnabled { get; set; } = false;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] public bool DoNotExecuteCheckIfValueIsChanged { get; set; } = false;

        private void OnValueChanged(EventArgs e)
        {
            ValueChanged?.Invoke(this, e);
        }

        public void SetValueWithoutEvent(float value)
        {
            lock (locker)
            {
                var precValue = ValueChangedEventEnabled;

                ValueChangedEventEnabled = false;

                PropertyValue = value;

                ValueChangedEventEnabled = precValue;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public float MinValue { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public float MaxValue { get; set; }

        public MachinePropertyNumericEditBoxHorizontal()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
        }

        public MachinePropertyNumericEditBoxHorizontal(string propertyName)
        {
            InitializeComponent();

            PropertyName = propertyName;
        }

        private void SpreaderEditBoxHorizontal_Load(object sender, EventArgs e)
        {
            slPropertyName.BorderStyle = BorderStyle.None;
            slPropertyValue.BorderStyle = BorderStyle.FixedSingle;
            //GPIx258
            slPropertyValueYard.BorderStyle = BorderStyle.FixedSingle;
            //GPFx258
            TabStop = false;

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
            str01 = str01.Replace(".", "").Replace(",", "");
            slPropertyValueYard.Text = str01;
            //slPropertyValueYard.Text = $"{propertyValueInInch:0.000} inch";
            slPropertyValueYard.Refresh();
            //GPFx258
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            BorderStyle = BorderStyle.FixedSingle;
            BackColor = Color.FromArgb(200, 200, 200);

            PropertyValue = 0.0f;

            Refresh();
        }

        /*private void TextBoxGotFocus(object sender, EventArgs args)
        {
            ((TextBox)sender).Parent.Focus();
        }*/

        private void HandleKeyboard()
        {
            if (DisableEditVariable)
            {
                return;
            }

            //GPIx258
            if (!IsImperialYard)
            {
                using (var keyb = new TouchNumericKeyboard(PropertyName, PropertyValue))
                {
                    //////////keyb.KeyCommaEnabled = false;
                    //////////keyb.KeyPlusMinusEnabled = false;

                    keyb.UnitMeasure = "mm";

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
                decimal valueInYard = Math.Truncate((decimal)((propertyValue / 25.4f) / 36f));
                decimal valueInInchRemaining = (decimal)(propertyValue / 25.4f) - (Math.Truncate((decimal)((propertyValue / 25.4f) / 36f)) * 36m);
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

                        valueInMm = Math.Round(valueInMm, 0, MidpointRounding.AwayFromZero);
                        PropertyValue = (float)valueInMm;
                    }
                }

            }
            //GPFx258

        }

        private void slPropertyName_DoubleClick(object sender, EventArgs e)
        {
            HandleKeyboard();
        }

        private void SlPropertyName_Click(object sender, EventArgs e)
        {
            HandleKeyboard();
        }

        private void slPropertyValue_DoubleClick(object sender, EventArgs e)
        {
            HandleKeyboard();
        }

        private void SlPropertyValue_Click(object sender, EventArgs e)
        {
            HandleKeyboard();
        }

        private void slPropertyName_Enter(object sender, EventArgs e)
        {
            BackColor = Color.FromArgb(160, 160, 160);
        }

        private void slPropertyName_Leave(object sender, EventArgs e)
        {
            BackColor = Color.LightGray;
        }

        private void slPropertyValue_Enter(object sender, EventArgs e)
        {
            BackColor = Color.FromArgb(160, 160, 160);
        }

        private void slPropertyValue_Leave(object sender, EventArgs e)
        {
            BackColor = Color.LightGray;
        }
    }
}
