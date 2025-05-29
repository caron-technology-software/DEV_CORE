using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using FontAwesome.Sharp;

using ProRob;

using Machine.UI.Communication;
using Machine.UI.Controls;
using Machine.Models;
using Machine.Common;

using Caron.Cradle.Control;
using Caron.Cradle.Control.LowLevel;

namespace Caron.Cradle.UI
{
    public partial class FormTopBar : FormCradleBase
    {
        public const int IconSize = Machine.UI.Constants.Sizes.TopBar.IconSize;

        public FormTopBar()
        {
            InitializeComponent();
        }

        private void FormTopBar_Load(object sender, EventArgs e)
        {
            string pathLogo = Path.Combine(Caron.Cradle.Constants.Path.AssetsFolder, "top_panel_logo.png");
            Bitmap bmp;
            if (File.Exists(pathLogo))
            {
                bmp = new Bitmap(pathLogo);
            }
            else
            {
                bmp = global::Caron.Cradle.UI.Properties.Resources.caron_logo_color_small;
            }

            bmp.MakeTransparent(bmp.GetPixel(0, 0));
            panelLogo.BackgroundImage = bmp;
            panelLogo.BackgroundImageLayout = ImageLayout.Center;

            panelShutdown.BackgroundImage = IconChar.PowerOff.ToBitmap(Machine.UI.Constants.Colors.TopButton, IconSize);
            panelShutdown.BackgroundImageLayout = ImageLayout.Center;

            panelWorkings.BackgroundImage = IconChar.Archive.ToBitmap(Machine.UI.Constants.Colors.TopButton, IconSize);
            panelWorkings.BackgroundImageLayout = ImageLayout.Center;

            panelRootSettings.BackgroundImage = IconChar.Cog.ToBitmap(Machine.UI.Constants.Colors.TopButton, IconSize);
            panelRootSettings.BackgroundImageLayout = ImageLayout.Center;

            panelBroswerInterface.BackgroundImage = IconChar.Tablet.ToBitmap(Machine.UI.Constants.Colors.TopButton, IconSize);
            panelBroswerInterface.BackgroundImageLayout = ImageLayout.Center;

            panelPlots.BackgroundImage = IconChar.ChartArea.ToBitmap(Machine.UI.Constants.Colors.TopButton, IconSize);
            panelPlots.BackgroundImageLayout = ImageLayout.Center;

            Task.Run(() =>
            {
                Thread.CurrentThread.Priority = ThreadPriority.Lowest;

                while (Supervisor.IsRunning)
                {
                    UpdateUIForm();

                    Thread.Sleep(Machine.UI.Constants.Intervals.SlowUpdateUIControls);
                }
            });
        }

        protected override void UpdateUIForm()
        {
            try
            {
                this?.Invoke((MethodInvoker)delegate ()
                {
                    labelTime.Text = DateTime.Now.ToString("HH:mm:ss");


                    #region Debug Info
                    if (Supervisor.Control.HighLevel.Settings.UI.ShowDebugInfo.Value)
                    {
                        panelDebug.Visible = true;

                        var state = Communicator.SendHttpGetRequest("state_machine", "current").Trim('"');
                        var subState = Communicator.SendHttpGetRequest("state_machine", "sub_state").Trim('"');
                        var statistics = Communicator.GetData<ControlStatistics>("statistics");
                        var lowLevelState = (Control.LowLevel.ControlState)Supervisor.Control.LowLevel.Info.MachineState;
                        int marchEnabledMachine = Supervisor.Control.LowLevel.IO.MachineInputs[(byte)MachineInput.MarchEnabled] ? 1 : 0;
                        var commandSpeed = Supervisor.Control.LowLevel.Axes.Cradle.DriverCommandSpeed;  //GPIx245

                        labelLine.Text = $"INFO [m:{marchEnabledMachine}] (f: {statistics.AveragePacketsForSecond.ToString("0.0")} n:{Supervisor.ControlStatusReceived} e: {statistics.CommunicationErrors} - {Supervisor.ControlStatusErrors}) cpu: {ApplicationInfo.AverageCpuUtilization.ToString("0.00")}% threads: {ApplicationInfo.NumberOfThreads}) t:{TimeSpan.FromHours(Supervisor.Control.HighLevel.MachineEndurance.WorkingHours.MachineMaintenanceHours).TotalSeconds:0.00}";

                        var sb = new StringBuilder();
                        sb.Append($"Control: {state} ({subState})\nUI: {Supervisor.UI.State}\nLow: {lowLevelState}        CommandSpeed: {commandSpeed}");//GPIx245
                        labelSubLine.Text = sb.ToString();
                    }
                    else
                    {
                        panelDebug.Visible = false;
                    }
                    #endregion

                    #region WorkingsStatistics
                    if (Supervisor.Control.HighLevel.WorkingStatus.InProgress)
                    {
                        panelWorkings.BackgroundImage = IconChar.Archive.ToBitmap(Machine.UI.Constants.Colors.TopButtonStarted, IconSize);
                    }
                    else
                    {
                        panelWorkings.BackgroundImage = IconChar.Archive.ToBitmap(Machine.UI.Constants.Colors.TopButtonStopped, IconSize);
                    }
                    #endregion

                    #region Plot
                    if (Supervisor.Control.HighLevel.Settings.UI.EnablePlots.Value)
                    {
                        panelPlots.Visible = true;
                    }
                    else
                    {
                        panelPlots.Visible = false;
                    }
                    #endregion
                });
            }
            catch
            {
                // --
            }
        }

