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
    public partial class MachinePlusMinusPropertyBox : UserControl
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool InternalCounterLogicEnabled { get; set; } = true;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int DeltaValue { get; set; } = 0;

        public event EventHandler<EventArgs> PlusButtonClicked;
        public event EventHandler<EventArgs> MinusButtonClicked;

        private int maxValue = 0;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int MaxValue
        {
            get
            {
                return maxValue;
            }

            set
            {
                if (value != maxValue)
                {
                    maxValue = value;
                    UpdateLabel();
                }
            }
        }


        private int minValue = 0;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int MinValue
        {
            get
            {
                return minValue;
            }

            set
            {
                if (value != minValue)
                {
                    minValue = value;
                    UpdateLabel();
                }
            }
        }

        private void OnPlusButtonClicked(EventArgs e)
        {
            PlusButtonClicked?.Invoke(this, e);
        }

        private void OnMinusButtonClicked(EventArgs e)
        {
            MinusButtonClicked?.Invoke(this, e);
        }

        private string propertyName = string.Empty;
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

        private void UpdateLabel()
        {
            slPropertyValue.Text = $"{Value + DeltaValue}/{MaxValue}";
        }

        private int value = 0;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int Value
        {
            get
            {
                return value;
            }

            set
            {
                if (this.value != value)
                {
                    this.value = value;
                    UpdateLabel();
                }
            }
        }

        public MachinePlusMinusPropertyBox()
        {
            InitializeComponent();

            this.DoubleBuffered = true;

            PropertyName = "Property Name";
            Value = 0;
        }

        private void pictureBoxMinus_Click(object sender, EventArgs e)
        {
            if (InternalCounterLogicEnabled)
            {
                if (value - 1 >= MinValue)
                {
                    Value--;
                }
            }

            OnMinusButtonClicked(new EventArgs());
        }

        private void pictureBoxPlus_Click(object sender, EventArgs e)
        {
            if (InternalCounterLogicEnabled)
            {
                if ((value + 1) <= MaxValue)
                {
                    Value++;
                }
            }

            OnPlusButtonClicked(new EventArgs());
        }
    }
}
