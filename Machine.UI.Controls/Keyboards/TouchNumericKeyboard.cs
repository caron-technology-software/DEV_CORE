using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using FontAwesome.Sharp;

namespace Machine.UI.Controls
{
    public partial class TouchNumericKeyboard : Form
    {
        private bool firstTypingResetEnable = false;
        public float InitialValue { get; private set; }

        private string unitMeasure = String.Empty;
        public string UnitMeasure
        {
            get => unitMeasure;
            set
            {
                unitMeasure = $"[{value}]";
                labelUnitMeasure.Text = unitMeasure;
            }
        }

        private string variableRange;
        private string VariableRange
        {
            get
            {
                return variableRange;
            }
            set
            {
                variableRange = value;
                labelRange.Text = variableRange;
            }
        }

        private float? minValue = null;
        public float? MinValue
        {
            get
            {
                return minValue;
            }
            set
            {
                minValue = value;
                SetVariableRange();
            }
        }

        private float? maxValue = null;
        public float? MaxValue
        {
            get
            {
                return maxValue;
            }
            set
            {
                maxValue = value;
                SetVariableRange();
            }
        }

        private bool passwordEnabled = false;
        public bool PasswordEnabled
        {
            get
            {
                return passwordEnabled;
            }

            set
            {
                passwordEnabled = value;

                if (passwordEnabled)
                {
                    stringValue = "";
                    labelValue.Text = "";
                    KeyCommaEnabled = false;
                    KeyPlusMinusEnabled = false;
                }
            }
        }

        private bool keyCommaEnabled = true;
        public bool KeyCommaEnabled
        {
            get
            {
                return keyCommaEnabled;
            }

            set
            {

                if (keyCommaEnabled != value)
                {
                    keyCommaEnabled = value;

                    btnComma.Enabled = keyCommaEnabled;
                }
            }
        }

        private bool keyPlusMinusEnabled = true;
        public bool KeyPlusMinusEnabled
        {
            get
            {
                return keyPlusMinusEnabled;
            }

            set
            {

                if (keyPlusMinusEnabled != value)
                {
                    keyPlusMinusEnabled = value;

                    btnPlusMinus.Enabled = keyPlusMinusEnabled;
                }
            }
        }

        private float value = 0.0f;
        public float Value
        {
            get
            {
                return this.value;
            }
            private set
            {
                this.value = value;
            }
        }

        public string stringValue = String.Empty;
        public string StringValue
        {
            get
            {
                return stringValue;
            }
            set
            {
                #region Implementation
                if (PasswordEnabled)
                {
                    stringValue = value;
                }
                else
                {
                    if (firstTypingResetEnable && value[0] != '-')
                    {
                        value = value.Substring(value.Length - 1, 1);
                        firstTypingResetEnable = false;
                    }

                    if (value.Count(x => x.Equals(',')) <= 1)
                    {
                        stringValue = value;
                    }

                    if (stringValue.Length == 0)
                    {
                        stringValue = "0";
                    }
                    else if (stringValue.Length >= 2)
                    {
                        if (stringValue[0] == '0' && stringValue[1] == '0')
                        {
                            stringValue = stringValue.Substring(1);
                        }
                        else if (stringValue[0] == '0' && stringValue[1] != '0')
                        {
                            if (stringValue[1] != ',')
                            {
                                stringValue = stringValue.Substring(1);
                            }
                        }
                    }
                }
                #endregion

                RefreshLabel();
            }
        }

        //GPIx215
        private const int MillisecondsTimeout = 25; //[ms]
        private volatile bool taskRunning = false;
        private StreamHID StreamHID { get; set; }
        //GPFx215

        public TouchNumericKeyboard(string propertyName, float propertyValue, string notes = "", StreamHID streamHID = null)  //GPIx215
        {
            InitializeComponent();

            this.DoubleBuffered = true;

            labelPropertyName.Text = propertyName;
            InitialValue = propertyValue;

            StringValue = propertyValue.ToString("F99").TrimEnd('0').TrimEnd(',');
            //GPIx215
            StreamHID = streamHID;
            //GPFx215
            SetVariableRange();

            firstTypingResetEnable = true;

            btnCancelDigit.Image = IconChar.ArrowLeft.ToBitmap(Color.DarkGray, TouchKeyboard.IconSize);
            btnCancelDigit.Text = String.Empty;

            labelUnitMeasure.Text = UnitMeasure;
            labelNotes.Text = notes;

            labelValue.BorderStyle = BorderStyle.FixedSingle;

            DisableButtonsTabStop();

            TopLevel = true;
            TopMost = true;

            //GPIx215
            if (streamHID != null)
            {
                taskRunning = true;

                Task.Run(() =>
                {
                    StreamHID.Reset();

                    while (taskRunning)
                    {
                        if (string.IsNullOrEmpty(StreamHID.Text) == false)
                        {
                            StringValue = StreamHID.Text;
                            StreamHID.Reset();
                        }

                        Thread.Sleep(MillisecondsTimeout);
                    }
                });
            }
            //GPFx215
        }

