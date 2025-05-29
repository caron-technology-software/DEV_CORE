using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

using ProRob;
using ProRob.Extensions.Hashing;
using ProRob.Extensions.String;
using ProRob.Extensions.Json;
using ProRob.Extensions.Object;

using Machine;
using Machine.UI;
using Machine.UI.Communication;
using Machine.UI.Controls;
using Machine.Shell;
using Machine.Common;

using Caron.Cradle.Control.HighLevel.Settings;

namespace Caron.Cradle.UI
{
    public partial class FormRootSettings : FormCradleBase
    {
        private List<UserControl> rootFunctions;
        private List<UserControl> manufacturerFunctions;
        private List<UserControl> ditributorFunctions;
        private List<UserControl> userFunctions;

        public FormRootSettings()
        {
            InitializeComponent();

            #region Groups Functions
            rootFunctions = new List<UserControl>
            {
                mlMachineEndurance,
                mlMachineEnduranceLimits,
                mlMachineConfiguration,
                mlAnalogInputsCalibration,
                mlMachineParameters,
                mlLocalization,
                mlLowLevelStatus,
                mlSoftwareUpdate,
                mlResetSettings,
                mlBackupWorkingsSettings,
                mlUpdateWorkingSettings,
                mlBackupSystem,
                mlIOSettings,
                mlCmdMaintenance,
                mlShutdownApp,
                mlUpdateSettings,
                mlFilterUWF,
                mlResetMachineEndurance,
                mlBackupLog
            };

            manufacturerFunctions = new List<UserControl>
            {
                mlMachineEndurance,
                mlMachineEnduranceLimits,
                mlMachineConfiguration,
                mlAnalogInputsCalibration,
                mlMachineParameters,
                mlLocalization,
                mlLowLevelStatus,
                mlSoftwareUpdate,
                mlResetSettings,
                mlBackupWorkingsSettings,
                mlUpdateWorkingSettings,
                mlBackupSystem,
                mlIOSettings,
                mlCmdMaintenance,
                mlShutdownApp,
                mlUpdateSettings,
                mlFilterUWF,
                mlResetMachineEndurance,
                mlBackupLog
            };

            ditributorFunctions = new List<UserControl>
            {
                mlMachineConfiguration,
                mlAnalogInputsCalibration,
                mlMachineParameters,
                mlLocalization,
                mlLowLevelStatus,
                mlSoftwareUpdate,
                mlResetSettings,
                mlBackupWorkingsSettings,
                mlUpdateWorkingSettings,
                mlBackupSystem,
                mlCmdMaintenance,
                mlShutdownApp,
                mlUpdateSettings,
                mlFilterUWF,
                mlResetMachineEndurance,
                mlBackupLog
            };

            userFunctions = new List<UserControl>
            {
                mlLowLevelStatus,
                mlSoftwareUpdate,
                mlBackupWorkingsSettings,
                mlUpdateWorkingSettings,
                mlBackupSystem,
                mlCmdMaintenance,
                mlShutdownApp,
                mlFilterUWF,
                mlBackupLog
            };
            #endregion
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            #region Localization
            mlSoftwareVersion.Text = $"{Localization.SoftwareVersion}: {ApplicationInfo.ApplicationVersion}";
            mlMachineSerial.Text = $"{Localization.MachineSerial}: {Supervisor.Control.HighLevel.Configuration.MachineSerial}";
            mlIndustrialPcId.Text = $"{Localization.IndustrialPcId}: {Localization.Loading}";

            mlBackupWorkingsSettings.Text = Localization.BackupWorkingsSettings;
            mlUpdateWorkingSettings.Text = Localization.UpdateWorkingsSettings;
            mlMachineEndurance.Text = Localization.MachineWorkingStatus;
            mlMachineEnduranceLimits.Text = Localization.MachineWorkingStatusSettings;
            mlMachineConfiguration.Text = Localization.MachineConfiguration;
            mlAnalogInputsCalibration.Text = Localization.AnalogInputsCalibration;
            mlMachineParameters.Text = Localization.MachineParameters;
            mlLocalization.Text = Localization.Language;
            mlLowLevelStatus.Text = Localization.LowLevelStatus;
            mlSoftwareUpdate.Text = Localization.SoftwareUpdate;
            mlResetSettings.Text = Localization.ResetSettings;
            mlBackupSystem.Text = Localization.BackupSystem;
            mlIOSettings.Text = Localization.InputOutputSettings;
            mlCmdMaintenance.Text = Localization.SystemMaintenanceCommands;
            mlShutdownApp.Text = Localization.CloseApplication;
            mlUpdateSettings.Text = Localization.UpdateSettings;
            //mlBackupLog.Text = "Backup Log";
            mlBackupLog.Text = Localization.BackupLog;     ////GIMMI01
            mlFilterUWF.Text = Localization.UWFFilter;
            mlResetMachineEndurance.Text = Localization.ResetMachineWorkingStatus;
            #endregion

            #region Buttons
            cbReturn.Active = false;
            cbReturn.FlatAppearance.MouseDownBackColor = Machine.UI.Constants.Colors.TopBarBackground;
            cbReturn.FlatAppearance.BorderColor = Machine.UI.Constants.Colors.TopBarBackground;
            #endregion
        }

