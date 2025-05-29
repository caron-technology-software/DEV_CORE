using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Machine.UI.Controls.Properties;

//NOTES: RGB code: 	R:26 G:37 B:43 26;37;43

namespace Machine.UI.Controls
{
    internal partial class MachineSliderInternal : UserControl
    {
        public event EventHandler ValueChanged;

        public void OnValueChanged(ValueEventArgs<int> e)
        {
            ValueChanged?.Invoke(this, e);
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int Delta { get; set; } = 15;

        public int MinValue => 0;
        public int MaxValue => 10000;

        public int value = 5000;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int Value
        {
            get
            {
                return value;
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
                    slider.Value = value;
                }
            }
        }

        public MachineSliderInternal()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
        }

        protected override void OnCreateControl()
        {
            this.BackColor = Color.Transparent;

            panelMinus.BackColor = Color.Transparent;
            panelPlus.BackColor = Color.Transparent;

            slider.BackColor = Color.Transparent;
            slider.BarInnerColor = Color.Transparent;
            slider.BarPenColorTop = Color.Transparent;
            slider.BarPenColorBottom = Color.Transparent;
            slider.ElapsedInnerColor = Color.Transparent;
            slider.ElapsedPenColorTop = Color.Transparent;
            slider.ElapsedPenColorBottom = Color.Transparent;

            slider.TickColor = Color.Transparent;
            slider.ForeColor = Color.Transparent;

            Bitmap bmp = Resources.cursor_filled;
            bmp.MakeTransparent(bmp.GetPixel(0, 0));
            slider.ThumbImage = bmp;
            slider.BackgroundImageLayout = ImageLayout.Stretch;

            slider.Minimum = MinValue;
            slider.Maximum = MinValue;

            slider.DrawFocusRectangle = false;
            slider.BorderRoundRectSize = new Size(0, 0);
            slider.ThumbRoundRectSize = new Size(0, 0);
            slider.ThumbInnerColor = Color.Transparent;
            slider.ThumbOuterColor = Color.Transparent;
            slider.ThumbPenColor = Color.Transparent;

            slider.DrawSemitransparentThumb = true;
            slider.Value = Value;

            base.OnCreateControl();
        }


        private void Slider_ValueChanged(object sender, EventArgs e)
        {
            value = (int)slider.Value;
            OnValueChanged(new ValueEventArgs<int>(value));
        }

        private void PanelPlus_Click(object sender, EventArgs e)
        {
            Value += Delta;
        }

        private void panelPlus_DoubleClick(object sender, EventArgs e)
        {
            Value += Delta;
        }

        private void PanelMinus_Click(object sender, EventArgs e)
        {
            Value -= Delta;
        }

        private void panelMinus_DoubleClick(object sender, EventArgs e)
        {
            Value -= Delta;
        }

    }
}