        public void DisableButtonsTabStop()
        {
            btn0.TabStop = false;
            btn1.TabStop = false;
            btn2.TabStop = false;
            btn3.TabStop = false;
            btn4.TabStop = false;
            btn5.TabStop = false;
            btn6.TabStop = false;
            btn7.TabStop = false;
            btn8.TabStop = false;
            btn9.TabStop = false;
            btnCancel.TabStop = false;
            btnClear.TabStop = false;
            btnEnter.TabStop = false;
            btnPlusMinus.TabStop = false;
            btnCancelDigit.TabStop = false;
            btnComma.TabStop = false;
        }
        public void ResetOnlyLabel()
        {
            labelValue.Text = "";
        }
        private void RefreshLabel()
        {
            labelValue.ForeColor = Color.Black;

            if (float.TryParse(stringValue, out this.value))
            {
                if (MinValue != null)
                {
                    if (this.value < (float)MinValue)
                    {
                        this.value = (float)MinValue;
                        labelValue.ForeColor = Color.Red;
                    }
                }

                if (MaxValue != null)
                {
                    if (this.value > (float)MaxValue)
                    {
                        this.value = (float)MaxValue;
                        labelValue.ForeColor = Color.Red;
                    }
                }
            }

            labelValue.Text = PasswordEnabled ? string.Concat(Enumerable.Repeat("•", stringValue.Length)) : stringValue;
        }
        private void SetVariableRange()
        {
            if (MinValue != null && MaxValue != null)
            {
                if (MaxValue >= (int.MaxValue - 1))
                {
                    VariableRange = $"Min={((float)(MinValue)).ToString()}";
                }
                else if (MinValue <= (int.MinValue + 1))
                {
                    VariableRange = $"Max={((float)(MaxValue)).ToString()}";
                }
                else
                {
                    VariableRange = $"Min={((float)(MinValue)).ToString()} Max={((float)(MaxValue)).ToString()}";
                }
            }
            else if (MinValue != null)
            {
                VariableRange = $"Min={((float)(MinValue)).ToString()}";
            }
            else if (MaxValue != null)
            {
                VariableRange = $"Max={((float)(MaxValue)).ToString()}";
            }
            else
            {
                VariableRange = String.Empty;
            }
        }

        #region Numbers Click
        private void btn1_Click(object sender, EventArgs e)
        {
            StringValue += '1';
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            StringValue += '2';
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            StringValue += '3';
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            StringValue += '4';
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            StringValue += '5';
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            StringValue += '6';
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            StringValue += '7';
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            StringValue += '8';
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            StringValue += '9';
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            StringValue += '0';
        }
        #endregion

        #region Buttons Click
        private void btnComma_Click(object sender, EventArgs e)
        {
            StringValue += ',';
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            StringValue = PasswordEnabled ? "" : "0";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            //GPIx215
            taskRunning = false;
            //GPFx215
            Value = InitialValue;
            this.Close();
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            //GPIx215
            taskRunning = false;
            //GPFx215
            this.Close();
        }

        private void btnMinusPlus_Click(object sender, EventArgs e)
        {
            if (stringValue.ElementAt(0) == '-')
            {
                stringValue = stringValue.Substring(1);
            }
            else
            {
                stringValue = String.Concat("-", stringValue);
            }

            RefreshLabel();
        }

        private void btnCancelDigit_Click(object sender, EventArgs e)
        {
            if (StringValue.Length == 1)
            {
                StringValue = PasswordEnabled ? "" : "0";
            }
            else if (StringValue.Length > 1)
            {
                StringValue = StringValue.Substring(0, StringValue.Length - 1);
            }
        }
        #endregion
    }
}
