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
    public partial class MachinePropertyBoxHorizontal : UserControl
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool LeftBorder { get; set; } = false;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool RighBorder { get; set; } = false;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool TopBorder { get; set; } = false;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool BottomBorder { get; set; } = false;

        private string propertyName;
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

                    //Refresh();
                }
            }
        }

        private string propertyValue;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        public MachinePropertyBoxHorizontal()
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

            slPropertyName.Text = "Property Name";
            slPropertyValue.Text = "Value";

            Refresh();
        }

        public override void Refresh()
        {
            base.Refresh();
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
    }
}