        private void panelShutdown_Click(object sender, EventArgs e)
        {
            if (DialogResult.Cancel == MachineMessageBox.Show(Localization.Warning, Localization.AreYouSureToShutdownTheSystem))
            {
                return;
            }

#if TEST || DEBUG
            Supervisor.UIFunctions.Close();
#else
            Supervisor.UIFunctions.ShutdownMachine();
#endif
        }

        private void panelRootSettings_Click(object sender, EventArgs e)
        {
            using (var keyb = new TouchNumericKeyboard(Localization.InsertPassword, 0))
            {
                keyb.PasswordEnabled = true;
                keyb.ResetOnlyLabel();

                var dialogResult = keyb.ShowDialog();

                if ((dialogResult == DialogResult.Abort) ||
                    (dialogResult == DialogResult.Cancel))
                {
                    return;
                }

                switch (keyb.StringValue)
                {
                    case Caron.Constants.Password.Root:
                        Supervisor.SetUIState(StateUI.RootSettings, UserType.Root);
                        break;

                    case Caron.Constants.Password.Manufacturer:
                        Supervisor.SetUIState(StateUI.RootSettings, UserType.Manufacturer);
                        break;

                    case Caron.Constants.Password.Distributor:
                        Supervisor.SetUIState(StateUI.RootSettings, UserType.Distributor);
                        break;

                    case Caron.Constants.Password.User:
                        Supervisor.SetUIState(StateUI.RootSettings, UserType.User);
                        break;

                    default:
                        var msgBox = new MachineMessageBoxFullScreen(Localization.Warning, Localization.PasswordNotCorrect).ShowDialog();
                        break;
                }
            }
        }

        private void panelBroswerInterface_Click(object sender, EventArgs e)
        {
            if (Supervisor.UI.State != StateUI.BroswerInterface)
            {
                Supervisor.SetUIState(StateUI.BroswerInterface);
            }
            else
            {
                if (Supervisor.UI.PrecedentState != StateUI.RootSettings)
                {
                    Supervisor.SetPrecedentUIState();
                }
                else
                {
                    Supervisor.SetUIState(StateUI.Dashboard);
                }
            }
        }

        private void panelPlots_Click(object sender, EventArgs e)
        {
            var form = new FormMachinePlots();
            form.Show();
        }

        private void panelWorkings_Click(object sender, EventArgs e)
        {
            //Fix
            if (Supervisor.UI.State == StateUI.BroswerInterface)
            {
                Supervisor.SetUIState(StateUI.Dashboard);
            }

            Supervisor.SetUIState(StateUI.WorkingsStatistics);
        }

        private void LabelTime_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == MachineMessageBox.Show(Localization.Warning, Localization.DoYouWantToMinimizeApplication))
            {
                Supervisor.UI.Forms.Main.WindowState = FormWindowState.Minimized;

                return;
            }
        }
    }
}
