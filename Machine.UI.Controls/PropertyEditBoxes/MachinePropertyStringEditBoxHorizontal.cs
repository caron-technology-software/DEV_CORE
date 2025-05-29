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
    public partial class MachinePropertyStringEditBoxHorizontal : UserControl
    {
        private static readonly object locker = new object();

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

        private string propertyValue = "";
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string PropertyValue
        {
            get
            {
                return propertyValue;
            }

            set
            {
                if (value is null)
                {
                    return;
                }

                if (propertyValue != value)
                {
                    propertyValue = value;
                    slPropertyValue.Text = propertyValue?.ToString();
                    slPropertyValue.Refresh();

                    if (ValueChangedEventEnabled)
                    {
                        OnValueChanged(new ValueEventArgs<string>(value));
                    }
                }
            }
        }

        public event EventHandler ValueChanged;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool ValueChangedEventEnabled { get; set; } = false;

        private void OnValueChanged(EventArgs e)
        {
            ValueChanged?.Invoke(this, e);
        }

        public void SetValueWithoutEvent(string value)
        {
            lock (locker)
            {
                var precValue = ValueChangedEventEnabled;

                ValueChangedEventEnabled = false;

                PropertyValue = value;

                ValueChangedEventEnabled = precValue;
            }
        }

        private void LoadKeyboard()
        {
            using (var keyb = new TouchAlphaNumericKeyboard(PropertyName, PropertyValue))
            {
                keyb.ShowDialog();
                PropertyValue = keyb.StringValue;
            }
        }

        public MachinePropertyStringEditBoxHorizontal()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
        }

        public MachinePropertyStringEditBoxHorizontal(string propertyName)
        {
            InitializeComponent();

            PropertyName = propertyName;
        }

        private void SpreaderEditBoxHorizontal_Load(object sender, EventArgs e)
        {
            slPropertyName.BorderStyle = BorderStyle.None;
            slPropertyValue.BorderStyle = BorderStyle.FixedSingle;
            TabStop = false;
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            BorderStyle = BorderStyle.FixedSingle;
            BackColor = Color.FromArgb(200, 200, 200);

            PropertyValue = "";

            Refresh();
        }

        /*private void TextBoxGotFocus(object sender, EventArgs args)
        {
            ((TextBox)sender).Parent.Focus();
        }*/

        private void slPropertyName_DoubleClick(object sender, EventArgs e)
        {
            LoadKeyboard();
        }

        private void slPropertyValue_DoubleClick(object sender, EventArgs e)
        {
            LoadKeyboard();
        }

        private void SlPropertyValue_Click(object sender, EventArgs e)
        {
            LoadKeyboard();
        }
        private void SlPropertyValue_DoubleClick(object sender, EventArgs e)
        {
            LoadKeyboard();
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
