using System;
using System.Drawing;
using System.Windows.Forms;

namespace Machine.UI.Controls
{
    public partial class MachineButtonLabel : UserControl
    {
        private volatile bool disableOnActiveChange = false;
        public event EventHandler<EventArgs> ActiveChanged;

        public int ButtonSize
        {
            get
            {
                return machineButton.ButtonSize;
            }

            set
            {
                machineButton.ButtonSize = value;
                Refresh();

                labelButton.Height = machineButton.Size.Height;
                Refresh();
            }
        }

        public Bitmap ActiveBackgroundImage
        {
            get
            {
                return machineButton.ActiveBackgroundImage;
            }

            set
            {
                if (machineButton.ActiveBackgroundImage != value)
                {
                    machineButton.ActiveBackgroundImage = value;
                }
            }
        }

        public Bitmap InactiveBackgroundImage
        {
            get
            {
                return machineButton.InactiveBackgroundImage;
            }

            set
            {
                if (machineButton.InactiveBackgroundImage != value)
                {
                    machineButton.InactiveBackgroundImage = value;
                }
            }
        }
        private string propertyName = "Property Name";
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
                    labelButton.Text = propertyName;
                }
            }
        }

        public void SetActiveWithoutEvent(bool active)
        {
            if (Active != active)
            {
                disableOnActiveChange = true;
                Active = active;
                disableOnActiveChange = false;
            }
        }

        public DateTime LastActivation => machineButton.LastActivation;
        public DateTime LastDisactivation => machineButton.LastDisactivation;

        public bool Active
        {
            get
            {
                return machineButton.Active;
            }
            set
            {
                if (value != Active)
                {
                    machineButton.Active = value;

                    if (disableOnActiveChange == false)
                    {
                        OnActiveChanged(new EventArgs());
                    }
                }

            }
        }

        public MachineButtonLabel()
        {
            InitializeComponent();

            machineButton.ActiveChanged += MachineButtonActiveChanged;
            machineButton.FlatAppearance.BorderColor = Constants.Colors.DefaultBackColor;

            this.DoubleBuffered = true;
        }
        private void OnActiveChanged(EventArgs e)
        {
            ActiveChanged?.Invoke(this, e);
        }

        private void MachineButtonActiveChanged(object sender, EventArgs e)
        {
            this.OnActiveChanged(new EventArgs());
        }

        private void SpreaderButton_Click(object sender, EventArgs e)
        {
            this.OnClick(e);
        }

        private void SpreaderButton_MouseClick(object sender, MouseEventArgs e)
        {
            this.OnMouseClick(e);
        }

        private void SpreaderButton_MouseEnter(object sender, EventArgs e)
        {
            this.OnMouseEnter(e);
        }

        private void SpreaderButton_MouseDown(object sender, MouseEventArgs e)
        {
            this.OnMouseDown(e);
        }

        private void SpreaderButton_MouseLeave(object sender, EventArgs e)
        {
            this.OnMouseLeave(e);
        }

        private void SpreaderButton_MouseUp(object sender, MouseEventArgs e)
        {
            this.OnMouseUp(e);
        }
    }
}
