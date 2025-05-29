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
    public partial class MachinePanelButtonSlider : UserControl
    {
        public event EventHandler ValueChanged;
        public event EventHandler SliderExit;

        private void OnValueChanged(ValueEventArgs<float> e)
        {
            ValueChanged?.Invoke(this, e);
        }

        private void OnExit(EventArgs e)
        {
            SliderExit?.Invoke(this, e);
        }

        private static void UpdateUIEvent(object sender, EventArgs e, MachineLabel label, string text)
        {
            label.Text = text;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string PropertyName { get => mbSlider.PropertyName; set => mbSlider.PropertyName = value; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public float MinValue { get => mbSlider.MinValue; set => mbSlider.MinValue = value; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public float MaxValue { get => mbSlider.MaxValue; set => mbSlider.MaxValue = value; }

        private bool valueChangedEventEnabled = false;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool ValueChangedEventEnabled
        {
            get
            {
                return valueChangedEventEnabled;
            }
            set
            {
                valueChangedEventEnabled = value;
                mbSlider.ValueChangedEventEnabled = valueChangedEventEnabled;
            }
        }

        public void SetValueWithoutEvent(double value)
        {
            SetValueWithoutEvent((float)value);
        }

        public void SetValueWithoutEvent(float value)
        {
            var precValue = ValueChangedEventEnabled;

            ValueChangedEventEnabled = false;
            Value = value;
            ValueChangedEventEnabled = precValue;
        }

        private float sliderValue = float.NaN;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public float Value
        {
            get
            {
                return mbSlider.Value;
            }
            set
            {
                if (this.sliderValue != value)
                {
                    this.sliderValue = value;

                    if (this.sliderValue > MaxValue)
                    {
                        this.sliderValue = MaxValue;
                    }
                    else if (this.sliderValue < MinValue)
                    {
                        this.sliderValue = MinValue;
                    }

                    if (ValueChangedEventEnabled)
                    {
                        mbSlider.Value = this.sliderValue;
                    }
                    else
                    {
                        mbSlider.SetValueWithoutEvent(this.sliderValue);
                    }

                    this?.Invoke((MethodInvoker)delegate ()
                    {
                        mlSliderValue.Text = $"{this.sliderValue.ToString("0.0")} %";
                    });
                }
            }
        }

        public MachinePanelButtonSlider()
        {
            InitializeComponent();

            mbSlider.ValueChanged += (sender, e) => UpdateUIEvent(sender, e, mlSliderValue, $"{mbSlider.Value.ToString("0.0")}%");
            mbSlider.ValueChanged += SbSlider_ValueChanged;
            mbSlider.Exit += SbSlider_Exit;

            sliderValue = mbSlider.Value;

            mbSlider.FlatAppearance.BorderColor = BackColor;
        }

        private void SbSlider_Exit(object sender, EventArgs e)
        {
            OnExit(new EventArgs());
        }

        private void SbSlider_ValueChanged(object sender, EventArgs e)
        {
            Value = ((ValueEventArgs<float>)e).Value;
            OnValueChanged(new ValueEventArgs<float>(Value));
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            mlSliderValue.Text = "0.0%";

            Refresh();
        }

        public void LoadBackgroundImages(Image disabledButtonImage, List<Image> framesImages)
        {
            mbSlider.LoadBackgroundImages(disabledButtonImage, framesImages);
        }

        private void mlSliderValue_Click(object sender, EventArgs e)
        {
            if (Enabled)
            {
                mbSlider.OpenSlider();
            }
        }
    }
}
