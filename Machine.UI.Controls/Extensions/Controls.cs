using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Machine.UI.Controls.Extensions
{
    public static class MachineControlsExtensions
    {
        #region MachineButton
        public static void SetAllToActive(this IEnumerable<MachineButton> buttons)
        {
            foreach (var b in buttons)
            {
                b.Active = true;
            }
        }

        public static void SetAllToInactive(this IEnumerable<MachineButton> buttons)
        {
            foreach (var b in buttons)
            {
                b.Active = false;
            }
        }

        public static void SetRangeToActive(this IEnumerable<MachineButton> buttons, int indexStart, int indexStop)
        {
            if (indexStop > buttons.Count() || indexStart < 0)
            {
                return;
            }

            buttons.SetAllToInactive();

            for (int i = indexStart; i < indexStop; i++)
            {
                buttons.ElementAt(i).Active = true;
            }
        }

        public static void SetRangeToInactive(this IEnumerable<MachineButton> buttons, int indexStart, int indexStop)
        {
            if (indexStop > buttons.Count() || indexStart < 0)
            {
                return;
            }

            buttons.SetAllToActive();

            for (int i = indexStart; i < indexStop; i++)
            {
                buttons.ElementAt(i).Active = false;
            }
        }
        #endregion



        #region Control
        public static void SetAllToVisible(this IEnumerable<Control> controls)
        {
            foreach (var c in controls)
            {
                c.Visible = true;
            }
        }

        public static void SetAllToInvisible(this IEnumerable<Control> controls)
        {
            foreach (var c in controls)
            {
                c.Visible = false;
            }
        }

        public static void SetRangeToVisible(this IEnumerable<Control> controls, int indexStart, int indexStop)
        {
            if (indexStop > controls.Count() || indexStart < 0)
            {
                return;
            }

            controls.SetAllToInvisible();

            for (int i = indexStart; i < indexStop; i++)
            {
                controls.ElementAt(i).Visible = true;
            }
        }

        public static void SetRangeToInvisible(this IEnumerable<Control> controls, int indexStart, int indexStop)
        {
            if (indexStop > controls.Count() || indexStart < 0)
            {
                return;
            }

            controls.SetAllToVisible();

            for (int i = indexStart; i < indexStop; i++)
            {
                controls.ElementAt(i).Visible = false;
            }
        }
        #endregion
    }
}
