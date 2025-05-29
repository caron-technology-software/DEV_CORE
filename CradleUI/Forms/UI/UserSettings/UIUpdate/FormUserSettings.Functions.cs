using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ProRob.Extensions.Object;

using Machine.UI.Controls;
using Machine.UI.Communication;

using Caron.Cradle.Control.HighLevel;

namespace Caron.Cradle.UI
{
    public partial class FormUserSettings : FormCradleBase
    {
        protected override void UpdateUIForm()
        {
            UpdateUIControls();
        }

        private void UpdateUIControls()
        {
            Console.WriteLine($"[FormEnableDisableSettings] UpdateUIControls()");

            try
            {
                this?.Invoke((MethodInvoker)delegate ()
                {
                    //---------------------------
                    // Buttons
                    //---------------------------
                    SetButtonsStatus();

                    //---------------------------
                    // Cutter Velocity
                    //---------------------------
                    float velocity = (float)(Supervisor.Control.HighLevel.WorkingContext.Parameters.CutterVelocity * 100.0);
                    mpbsCutterVelocity.SetValueWithoutEvent(velocity);

                    //---------------------------
                    // Pre Feed
                    //---------------------------
                    mpPreFeed.SetValueWithoutEvent((int)Supervisor.Control.HighLevel.WorkingContext.Parameters.PreFeedMaterial);

                    //---------------------------
                    // Unavailable settings
                    //---------------------------
                    var fe = Supervisor.Control.HighLevel.Settings.HighLevel.FunctionsEnabled;

                    if (fe.EnabledEncoder.Value == false)
                    {
                        cbEncoderEnabled.SetButtonInactiveAndUnavailable();
                        cbDanceBarEnabled.SetButtonActiveAndUnavailable();
                    }
                });
            }
            catch
            {
                //--
            }
        }

        private void SetButtonsStatus()
        {
            cbEncoderEnabled.SetDefaultBackgroundImages();
            cbPhotocellAlignment.SetDefaultBackgroundImages();
            cbDanceBarEnabled.SetDefaultBackgroundImages();
            cbPhotocellMaterialPresence.SetDefaultBackgroundImages();
            //GPIx101 3)
            cbEnablePhotocellRollPresence.SetDefaultBackgroundImages();
            //GPFx101

            cbEncoderEnabled.Enabled = true;
            cbPhotocellAlignment.Enabled = true;
            cbDanceBarEnabled.Enabled = true;
            cbPhotocellMaterialPresence.Enabled = true;
            //GPIx101 3)
            cbEnablePhotocellRollPresence.Enabled = true;
            //GPFx101

            var wc = Supervisor.Control.HighLevel.WorkingContext.Parameters.Clone();

            switch (wc.WorkingMode)
            {
                case Control.HighLevel.WorkingMode.Encoder:
                    cbEncoderEnabled.SetActiveWithoutEvent(true);
                    cbDanceBarEnabled.SetActiveWithoutEvent(false);
                    break;

                case Control.HighLevel.WorkingMode.EncoderDancebar:
                    cbEncoderEnabled.SetActiveWithoutEvent(true);
                    cbDanceBarEnabled.SetActiveWithoutEvent(true);
                    break;

                case Control.HighLevel.WorkingMode.Dancebar:
                    cbEncoderEnabled.SetActiveWithoutEvent(false);
                    cbDanceBarEnabled.SetActiveWithoutEvent(true);
                    break;
            }

            cbPhotocellAlignment.SetActiveWithoutEvent(wc.PhotocellAlignmentEnabled);
            cbPhotocellMaterialPresence.SetActiveWithoutEvent(wc.PhotocellMaterialPresenceEnabled);
            //GPIx101 3)
            cbEnablePhotocellRollPresence.SetActiveWithoutEvent(wc.EnablePhotocellRollPresence);
            //GPFx101
        }

        private void SetSettingsChanges()
        {
            var parameters = Supervisor.Control.HighLevel.WorkingContext.Parameters.Clone();

            bool ee = cbEncoderEnabled.Active;
            bool de = cbDanceBarEnabled.Active;

            WorkingMode workingMode = WorkingMode.Dancebar;

            if (ee && de)
            {
                workingMode = WorkingMode.EncoderDancebar;
            }
            else if (ee && !de)
            {
                workingMode = WorkingMode.Encoder;
            }
            else if (!ee && de)
            {
                workingMode = WorkingMode.Dancebar;
            }

            parameters.WorkingMode = workingMode;
            parameters.PhotocellAlignmentEnabled = cbPhotocellAlignment.Active;
            parameters.PhotocellMaterialPresenceEnabled = cbPhotocellMaterialPresence.Active;
            //GPIx101 3)
            parameters.EnablePhotocellRollPresence = cbEnablePhotocellRollPresence.Active;
            //GPFx101

            Communicator.SendHttpPostRequest("working_context/parameters", parameters);
        }
    }
}