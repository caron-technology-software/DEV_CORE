using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

//https://stackoverflow.com/questions/835100/winforms-suspendlayout-resumelayout-is-not-enough/835751

namespace Machine.UI.Controls
{
    public class MachineFlowLayoutPanel : FlowLayoutPanel
    {
        public Color BorderColor { get; set; } = Color.Transparent;

        public MachineFlowLayoutPanel() : base()
        {
            this.SetStyle(ControlStyles.UserPaint, true);

            this.DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.DrawRectangle(new Pen(new SolidBrush(BorderColor), 2), e.ClipRectangle);
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            base.VerticalScroll.Enabled = false;
            base.VerticalScroll.Visible = false;
        }
    }
}
