using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using FontAwesome.Sharp;

using ProRob.Threading;

using Machine.DataCollections;
using Machine;

namespace Caron.Cradle.UI
{
    public partial class FormMachinePlots : FormCradleBase
    {
        private const int ButtonSize = 60;

        private ThreadDispatcher threadDispatcher;

        private volatile int indexComboBoxItems = 0;
        private volatile bool showLastPeriod = false;
        private volatile bool threadRunning = false;

        public FormMachinePlots()
        {
            InitializeComponent();

            machineTransparentPanel.BringToFront();

            panelRefresh.BackgroundImage = IconChar.WindowRestore.ToBitmap(Machine.UI.Constants.Colors.TopButton, Machine.UI.Constants.Sizes.TopBar.IconSize);
            panelRefresh.BackgroundImageLayout = ImageLayout.Center;

            cbReturn.Size = new Size(ButtonSize, ButtonSize);

            comboBoxItems.Items.AddRange(new string[] {
                $"{Localization.Cradle} ({Localization.Position})",
                $"{Localization.Cradle} ({Localization.Velocity})",
                $"{Localization.Cradle} ({Localization.Error})",
                $"{Localization.Cradle} ({Localization.DriverCommandSpeed})",
                $"{Localization.Cradle} ({Localization.ProportionalAction})",
                $"{Localization.Cradle} ({Localization.IntegralAction})",
                $"{Localization.Cradle} ({Localization.DerivativeAction})",
                $"{Localization.Cradle} ({Localization.FeedForwardAction})",

                $"{Localization.Table} ({Localization.Position})",
                $"{Localization.Table} ({Localization.Velocity})",
                $"{Localization.Dancer} ({Localization.Percentual})"}
            );

            comboBoxItems.SelectedIndexChanged += ComboBoxItems_SelectedIndexChanged;
            comboBoxItems.SelectedIndex = indexComboBoxItems;

            //Azione di apertura PopUp in caso di click
            comboBoxItems.Click += (sender, args) => comboBoxItems.DroppedDown = true;

            threadDispatcher = new ThreadDispatcher(
            new ThreadConfiguration[]
            {
                new ThreadConfiguration(ThreadPlotUpdate,ThreadPriority.Lowest)
            });
        }

        private void ComboBoxItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            indexComboBoxItems = comboBoxItems.SelectedIndex;

            plot.SendToBack();
            machineTransparentPanel.BringToFront();

            Refresh();
        }

        private void FormMachineCharts_Load(object sender, EventArgs e)
        {
            //--
        }

        private void cbReturn_Click(object sender, EventArgs e)
        {
            StopThread();
            Close();
        }

        private void panelRefresh_Click(object sender, EventArgs e)
        {
            showLastPeriod = !showLastPeriod;
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            if (Visible)
            {
                StartThread();
            }
            else
            {
                StopThread();
            }

            base.OnVisibleChanged(e);
        }

        private void StartThread()
        {
            if (threadRunning)
            {
                return;
            }

            threadRunning = true;
            threadDispatcher.Start();
        }

        private void StopThread()
        {
            if (!threadRunning)
            {
                return;
            }

            threadRunning = false;

            threadDispatcher.WaitWithTimeout(TimeSpan.FromMilliseconds(100));

            threadDispatcher.Stop();
        }
    }
}

