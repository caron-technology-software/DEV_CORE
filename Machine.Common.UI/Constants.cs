using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Machine.UI
{
    public static class Constants
    {
        public static class Sizes
        {
            public static class TopBar
            {
                public const int IconSize = 45;
            }

            public static class Dashboard
            {
                public const int ButtonSize = 125;
            }
        }

        public static class Intervals
        {
            public static readonly TimeSpan UpdateControlStatus = TimeSpan.FromMilliseconds(20);
            public static readonly TimeSpan UpdateUIControls = TimeSpan.FromMilliseconds(200);
            public static readonly TimeSpan CheckState = TimeSpan.FromMilliseconds(200);
            public static readonly TimeSpan SlowUpdateUIControls = TimeSpan.FromMilliseconds(1000);
            public static readonly TimeSpan Heartbeat = TimeSpan.FromMilliseconds(1000);
            public static readonly TimeSpan WaitUIShutdown = TimeSpan.FromMilliseconds(100);
            public static readonly TimeSpan WaitAfterUIDeinitialization = TimeSpan.FromMilliseconds(250);
            public static readonly TimeSpan WaitGeneric = TimeSpan.FromMilliseconds(50);
            public static readonly TimeSpan CheckLineaPlannerActive = TimeSpan.FromMilliseconds(500);

            public static readonly TimeSpan CutOffMinButtonInterval = TimeSpan.FromMilliseconds(400);

            public static readonly TimeSpan MinSliderUpdate = TimeSpan.FromMilliseconds(50);

            public static readonly TimeSpan DelayJogIfCradleInMovement = TimeSpan.FromMilliseconds(500);

            public static readonly TimeSpan DelayJogIfCradleIsStationary = TimeSpan.FromMilliseconds(25);
            public static readonly TimeSpan IntervalBetweenCradleJogs = TimeSpan.FromMilliseconds(25);
            public static readonly TimeSpan IntervalAfterMouseUpEvent = TimeSpan.FromMilliseconds(10);

            public static readonly TimeSpan FadeIn = TimeSpan.FromMilliseconds(150);
            public static readonly TimeSpan FadeOut = TimeSpan.FromMilliseconds(50);

        }

        public static class Colors
        {
            public static readonly Color TopBarBackground = Color.FromArgb(26, 37, 43);
            public static readonly Color MenuBackground = Color.FromArgb(200, 200, 200);
            public static readonly Color ActionsBackground = Color.FromArgb(200, 200, 200);
            public static readonly Color PanelInsideForm = Color.FromArgb(190, 190, 190);
            public static readonly Color DefaultBackgroung = Color.FromArgb(240, 240, 240);

            //https://www.schemecolor.com
            public static readonly Color TopButton = Color.FromArgb(170, 170, 170);
            public static readonly Color TopButtonActive = Color.FromArgb(210, 210, 210);
            public static readonly Color TopButtonStarted = Color.FromArgb(132, 186, 86);
            public static readonly Color TopButtonStopped = Color.FromArgb(198, 71, 66);

            public static readonly Color Red = Color.FromArgb(220, 20, 20);
            public static readonly Color Green = Color.FromArgb(20, 220, 20);
        }
    }
}
