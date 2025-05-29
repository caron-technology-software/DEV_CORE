using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

using Machine;
using Machine.UI.Communication;
using Machine.UI.Controls;
using Machine.Utility;
using ProRob;

namespace Caron.Cradle.UI
{
    public partial class FormLocalization : FormCradleBase
    {
        public FormLocalization()
        {
            InitializeComponent();
        }

        protected override void UpdateUIForm()
        {
            labelTitle.Text = Machine.Localization.MachineLanguage.ToString();
        }

        private void SetButton(MachineButton button, Bitmap image, MachineLanguage machineLanguage)
        {
            button.StateChangeActivated = false;
            button.SetImages(image, image);
            button.BackColor = Color.Gainsboro;
            button.FlatAppearance.MouseDownBackColor = Color.Gainsboro;
            button.FlatAppearance.BorderColor = Color.Gainsboro;

            button.Click += (obj, args) => { SetLanguage(machineLanguage); };
        }

        private void SetLanguage(MachineLanguage machineLanguage)
        {

            Communicator.SetVariable("localization/language", "machineLanguage", machineLanguage.ToString());

            MachineMessageBox.Show(Localization.CradleModel, Localization.Welcome);
            UpdateUIForm();
        }

        private void FormRootSettingsLanguage_Load(object sender, EventArgs e)
        {
            cbReturn.StateChangeActivated = false;

            SetButton(cbItalian, Machine.UI.ResourceFlags.italy, MachineLanguage.Italiano);
            SetButton(cbEnglish, Machine.UI.ResourceFlags.united_kingdom, MachineLanguage.English);
            SetButton(cbFrench, Machine.UI.ResourceFlags.france, MachineLanguage.Français);
            SetButton(cbGerman, Machine.UI.ResourceFlags.germany, MachineLanguage.Deutsch);
            SetButton(cbSpanish, Machine.UI.ResourceFlags.spain, MachineLanguage.Español);
            SetButton(cbRussian, Machine.UI.ResourceFlags.russia, MachineLanguage.Pусский);
            SetButton(cbPolish, Machine.UI.ResourceFlags.republic_of_poland, MachineLanguage.Polski);
            SetButton(cbPortuguese, Machine.UI.ResourceFlags.portugal, MachineLanguage.Português);
            SetButton(cbNetherlands, Machine.UI.ResourceFlags.netherlands, MachineLanguage.Nederlands);
            SetButton(cbUserDefined, Machine.UI.ResourceFlags.european_union, MachineLanguage.UserDefined);
        }

        private void cbReturn_Click(object sender, EventArgs e)
        {
            cbReturn.PulseButton();
            cbReturn.Active = false;

            Supervisor.Events.InvokeSystemLocalizationEvent();

            Close();
        }

        private void mbLoad_Click(object sender, EventArgs e)
        {
            try
            {
                if (!RemovableDrives.IsPresentRemovableDevice())
                {
                    MachineMessageBox.Show(Localization.Info, Localization.InsertRemovableDevice);
                }
                else
                {
                    string filePath = Path.Combine(
                                          RemovableDrives.GetLetterDrive(),
                                          FileSystem.GetFilenameFromFullPath(Cradle.Constants.Path.Data.LocalizationsFile));

                    if (!File.Exists(filePath))
                    {
                        MachineMessageBox.Show(Localization.Error, $"{Localization.FileNotFound}");
                        return;
                    }

                    var msgBox = new MachineMessageBox(Localization.Warning, Localization.PressOkToStartTheProcess);

                    if (msgBox.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        File.Delete(Cradle.Constants.Path.Data.LocalizationsFile);
                        File.Copy(filePath, Cradle.Constants.Path.Data.LocalizationsFile);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Exception] Source:{ex.Source} Message:{ex.Message}");
                MachineMessageBox.Show(Localization.Error, Localization.UnexpectedError);
            }
        }
    }
}
