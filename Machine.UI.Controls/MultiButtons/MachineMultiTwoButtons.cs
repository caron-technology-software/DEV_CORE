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
    public partial class MachineMultiTwoButtons : UserControl
    {
        public const int ButtonSize = 102;
        public const int NumberOfButtons = 2;
        public const int LineWidht = 4;
        public const int Radius = 10;

        public bool ValueChangedEventEnabled { get; set; } = false;

        public void SetValueWithoutEvent(int value)
        {
            var precValueChangedEventEnabled = ValueChangedEventEnabled;

            ValueChangedEventEnabled = false;
            Value = value;

            ValueChangedEventEnabled = precValueChangedEventEnabled;
        }

        public event EventHandler<MultiButtonsEventArgs> ValueChanged;

        private void OnValueChanged(MultiButtonsEventArgs e)
        {
            if (ValueChangedEventEnabled)
            {
                ValueChanged?.Invoke(this, e);
            }
        }

        private int value = -1;
        public int Value
        {
            get
            {
                return value;
            }

            set
            {
                if (value >= 0 && value <= (NumberOfButtons - 1))
                {
                    SetButtonsToInactiveState();

                    buttons[value].Active = true;

                    foreach (var b in buttons)
                    {
                        b.Refresh();
                    }

                    this.value = value;

                    if (ValueChangedEventEnabled)
                    {
                        OnValueChanged(new MultiButtonsEventArgs(Value));
                    }
                }
            }
        }

        private readonly List<MachineButton> buttons = new List<MachineButton>();

        public MachineMultiTwoButtons()
        {
            InitializeComponent();

            buttons.Add(b1);
            buttons.Add(b2);

            this.DoubleBuffered = true;

            b1.FlatAppearance.BorderColor = Constants.Colors.DefaultBackColor;
            b2.FlatAppearance.BorderColor = Constants.Colors.DefaultBackColor;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            RectDrawer.DrawEdgeRoundedRect(e, LineWidht, Radius, Color.LightGray, Width, Height);
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            foreach (var b in buttons)
            {
                b.Size = new Size(ButtonSize, ButtonSize);
            }

            Value = 0;

            Refresh();
        }

        private void SetButtonsToInactiveState()
        {
            foreach (var b in buttons)
            {
                if (b != null)
                {
                    if (b.Active)
                    {
                        b.Active = false;
                    }
                }
            }
        }

        private Bitmap GetBitmapFromImage(Image image)
        {
            Bitmap bmp = new Bitmap(image, new Size(ButtonSize, ButtonSize));
            bmp.MakeTransparent(bmp.GetPixel(0, 0));
            return bmp;
        }

        public void SetImages(List<Tuple<Image, Image>> images)
        {
            for (int i = 0; i < NumberOfButtons; i++)
            {
                buttons[i].InactiveBackgroundImage = GetBitmapFromImage(images[i].Item1);
                buttons[i].ActiveBackgroundImage = GetBitmapFromImage(images[i].Item2);
            }
        }

        #region Button's click
        private void b1_MouseUp(object sender, MouseEventArgs e)
        {
            Value = 0;
        }

        private void b2_MouseUp(object sender, MouseEventArgs e)
        {
            Value = 1;
        }

        #endregion
    }
}
