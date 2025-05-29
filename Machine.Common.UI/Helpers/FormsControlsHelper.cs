using ProRob.Extensions.Hashing;
using ProRob.Extensions.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Machine.UI
{
    public partial class FormsControlsHelper
    {
        public static void FadeInEffect(IntPtr handle, TimeSpan interval)
        {
            Win32Api.AnimateWindow(handle, (int)interval.TotalMilliseconds, Win32Api.AW_ACTIVATE | Win32Api.AW_BLEND);
        }

        public static void FadeOutEffect(IntPtr handle, TimeSpan interval)
        {
            Win32Api.AnimateWindow(handle, (int)interval.TotalMilliseconds, Win32Api.AW_HIDE | Win32Api.AW_BLEND);
        }

        public static void SwapControlsLocations(System.Windows.Forms.Control control1, System.Windows.Forms.Control control2)
        {
            var loc1 = control1.Location.Clone();
            var loc2 = control2.Location.Clone();

            control1.Location = loc2;
            control2.Location = loc1;
        }

        public static void HideTableLayoutPanelHorizontalScrollBar(ref TableLayoutPanel pan)
        {
            if (!pan.HorizontalScroll.Visible)
            {
                return;
            }

            pan.Padding = new Padding(0, 0, 0, 0);

            while (!!pan.HorizontalScroll.Visible || pan.Padding.Right >= SystemInformation.VerticalScrollBarWidth * 2)
            {
                pan.Padding = new Padding(0, 0, pan.Padding.Right + 1, 0);
            }
        }

        public static void MirrorAllControls(
            Form form,
            IEnumerable<System.Windows.Forms.Control> controlsToExclude = null,
            IEnumerable<System.Windows.Forms.Control> controlsToForce = null)
        {
            var controls = new List<System.Windows.Forms.Control>();

            foreach (var obj in form.Controls)
            {
                controls.Add((obj as System.Windows.Forms.Control));
            }

            if (controlsToExclude != null)
            {
                controls = Enumerable.Except(controls, controlsToExclude).ToList();
            }

            if (controlsToForce != null)
            {
                controls.AddRange(controlsToForce);
            }

            foreach (var control in controls)
            {
                var location = control.Location;
                int x = location.X;
                int w = control.Width;
                location.X += form.Width - 2 * x - w;
                control.Location = location;
            }
        }

        ////public static void OpacityFadeWithForm(Form form)
        ////{
        ////    using var opacityForm = new Form();
        ////    opacityForm.Size = form.Size;
        ////    opacityForm.Location = form.Location;
        ////    opacityForm.Opacity = 1;
        ////    opacityForm.Show();

        ////    Task.Run(() =>
        ////    {
        ////        for (int i = 0; i < 10; i++)
        ////        {
        ////            opacityForm?.Invoke((MethodInvoker)delegate ()
        ////            {
        ////                opacityForm.Opacity -= 0.1f;
        ////                opacityForm.Refresh();
        ////                opacityForm.Update();
        ////            });

        ////            Thread.Sleep(10);
        ////        }

        ////        opacityForm.Close();
        ////    });
        ////}

        public static void OpacityFade(Form form, double start, double end, int n = 100, int delay = 1)
        {
            form?.Invoke((MethodInvoker)delegate ()
            {
                form.Opacity = start;
            });

            for (int i = 0; i < n; i++)
            {
                form?.Invoke((MethodInvoker)delegate ()
                {
                    form.Opacity = form.Opacity + (end - start) / n;
                    form.Refresh();
                });

                Thread.Sleep(delay);
            }
        }
    }
}
