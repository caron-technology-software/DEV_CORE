using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Machine.UI.Controls
{
    public partial class MachineSlider : Form
    {
        public event EventHandler ValueChanged;

        private float GetValueFromSlider()
        {
            float d = MaxValue - MinValue;
            int dSlider = slider.MaxValue - slider.MinValue;

            float value = slider.Value * d / (float)dSlider + MinValue;

            //Console.WriteLine($"GetValueFromSlider: {value.ToString("0.000")} [{slider.Value}]");

            return value;
        }

        private void SetSliderFromValue(float value)
        {
            float d = MaxValue - MinValue;
            int dSlider = slider.MaxValue - slider.MinValue;

            int sliderValue = (int)((value - MinValue) / d * (float)dSlider);

            slider.Value = sliderValue;
        }

        public void OnValueChanged(ValueEventArgs<float> e)
        {
            ValueChanged?.Invoke(this, e);
        }

        public float NormalizedSliderValue => (float)slider.Value / (float)(slider.MaxValue - slider.MinValue);
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public float MinValue { get; private set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public float MaxValue { get; private set; }

        private float value = float.NaN;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public float Value
        {
            get
            {
                return this.value;
            }

            set
            {
                if (value < MinValue)
                {
                    value = MinValue;
                }
                else if (value > MaxValue)
                {
                    value = MaxValue;
                }

                if (this.value != value)
                {
                    this.value = value;
                    SetSliderFromValue(value);
                    labelValue.Text = $"{value.ToString("0.0")} %";
                }
            }
        }

        public MachineSlider(string propertyName, float value, float minValue, float maxValue)
        {
            InitializeComponent();

            slPropertyName.Text = propertyName;

            MinValue = minValue;
            MaxValue = maxValue;

            Value = value;

            slider.ValueChanged += Slider_ValueChanged;
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            if (!Visible)
            {
                Opacity = 1;

                for (int i = 0; i < 5; i++)
                {
                    Opacity -= 0.2;
                    Thread.Sleep(10);
                    Refresh();
                }
            }

            base.OnVisibleChanged(e);
        }

        protected override void OnShown(EventArgs e)
        {
            Opacity = 0;

            for (int i = 0; i < 5; i++)
            {
                Opacity += 0.2;
                Thread.Sleep(10);
                Refresh();
            }

            base.OnShown(e);
        }

        private void Slider_ValueChanged(object sender, EventArgs e)
        {
            value = GetValueFromSlider();
            labelValue.Text = $"{value.ToString("0.0")} %";
            OnValueChanged(new ValueEventArgs<float>(value));
        }

        private void SpreaderSlider_Load(object sender, EventArgs e)
        {
            //Per transparenza Form
            BackColor = Color.FromArgb(221, 221, 221);
            TransparencyKey = Color.FromArgb(221, 221, 221);
        }

        private void HandleExit()
        {
            Close();
        }

        private void SlPropertyName_DoubleClick(object sender, EventArgs e)
        {
            HandleExit();
        }

        private void slPropertyName_Click(object sender, EventArgs e)
        {
            HandleExit();
        }

        private void labelValue_Click(object sender, EventArgs e)
        {
            using (var keyb = new TouchNumericKeyboard(slPropertyName.Text, (float)Math.Round(value * 10.0) / 10.0f))
            {
                keyb.MinValue = MinValue;
                keyb.MaxValue = MaxValue;

                keyb.ShowDialog();
                value = (float)(keyb.Value);

                HandleExit();
            }
        }
    }
}
