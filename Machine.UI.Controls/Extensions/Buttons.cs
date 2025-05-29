using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Machine.UI.Controls
{
    public static class MachineButtonsExtensions
    {
        public static void PulseButton(this MachineButton machineButton, int delay = 200, int repetition = 1)
        {
            bool currentState = machineButton.Active;

            for (int i = 0; i < repetition; i++)
            {
                machineButton.Active = !currentState;
                Thread.Sleep(delay);
                machineButton.Active = currentState;
                if (repetition > 1)
                {
                    Thread.Sleep(delay);
                }
            }
        }

        public static void PulseButton(this MachineButtonLabel machineButton, int delay = 200, int repetition = 1)
        {
            bool currentState = machineButton.Active;

            for (int i = 0; i < repetition; i++)
            {
                machineButton.Active = !currentState;
                Thread.Sleep(delay);
                machineButton.Active = currentState;
                if (repetition > 1)
                {
                    Thread.Sleep(delay);
                }
            }
        }

        public static void SetDefaultBackgroundImages(this MachineButtonLabel button)
        {
            button.ActiveBackgroundImage = Machine.UI.Controls.Properties.Resources.verified_green;
            button.InactiveBackgroundImage = Machine.UI.Controls.Properties.Resources.verified_gray;
        }

        public static void SetButtonActiveAndUnavailable(this MachineButtonLabel button)
        {
            button.Enabled = false;
            button.SetActiveWithoutEvent(true);
            button.ActiveBackgroundImage = Machine.UI.Controls.Properties.Resources.verified_active_unavailable_green;
            button.InactiveBackgroundImage = Machine.UI.Controls.Properties.Resources.verified_active_unavailable_gray;
        }

        public static void SetButtonInactiveAndUnavailable(this MachineButtonLabel button)
        {
            button.Enabled = false;
            button.SetActiveWithoutEvent(false);
            button.ActiveBackgroundImage = Machine.UI.Controls.Properties.Resources.verified_unactive_unavailable_green;
            button.InactiveBackgroundImage = Machine.UI.Controls.Properties.Resources.verified_unactive_unavailable_gray;
        }
    }
}
