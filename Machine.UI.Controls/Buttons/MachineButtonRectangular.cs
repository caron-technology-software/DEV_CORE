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
    [DefaultEvent("Click")]
    public partial class MachineButtonRectangular : UserControl
    {
        public MachineButtonRectangular()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                machineLabel.Text = value;
                Refresh();
            }

        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Font Font
        {
            get
            {
                return machineLabel.Font;
            }
            set
            {
                machineLabel.Font = value;
                Refresh();
            }

        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            machineLabel.Size = this.Size;
            machinePanel.Size = this.Size;
        }

        private void machineLabel_Click(object sender, EventArgs e)
        {
            this.OnClick(e);
        }

        private void machineLabel_DoubleClick(object sender, EventArgs e)
        {
            this.OnDoubleClick(e);
        }

        private void machineLabel_MouseDown(object sender, MouseEventArgs e)
        {
            this.OnMouseDown(e);
        }

        private void machineLabel_MouseUp(object sender, MouseEventArgs e)
        {
            this.OnMouseUp(e);
        }
    }
}
