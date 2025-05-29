using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

using Machine.UI.Controls.Properties;

namespace Machine.UI.Controls
{
    public partial class MachineButtonSlider : Button
    {
        private static readonly object locker = new object();

        const int ButtonSize = 125;

        public event EventHandler ValueChanged;
        public event EventHandler Exit;

        private void OnValueChanged(EventArgs e)
        {
            ValueChanged?.Invoke(this, e);
        }

        private void OnExit(EventArgs e)
        {
            Exit?.Invoke(this, e);
        }

        public string PropertyName { get; set; } = String.Empty;

        public float MinValue { get; set; } = 0;
        public float MaxValue { get; set; } = 100;

        public bool ValueChangedEventEnabled { get; set; } = false;

        public void SetValueWithoutEvent(float value)
        {
            lock (locker)
            {
                var precValue = ValueChangedEventEnabled;

                ValueChangedEventEnabled = false;

                Value = value;

                ValueChangedEventEnabled = precValue;
            }
        }

        private float value = 0;
        public float Value
        {
            get
            {
                return value;
            }

            set
            {
                lock (locker)
                {
                    if (this.value != value)
                    {
                        this.value = value;

                        SetImage(framesImages[GetIndexImageFromNormalizedValue()]);

                        if (ValueChangedEventEnabled)
                        {
                            OnValueChanged(new ValueEventArgs<float>(value));
                        }
                    }
                }
            }
        }


        public bool IsInitialized { get; private set; } = false;
        public int NumberOfFrames { get; private set; } = 0;

        private Image disabledButtonImage;
        private List<Image> framesImages;

        // Prevent Text from being set on the button (since it will be an icon)
        [Browsable(false)]
        public override string Text { get { return ""; } set { base.Text = ""; } }

        [Browsable(false)]
        public override ContentAlignment TextAlign { get { return base.TextAlign; } set { base.TextAlign = value; } }

        private int GetIndexImageFromNormalizedValue()
        {
            float normalizedValue = (Value - MinValue) / (MaxValue - MinValue);

            var index = (int)(normalizedValue * (float)NumberOfFrames);

            if (index >= NumberOfFrames)
            {
                index = NumberOfFrames - 1;
            }
            else if (index < 0)
            {
                index = 0;
            }

            return index;
        }

        public void LoadBackgroundImages(Image disabledButtonImage, List<Image> framesImages)
        {
            this.disabledButtonImage = disabledButtonImage;
            this.framesImages = framesImages;

            NumberOfFrames = framesImages.Count;

            SetImage(disabledButtonImage);

            IsInitialized = true;
        }

        private void SetImage(Image image)
        {
            Bitmap bmp = new Bitmap(image, new Size(ButtonSize, ButtonSize));
            bmp.MakeTransparent(bmp.GetPixel(0, 0));
            BackgroundImage = bmp;
        }

        public MachineButtonSlider()
        {
            InitializeComponent();
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            base.Size = new Size(ButtonSize, ButtonSize);

            ForeColor = Color.Transparent;
            BackColor = Color.Transparent;

            FlatStyle = FlatStyle.Flat;

            FlatAppearance.BorderSize = 0;
            FlatAppearance.MouseDownBackColor = Color.Transparent;
            FlatAppearance.MouseOverBackColor = Color.Transparent;

            BackgroundImageLayout = ImageLayout.Zoom;

            SetImage(Resources.verified_green);

            Refresh();
        }

        public void OpenSlider()
        {
            var slider = new MachineSlider(PropertyName, Value, MinValue, MaxValue);

            slider.ValueChanged += Slider_ValueChanged;
            slider.Value = Value;
            slider.Opacity = 0;
            slider.ShowDialog();

            Value = slider.Value;
            slider.Close();

            if (framesImages is null)
            {
                MessageBox.Show("Frames images not setted", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            OnExit(new EventArgs());
        }

        protected override void OnClick(EventArgs e)
        {
            OpenSlider();
        }

        private void Slider_ValueChanged(object sender, EventArgs e)
        {
            Value = ((ValueEventArgs<float>)e).Value;
        }
    }
}