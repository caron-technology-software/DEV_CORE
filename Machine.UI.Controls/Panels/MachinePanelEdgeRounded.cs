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
    public partial class MachinePanelEdgeRounded : Panel
    {
        private int radius = 10;
        public int Radius
        {
            get
            {
                return radius;
            }

            set
            {
                radius = value;
                Refresh();
            }
        }

        private int lineWidth = 5;
        public int LineWidth
        {
            get
            {
                return lineWidth;
            }

            set
            {
                lineWidth = value;
                Refresh();
            }
        }

        public Color lineColor = Color.LightGray;
        public Color LineColor
        {
            get
            {
                return lineColor;
            }

            set
            {
                lineColor = value;
                Refresh();
            }
        }

        public MachinePanelEdgeRounded()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            RectDrawer.DrawEdgeRoundedRect(e, LineWidth, Radius, LineColor, Width, Height);
        }

    }
}
