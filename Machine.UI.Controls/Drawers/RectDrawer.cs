#define ANTIALIASING

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Machine.UI.Controls
{
    public partial class RectDrawer
    {
        public static void DrawRoundedRect(PaintEventArgs e, int lineWidth, Color lineColor, int width, int height)
        {
#if ANTIALIASING
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
#endif
            int lw = lineWidth;
            int r = height / 2 - lw;

            var pen = new Pen(lineColor, lw);

            //Up
            var p1 = new PointF(0 + r, 0 + lw / 2);
            var p2 = new PointF(width - r, 0 + lw / 2);
            e.Graphics.DrawLine(pen, p1, p2);

            //Down
            var p3 = new PointF(0 + r, height - lw / 2 - 1);
            var p4 = new PointF(width - r, height - lw / 2 - 1);
            e.Graphics.DrawLine(pen, p3, p4);

            //Left
            e.Graphics.DrawArc(pen, new Rectangle(0 + lw / 2, 0 + lw / 2, 2 * r, height - lw - 1), 90, 180);

            //Right
            e.Graphics.DrawArc(pen, new Rectangle((int)p2.X - r - lw / 2, lw / 2, 2 * r - 1, height - lw - 1), -90, 180);
        }

        public static void DrawEdgeRoundedRect(PaintEventArgs e, int lineWidth, int radius, Color lineColor, int width, int height)
        {
#if ANTIALIASING
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
#endif
            int lw = lineWidth;
            int r = radius;

            var pen = new Pen(lineColor, lw);

            //Up
            var p1 = new PointF(0 + r, 0 + lw / 2);
            var p2 = new PointF(width - r, 0 + lw / 2);
            e.Graphics.DrawLine(pen, p1, p2);

            //Down
            var p3 = new PointF(0 + r, height - lw / 2 - 1);
            var p4 = new PointF(width - r, height - lw / 2 - 1);
            e.Graphics.DrawLine(pen, p3, p4);

            //Left
            var p5 = new PointF(0 + lw / 2, r);
            var p6 = new PointF(0 + lw / 2, height - r);
            e.Graphics.DrawLine(pen, p5, p6);

            //Right
            var p7 = new PointF(width - lw / 2 - 1, r);
            var p8 = new PointF(width - lw / 2 - 1, height - r);
            e.Graphics.DrawLine(pen, p7, p8);

            //Up-Left
            e.Graphics.DrawArc(pen, 0 + lw / 2, 0 + lw / 2, 2 * r, 2 * r, 180, 90);

            //Down-Left
            e.Graphics.DrawArc(pen, 0 + lw / 2, height - 2 * r - lw / 2 - 1, 2 * r, 2 * r, 90, 90);

            //Up-Right
            e.Graphics.DrawArc(pen, width - 2 * r - lw / 2 - 1, 0 + lw / 2, 2 * r, 2 * r, -90, 90);

            //Down-Left
            e.Graphics.DrawArc(pen, width - 2 * r - lw / 2 - 1, height - 2 * r - lw / 2 - 1, 2 * r, 2 * r, 0, 90);
        }
    }
}
