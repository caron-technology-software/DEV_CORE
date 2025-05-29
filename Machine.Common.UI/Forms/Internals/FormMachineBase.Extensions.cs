using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Machine.UI.Common;

namespace Machine.UI
{
    public static class FormMachineBaseExtensions
    {
        public static void ShowAboveTransparentForm(this FormMachineBase form, float opacity = 0.97f)
        {
            var transparentForm = new FormTransparencyInjector()
            {
                TopMost = true,
                Opacity = opacity
            };

            form.TopMost = true;
            transparentForm.Show(form);
        }

        public static void ShowAndBringToFront(this FormMachineBase form)
        {
            form.TopLevel = true;
            form.TopMost = true;
            form.Show();
            form.WindowState = FormWindowState.Normal;
            form.BringToFront();
            form.Focus();
        }
    }
}
