using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Machine.Security;

using ProRob.Extensions.Encoding;
using ProRob.Extensions.Object;

using Machine.UI.Communication;
using Machine.UI.Controls;

namespace Caron.Cradle.UI
{
    public partial class FormLicenses : FormCradleBase
    {
        //GPIx243
        public volatile static string EthercutCodeError = "";
        //GPFx243
        public FormLicenses()
        {
            InitializeComponent();
        }

        private void RefreshCode()
        {
            var rnd = new Random();

            byte[] code = Enumerable.Range(0, 8).Select(x => (byte)rnd.Next(0, 255)).ToArray();

            //GPIx243
            //labelCode.Text = code.ToBase62();
            labelCode.Text = Supervisor.Control.HighLevel.MachineEndurance.Statistics.EthercatCode.ToString("D3");
            //GPFx243
        }

        private void FormError_Load(object sender, EventArgs e)
        {
            labelTitle.Text = $"{Localization.Warning}";
            //GPIx243
            //labelMessage.Text = $"{Localization.MachineMaintenanceFakeAlert}";
            labelMessage.Text = Localization.EthercatCodeError + " Code: ";
            //GPFx243

            RefreshCode();
        }

        protected override void UpdateUIForm()
        {
            base.UpdateUIForm();
        }

        private void labelCode_DoubleClick(object sender, EventArgs e)
        {
            string unlockCode = "";

            using (var keyb = new TouchAlphaNumericKeyboard(Localization.Code, "", TouchKeyboardType.NoSymbols))
            {
                keyb.ShowDialog();

                unlockCode = keyb.StringValue;
            }

            //GPIx243 da modificare con tua procedura per decodifica error code e verifica sblocco!!!:

            int result = 0;
            int varCodeAll = int.Parse(labelCode.Text);//Supervisor.Control.HighLevel.WorkingsSettings.GeneratedEthercatErrorAtStart;
            int varCodeKey;
            try
            {
                varCodeKey = int.Parse(unlockCode);//listboxStatistics.GetValue(2);
            }
            catch
            {
                MachineMessageBox.Show(Localization.Error, Localization.InvalidCode);
                return;
            }
            int varAux = varCodeKey / 1000;
            int varTemp = 0;
            int var0 = 0;
            int var1 = 0;
            int var2 = 0;
            for (int i = 0; i < 3; i++)
            {
                //float f1 = (float)varCodeKey % 1; //resto
                varTemp = varAux % 10;
                var0 = var0 + varTemp;
                if ((i % 2) == 1)
                {
                    var2 = var2 + varTemp;
                }
                else
                {
                    var1 = var1 + varTemp;
                }
                varAux = varAux / 10;
            }

            int varT0 = 0;
            int varT1 = 0;
            int varT2 = 0;
            int varCodeAllX = varCodeAll;
            int varCodeAll2 = varCodeAllX % 10;
            varCodeAllX = varCodeAllX / 10;
            int varCodeAll1 = varCodeAllX % 10;
            varCodeAllX = varCodeAllX / 10;
            int varCodeAll0 = varCodeAllX % 10;
            varT0 = ((var0 % 10) + 1) + varCodeAll0;
            varT1 = ((var1 % 10) + 2) + varCodeAll1;
            varT2 = ((var2 % 10) + 3) + varCodeAll2;
            varT0 = varT0 % 10;
            varT1 = varT1 % 10;
            varT2 = varT2 % 10;
            int varCodeKeyX = varCodeKey;
            int varCodeKey5 = varCodeKeyX % 10;
            varCodeKeyX = varCodeKeyX / 10;
            int varCodeKey4 = varCodeKeyX % 10;
            varCodeKeyX = varCodeKeyX / 10;
            int varCodeKey3 = varCodeKeyX % 10;

            if ((varCodeKey3 == varT0) && (varCodeKey4 == varT1) && (varCodeKey5 == varT2))
            {
                result = (varCodeKey / 1000);
            }

            var me = Supervisor.Control.HighLevel.MachineEndurance.Clone();
            bool b01 = true;
            if (result == 115) //sblocco totale
            {
                var limits = Supervisor.Control.HighLevel.Settings.HighLevel.EnduranceLimits.Clone();
                //var limits = Supervisor.Control.HighLevel.Settings.MachineEnduranceLimits.Clone();
                limits.WorkingHours.WorkingFakeHours = 0;
                //Communicator.SendHttpPostRequest("endurance_limits", limits);
                Communicator.SendHttpPostRequest("settings/root/endurance_limits", limits);
                me.Statistics.EthercatCode = 0;
                MachineMessageBox.Show(Localization.Warning, Localization.RestartMachineToActivateFunctions);
                b01 = false;
            }
            if (result == 119) //sblocco parziale
            {
                me.WorkingHours.WorkingFakeHours = 0;
                me.Statistics.EthercatCode = 0;
                MachineMessageBox.Show(Localization.Warning, Localization.RestartMachineToActivateFunctions);
                b01 = false;
            }
            if (b01)
            {
                MachineMessageBox.Show(Localization.Error, Localization.InvalidCode);
            }
            Communicator.SendHttpPostRequest("endurance", me);

            //try
            //{
            //    var decryptedBytes = Encryption.Decrypt(unlockCode.FromBase62(), labelCode.Text);
            //    var decryptedString = Encoding.ASCII.GetString(decryptedBytes);

            //    //MessageBox.Show(decryptedString, "Decryption");
            //    string unlockHeader = decryptedString.Substring(0, 6);
            //    int code = int.Parse(decryptedString.Substring(6 + 1, 2));
            //    int numberOfHours = int.Parse(decryptedString.Substring(10));

            //    if (unlockHeader.Equals("#UNLK#"))
            //    {
            //        //Reset Machine Endurance
            //        var endurance = Supervisor.Control.HighLevel.MachineEndurance.Clone();
            //        endurance.WorkingHours.WorkingFakeHours = 0;
            //        Communicator.SendHttpPostRequest("endurance", endurance);

            //        //Set Machine Endurance Limits
            //        var limits = Supervisor.Control.HighLevel.Settings.HighLevel.EnduranceLimits.Clone();
            //        limits.WorkingHours.WorkingFakeHours = numberOfHours;
            //        Communicator.SendHttpPostRequest("settings/root/endurance_limits", limits);

            //        MachineMessageBox.Show(Localization.Warning, Localization.RestartMachineToActivateFunctions);
            //    }
            //}
            //catch
            //{
            //    MachineMessageBox.Show(Localization.Error, Localization.InvalidCode);
            //}
            //GPFx243
        }
    }
}