        private void FormRootSettings_Load(object sender, EventArgs e)
        {
            #region Background Task
            Task.Run(() =>
            {
                this?.Invoke((MethodInvoker)delegate ()
                {
                    string id = ProRob.SystemInfo.SystemId.GetSystemId();
                    mlIndustrialPcId.Text = $"{Localization.IndustrialPcId}: {id}";
                });
            });
            #endregion

            TopLevel = true;
        }

        private void ArrangeFunctions(UserType userType)
        {
            foreach (var function in manufacturerFunctions)
            {
                function.Enabled = true;
                function.Visible = true;
            }

            switch (userType)
            {
                case UserType.Root:

                    mlTitle.Text = $"{Localization.Settings} ({Localization.Root})";

                    break;

                case UserType.Manufacturer:

                    mlTitle.Text = $"{Localization.Settings} ({Localization.Manufacturer})";

                    break;

                case UserType.Distributor:
                    mlTitle.Text = $"{Localization.Settings}: ({Localization.Distributor})";

                    foreach (var function in rootFunctions.Except(ditributorFunctions))
                    {
                        function.Enabled = false;
                        function.Visible = false;
                    }
                    break;

                case UserType.User:
                    mlTitle.Text = $"{Localization.Settings}: ({Localization.User})";

                    foreach (var function in rootFunctions.Except(userFunctions))
                    {
                        function.Enabled = false;
                        function.Visible = false;
                    }
                    break;
            }

            //------------------------------------------------------------------
            // Calculates Buttons location
            //------------------------------------------------------------------
            #region Calculates Buttons location
            var controls = manufacturerFunctions.Where(x => x.Visible == true).ToList();

            int nc = 3;
            int nr = (int)Math.Ceiling((double)controls.Count / (double)nc);

            int w = Width;
            int wc = controls.First().Width;
            int dx = 30;

            int h = Height;
            int hc = controls.First().Height;
            int hs = 40;
            int h1 = panelForm.Height + hs;
            int h2 = hs;
            int dy = 10;

            int xs = (int)((double)(w - nc * wc - (nc - 1) * dx) / 2.0);
            int ys = (int)((double)(h - h1 - h2 - nr * hc - (nr - 1) * dy) / 2.0) + h1;

            int idx = 0;

            for (int r = 1; r <= nr; r++)
            {
                for (int c = 1; c <= nc; c++)
                {
                    if (idx < controls.Count)
                    {
                        var location = new Point();

                        location.X = xs + (c - 1) * (wc + dx);
                        location.Y = ys + (r - 1) * (hc + dy);

                        controls[idx].Location = location;
                    }

                    idx++;
                }
            }
            #endregion
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            if (Visible)
            {
                UserType userType = (UserType)UIStateParameter;

                //-------------------------------------------------------
                // SuspendLayout
                //-------------------------------------------------------
                SuspendLayout();

                ArrangeFunctions(userType);

                //-------------------------------------------------------
                // ResumeLayout
                //-------------------------------------------------------
                ResumeLayout();
            }

            if (Visible)
            {
                Machine.UI.FormsControlsHelper.FadeInEffect(this.Handle, TimeSpan.FromMilliseconds(150));
            }
            else
            {
                Machine.UI.FormsControlsHelper.FadeOutEffect(this.Handle, TimeSpan.FromMilliseconds(50));
            }

        }

