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
    public partial class MachinePropertyBox : UserControl
    {
        private const int DeltaTextBox = 2;

        public bool LeftBorder { get; set; } = false;
        public bool RighBorder { get; set; } = false;
        public bool TopBorder { get; set; } = false;
        public bool BottomBorder { get; set; } = false;

        private string propertyName;
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
                    Refresh();
                }
            }
        }

        private string propertyValue;
        public string PropertyValue
        {
            get
            {
                return propertyValue;
            }
            set
            {
                if (propertyValue != value)
                {
                    propertyValue = value;
                    slPropertyValue.Text = propertyValue;

                    //Refresh();
                }
            }
        }

        public MachinePropertyBox()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            DrawBorders(e);
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            Refresh();
        }

        public override void Refresh()
        {
            base.Refresh();

            slPropertyName.Refresh();
            slPropertyName.Location = new Point(DeltaTextBox, slPropertyName.Location.Y);
            slPropertyName.Width = base.Width - DeltaTextBox * 2;

            slPropertyValue.Refresh();
            slPropertyValue.Width = base.Width - DeltaTextBox * 2;
            slPropertyValue.Location = new Point(DeltaTextBox, slPropertyValue.Location.Y);
        }

        private void SpreaderPropertyBox_Resize(object sender, EventArgs e)
        {
            Refresh();
        }

        public void DrawBorders(PaintEventArgs e)
        {
            if (LeftBorder || RighBorder || TopBorder || LeftBorder)
            {
                Pen pen = new Pen(Color.FromArgb(200, 0, 0, 0));

                if (LeftBorder)
                {
                    e.Graphics.DrawLine(pen, 0, 0, 0, this.Height - 1);
                }

                if (RighBorder)
                {
                    e.Graphics.DrawLine(pen, this.Width - 1, 0, this.Width - 1, this.Height - 1);
                }

                if (TopBorder)
                {
                    e.Graphics.DrawLine(pen, 0, 0, this.Width - 1, 0);
                }

                if (BottomBorder)
                {
                    e.Graphics.DrawLine(pen, 0, this.Height - 1, this.Width - 1, this.Height - 1);
                }
            }
        }

        private void SpreaderPropertyBox_Load(object sender, EventArgs e)
        {
            Refresh();
        }

        private void MachinePropertyBox_SizeChanged(object sender, EventArgs e)
        {

        }
    }
}
