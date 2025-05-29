using System;
using System.Drawing;
using System.Windows.Forms;

namespace Machine.UI.Controls
{
    public partial class MachineFloatingEditableItem : MachineEditableItem
    {
        //GPIx258
        public bool IsImperialYard { get; set; } = false;
        public bool NeedYard { get; set; } = false;
        //GPFx258
        public float? MinValue { get; set; }
        public float? MaxValue { get; set; }

        private float propertyValue = 0;
        public float PropertyValue
        {
            get
            {
                return propertyValue;
            }

            set
            {
                if ((MaxValue != null) && (MinValue != null))
                {
                    if (value > MaxValue)
                    {
                        propertyValue = (float)MaxValue;
                    }
                    else if (value < MinValue)
                    {
                        propertyValue = (float)MinValue;
                    }
                    else
                    {
                        propertyValue = value;
                    }
                }
                else
                {
                    propertyValue = value;
                }

                slPropertyValue.Text = PropertyValue.ToString();

                //GPIx258 -> da convertire stringa yard
                decimal propertyValueInInch = (decimal)(propertyValue / 25.4f);
                decimal propertyValueInInYard = Math.Truncate((decimal)((propertyValue / 25.4f) / 36f));
                decimal propertyValueInInchRemaining = (decimal)(propertyValue / 25.4f) - (Math.Truncate((decimal)((propertyValue / 25.4f) / 36f)) * 36m);
                decimal propertyValueInInchRemainingIntPart = Math.Truncate(propertyValueInInchRemaining);
                decimal propertyValueInInchRemainingDecimalPart = propertyValueInInchRemaining - Math.Truncate(propertyValueInInchRemaining);

                //propertyValueInInch = Math.Round(propertyValueInInch, 5, MidpointRounding.AwayFromZero);
                string str01 = $"{propertyValueInInYard:0}yd{propertyValueInInchRemainingIntPart:0}\"{propertyValueInInchRemainingDecimalPart:.000}";
                str01 = str01.Replace(".", "").Replace(",", "");
                slPropertyValueYard.Text = str01;
                //slPropertyValueYard.Text = $"{propertyValueInInch:0.000} inch";
                //GPFx258
            }
        }

        public MachineFloatingEditableItem()
        {
            InitializeComponent();

            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
        }

        public override object GetValue()
        {
            return PropertyValue;
        }

        protected override void UpdateControl()
        {
            slPropertyName.Text = PropertyName;
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            slPropertyName.TextAlign = ContentAlignment.MiddleLeft;
            slPropertyName.BorderStyle = BorderStyle.None;

            slPropertyValue.TextAlign = ContentAlignment.MiddleCenter;
            slPropertyValue.BorderStyle = BorderStyle.FixedSingle;

            //GPIx258
            slPropertyValueYard.TextAlign = ContentAlignment.MiddleCenter;
            slPropertyValueYard.BorderStyle = BorderStyle.FixedSingle;
            //GPFx258
        }

        private void HandleChangePropertyValue()
        {
            if (IsPropertyEditable == false)
            {
                MachineMessageBox.Show("", MessageBoxText);
                return;
            }

            //GPIx258
            if (!IsImperialYard)
            {
                using (var keyb = new TouchNumericKeyboard(PropertyName, PropertyValue))
                {
                    keyb.MinValue = MinValue;
                    keyb.MaxValue = MaxValue;

                    keyb.ShowDialog();
                    PropertyValue = (float)(keyb.Value);

                    OnPropertyChange(new EventArgs());
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
                //valueInInchRemaining = Math.Round(valueInInchRemaining, 5, MidpointRounding.AwayFromZero);
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

                        valueInMm = Math.Round(valueInMm, 5, MidpointRounding.AwayFromZero);
                        PropertyValue = (float)valueInMm;

                        OnPropertyChange(new EventArgs());
                    }
                }

            }
            //GPFx258
        }

        private void slPropertyValue_DoubleClick(object sender, EventArgs e)
        {
            HandleChangePropertyValue();
        }

        private void slPropertyName_Enter(object sender, EventArgs e)
        {
            BackColor = ColorBackgroundSelectedItem;
        }

        private void slPropertyName_Leave(object sender, EventArgs e)
        {
            BackColor = ColorBackground;
        }

        private void slPropertyValue_Enter(object sender, EventArgs e)
        {
            BackColor = ColorBackgroundSelectedItem;
        }

        private void slPropertyValue_Leave(object sender, EventArgs e)
        {
            BackColor = ColorBackground;
        }

        private void slPropertyName_DoubleClick(object sender, EventArgs e)
        {
            HandleChangePropertyValue();
        }

        private void slPropertyValue_Click(object sender, EventArgs e)
        {
            HandleChangePropertyValue();
        }

        private void SlPropertyName_Click(object sender, EventArgs e)
        {
            HandleChangePropertyValue();
        }
    }
}