        protected override void UpdateUIForm()
        {
            //--
        }

        private void CbReturn_Click(object sender, EventArgs e)
        {
            //GPIx101 3)
            if (Supervisor.Control.HighLevel.Settings.HighLevel.FunctionsEnabled.EnableFunctionPhotocellRollPresence.Value == false)
            {
                FormCollection fc = Application.OpenForms;
                foreach (Form frm in fc)
                {
                    if (frm.Name == "FormUserSettings")
                    {
                        Console.WriteLine(frm.Name);
                        ((FormUserSettings)frm).cbEnablePhotocellRollPresence.Visible = false;
                    }
                }
            }
            else
            {
                FormCollection fc = Application.OpenForms;
                foreach (Form frm in fc)
                {
                    if (frm.Name == "FormUserSettings")
                    {
                        Console.WriteLine(frm.Name);
                        ((FormUserSettings)frm).cbEnablePhotocellRollPresence.Visible = true;
                    }
                }
            }
            //GPFx101

            cbReturn.PulseButton();
            cbReturn.Active = false;

            Supervisor.SetPrecedentUIState();
        }

        private void MlMachineParameters_Click(object sender, EventArgs e)
        {
            var form = new FormMachineParameters();
            form.ShowAboveTransparentForm();
        }

        private void MlLocalization_Click(object sender, EventArgs e)
        {
            var form = new FormLocalization();
            form.ShowAboveTransparentForm();
        }

        private void MlMachineConfiguration_Click(object sender, EventArgs e)
        {
            var form = new FormMachineConfiguration();
            form.ShowAboveTransparentForm();
        }

        private void MlSoftwareUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                //GPIx120
                MachineMessageBoxFullScreen msgBoxX = null;

                if (UWF.IsInstalled() == true)
                {
                    if (UWF.IsEnabled())
                    {
                        //msgBoxX = new MachineMessageBoxFullScreen(Localization.Warning, $"{Localization.UWFFilter}: {Localization.Enable.ToLower()}. {Localization.YouCannotProceedWithTheUpdate}");
                        msgBoxX = new MachineMessageBoxFullScreen(Localization.Warning, $"{Localization.YouCannotProceedWithTheUpdate}");
                        msgBoxX.Show();

                        return;
                    }
                }
                //GPFx120

                if (!RemovableDrives.IsPresentRemovableDevice())
                {
                    MachineMessageBox.Show(Localization.Info, Localization.InsertRemovableDevice);
                    return;
                }

                var files = Directory.GetFiles(RemovableDrives.GetLetterDrive());

                var filePath = files.ToList().Where(x => x.Contains("cradle_software_update_")).FirstOrDefault();

                if (filePath is null || string.IsNullOrEmpty(filePath))
                {
                    MachineMessageBox.Show(Localization.Warning, Localization.UpdateFileNotFound);
                    return;
                }

                //Start update process
                var msgBoxStartUpdate = new MachineMessageBoxFullScreen(Localization.Info, Localization.PressOkToStartTheProcess);
                msgBoxStartUpdate.ShowDialog();

                FileSystem.DeleteDirectory(Constants.Path.UpdateFolder);
                Directory.CreateDirectory(Constants.Path.UpdateFolder);

                File.Copy(filePath, Path.Combine(Constants.Path.UpdateFolder, "software_update.prsw"));

                var softwareUpdate = new Machine.SoftwareUpdate.CheckSoftwareUpdateBundle(Constants.Path.RootFolder, Constants.Path.BinUtilityFolder);
                string password = $"{"CARON Technology".GetSHA512Hash()} {"YORK".GetSHA512Hash()} {"WREG-8450-kepd-2048".GetSHA512Hash()}".GetSHA512Hash().ToBase64();
                bool checkerCond = softwareUpdate.CheckIntegrity(Path.Combine(Constants.Path.UpdateFolder, "software_update.prsw"), password);

