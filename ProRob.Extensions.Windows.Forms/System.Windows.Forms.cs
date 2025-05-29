using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace ProRob.Extensions.Forms
{
    public static class FormsExtensions
    {
        public static void OpacityFade(this Form form, double start, double end, int n = 100, int delay = 1)
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