                if (checkerCond)
                {
                    var msgBox = new MachineMessageBoxFullScreen(Localization.Info, Localization.RebootMachineToStartUpdateProcess);
                    msgBox.ShowDialog();
                }
                else
                {
                    var msgBox = new MachineMessageBoxFullScreen(Localization.Error, Localization.UnexpectedError);
                    msgBox.ShowDialog();
                }
            }
            catch
            {
                MachineMessageBox.Show(Localization.Error, Localization.UnexpectedError);
            }
        }

        private void MlLowLevelStatus_Click(object sender, EventArgs e)
        {
            //GPIx21
            //change//Communicator.SetHighLevelControlState("io_config");

            //change//Communicator.SendHttpGetRequest($"external_commands", $"set_enable_io_settings?enable=true");

            //GPFx21
            var form = new FormLowLevelStatus();
            form.ShowAboveTransparentForm();
        }

        private void MlResetSettings_Click(object sender, EventArgs e)
        {
            if (File.Exists(Constants.Path.Settings.DefaultSettingsFile) == false)
            {
                var msgBox = new MachineMessageInfo(Localization.Warning, $"{Localization.FileNotFound}: {Constants.Path.Settings.DefaultSettingsFile}");
                msgBox.ShowDialog();

                return;
            }

            if (DialogResult.OK == MachineMessageBox.Show(Localization.Warning, Localization.AreYouSureToResetSettings))
            {
                Communicator.SendHttpGetRequest("machine_settings/reset");
                MachineMessageBox.Show(Localization.Warning, Localization.RestartMachineToActivateFunctions);
            }
        }

        private void MlResetMachineEndurance_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == MachineMessageBox.Show(Localization.Warning, Localization.AreYouSureToResetSettings))
            {
                ProConsole.WriteTitle("endurance/reset", ConsoleColor.Yellow);

                Communicator.SendHttpGetRequest("endurance/reset");

                MachineMessageBox.Show(Localization.Warning, Localization.RestartMachineToActivateFunctions);
            }
        }

        private void MlBackupWorkingSettings_Click(object sender, EventArgs e)
        {
            try
            {
                if (!RemovableDrives.IsPresentRemovableDevice())
                {
                    MachineMessageBox.Show(Localization.Info, Localization.InsertRemovableDevice);
                }
                else
                {
                    string filePath = Path.Combine(RemovableDrives.GetLetterDrive(), $"backup_cradle_working_settings.json");

                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }

                    Supervisor.Control.HighLevel.WorkingsSettings.SoftwareVersion = mlSoftwareVersion.Text;
                    Supervisor.Control.HighLevel.WorkingsSettings.ToJson().SaveToFile(filePath);

                    MachineMessageBox.Show(Localization.FileSavedSuccessfully, filePath);
                }
            }
            catch
            {
                MachineMessageBox.Show(Localization.Error, Localization.UnexpectedError);
            }
        }

        private void MlIOSettings_Click(object sender, EventArgs e)
        {
            var form = new FormIOSettings();
            form.ShowAboveTransparentForm();
        }

        private void MlCmdMaintenance_Click(object sender, EventArgs e)
        {
            //GPIx120
            MachineMessageBoxFullScreen msgBoxX = null;

            if (UWF.IsInstalled() == true)
            {
                if (UWF.IsEnabled())
                {
                    //msgBoxX = new MachineMessageBoxFullScreen(Localization.Warning, $"{Localization.UWFFilter}: {Localization.Enable.ToLower()}. {Localization.YouCannotProceedWithTheUpdate}");
                    msgBoxX = new MachineMessageBoxFullScreen(Localization.Warning, $"{Localization.YouCannotProceedWithTheUpdate}");
                    msgBoxX.Show();

                    return;
                }
            }
            //GPFx120

            var form = new FormShell(@"fsutil dirty set c:", Machine.Shell.CommandType.CommandPrompt);
            form.Title = Localization.SystemMaintenanceCommands;
            form.WaitBeforeExit = TimeSpan.FromSeconds(2);
            form.Show();
        }

        private void MlMachineCalibration_Click(object sender, EventArgs e)
        {
            var form = new FormAnalogInputsCalibration();
            form.ShowAboveTransparentForm();
        }

        private void MlMachineEnduranceLimits_Click(object sender, EventArgs e)
        {
            var form = new FormMachineEnduranceLimits();
            form.ShowAboveTransparentForm();
        }

        private void MlMachineEndurance_Click(object sender, EventArgs e)
        {
            var form = new FormMachineEndurance();
            form.ShowAboveTransparentForm();
        }

        private void MlShutdownApp_Click(object sender, EventArgs e)
        {
            if (DialogResult.Cancel == MachineMessageBox.Show(Localization.Warning, Localization.AreYouSureToCloseApplicationWithoutShutdownMachine))
            {
                return;
            }

            Supervisor.UIFunctions.Close();
        }

        private void MlRestoreWorkingSettings_Click(object sender, EventArgs e)
        {
            try
            {
                if (!RemovableDrives.IsPresentRemovableDevice())
                {
                    MachineMessageBox.Show(Localization.Info, Localization.InsertRemovableDevice);
                }
                else
                {
                    string filePath = Path.Combine(RemovableDrives.GetLetterDrive(), $"backup_cradle_working_settings.json");

                    if (!File.Exists(filePath))
                    {
                        MachineMessageBox.Show(Localization.Error, $"{Localization.FileNotFound}");
                        return;
                    }

                    var ws = Json.Deserialize<Control.HighLevel.Settings.WorkingsSettings>(File.ReadAllText(filePath));

                    var dialogResult = MachineMessageBox.Show(Localization.Warning, Localization.PressOkToStartTheProcess);

                    if (dialogResult == DialogResult.OK)
                    {
                        Communicator.SendHttpPostRequest("workings_settings", ws);
                    }
                }
            }
            catch
            {
                MachineMessageBox.Show(Localization.Error, Localization.UnexpectedError);
            }
        }

        private void mpInfo_DoubleClick(object sender, EventArgs e)
        {
            System.Windows.Forms.MessageBox.Show(ApplicationInfo.Instance.ToString(), "Application Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MlBackupSystemSettings_Click(object sender, EventArgs e)
        {
            try
            {
                //BACKUP
                {
                    var settings = new MachineSettings()
                    {
                        SoftwareVersion = mlSoftwareVersion.Text,
                        HighLevel = Supervisor.Control.HighLevel.Settings.HighLevel.Clone(),
                        UI = Supervisor.Control.HighLevel.Settings.UI.Clone(),
                        LowLevelMotion = Supervisor.Control.HighLevel.Settings.LowLevelMotion.Clone()
                    };

                    var json = Json.Serialize(settings);

                    Directory.CreateDirectory(Constants.Path.BackupsFolder);

                    File.WriteAllText(Path.Combine(Constants.Path.BackupsFolder, "backup_cradle_settings.json"), json);
                

                if (!RemovableDrives.IsPresentRemovableDevice())
                {
                    MachineMessageBox.Show(Localization.Info, Localization.InsertRemovableDevice);
                }
                else
                {

                    string path = Path.Combine(RemovableDrives.GetLetterDrive(), $"backup_cradle_settings.json");

                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }

                    //ZipFile.CreateFromDirectory(Cradle.Constants.Path.SettingsFolder, zipPath);
                    Supervisor.Control.HighLevel.Settings.SoftwareVersion = mlSoftwareVersion.Text;
                    File.WriteAllText(path, Json.Serialize(Supervisor.Control.HighLevel.Settings));
                    
                    MachineMessageBox.Show(Localization.FileSavedSuccessfully, path);
                }

                }

            }
            catch
            {
                MachineMessageBox.Show(Localization.Error, Localization.UnexpectedError);
            }
        }

        private void MlUpdateSettings_Click(object sender, EventArgs e)
        {
            try
            {
                if (!RemovableDrives.IsPresentRemovableDevice())
                {
                    MachineMessageBox.Show(Localization.Info, Localization.InsertRemovableDevice);
                }
                else
                {
                    string filePath = Path.Combine(RemovableDrives.GetLetterDrive(), $"backup_cradle_settings.json");

                    if (!File.Exists(filePath))
                    {
                        MachineMessageBox.Show(Localization.Error, $"{Localization.FileNotFound}");
                        return;
                    }

                    var settings = Json.Deserialize<MachineSettings>(File.ReadAllText(filePath));


                    ////Json.ISettingsReader settingsReader = new Json.SettingsReader(filePath);
                    ////settings.EnduranceLimits = settingsReader.LoadSection<MachineEnduranceLimits>();
                    ////settings.MachineParameters = settingsReader.LoadSection<MachineParameters>();
                    ////settings.FunctionsEnabled = settingsReader.LoadSection<FunctionsEnabled>();

                    var dialogResult = MachineMessageBox.Show(Localization.Warning, Localization.PressOkToStartTheProcess);

                    if (dialogResult == DialogResult.OK)
                    {
                        Communicator.SendHttpPostRequest("machine_settings", settings);

                        MachineMessageBox.Show(Localization.Warning, Localization.RestartMachineToActivateFunctions);
                    }
                }
            }
            catch
            {
                MachineMessageBox.Show(Localization.Error, Localization.UnexpectedError);
            }
        }

        private void MlFilterUWF_Click(object sender, EventArgs e)
        {
            MachineMessageBoxFullScreen msgBox = null;

            if (UWF.IsInstalled() == false)
            {
                msgBox = new MachineMessageBoxFullScreen(Localization.Error, $"{Localization.UWFFilter}: {Localization.Inactive.ToLower()}");
                msgBox.Show();

                return;
            }
            else if (UWF.IsEnabled())
            {
                msgBox = new MachineMessageBoxFullScreen(Localization.Warning, $"{Localization.UWFFilter}: {Localization.Enable.ToLower()}. {Localization.DoYouWantToDisable}");

                if (msgBox.ShowDialog() == DialogResult.OK)
                {
                    UWF.Disable();

                    msgBox = new MachineMessageBoxFullScreen(Localization.Warning, $"{Localization.PleaseShutdownAndRestartMachine}");
                    msgBox.ShowDialog();
                }
            }
            else
            {
                msgBox = new MachineMessageBoxFullScreen(Localization.Warning, $"{Localization.UWFFilter}: {Localization.Disable.ToLower()}. {Localization.DoYouWantToEnable}");

                if (msgBox.ShowDialog() == DialogResult.OK)
                {
                    UWF.Enable();

                    msgBox = new MachineMessageBoxFullScreen(Localization.Warning, $"{Localization.PleaseShutdownAndRestartMachine}");
                    msgBox.ShowDialog();
                }
            }
        }

        private void mlBackupLog_Click(object sender, EventArgs e)
        {
            if (!RemovableDrives.IsPresentRemovableDevice())
            {
                MachineMessageBox.Show(Localization.Info, Localization.InsertRemovableDevice);
            }
            else
            {

                //chiusura file di log prima di esportarli:
                Console.WriteLine("logs/stop_loggingX");
                Communicator.SendHttpGetRequest("logs/stop_loggingX");
                //chiusura file di log prima di esportarli-fine.

                string prefix = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string filename = prefix + "_log_archive.zip";
                string destpath = Path.Combine(RemovableDrives.GetLetterDrive(), filename);
                //string startpath = Path.Combine(Constants.Path.LogsFolder, filename);
                string startpath = Path.Combine(Constants.Path.MachineFolder, filename);

                try
                {
                    ZipFile.CreateFromDirectory(Constants.Path.LogsFolder, startpath);
                }
                catch (ArgumentNullException e01)
                {
                    Console.WriteLine("An exception ({0}) occurred.",
                                      e01.GetType().Name);
                    Console.WriteLine("Message:\n   {0}\n", e01.Message);
                    Console.WriteLine("Stack Trace:\n   {0}\n", e01.StackTrace);
                }

                //apertura log dopo averli esportati:
                Console.WriteLine("logs/restart_loggingX");
                Communicator.SendHttpGetRequest("logs/restart_loggingX");
                //apertura log dopo averli esportati-fine.

                if (File.Exists(startpath))
                {
                    File.Copy(startpath, destpath, true);
                }

                File.Delete(startpath);

                MachineMessageBox.Show(Localization.FileSavedSuccessfully, destpath);
            }
        }
    }
}
