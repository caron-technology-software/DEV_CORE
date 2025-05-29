using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ProRob.Extensions;

using Machine.UI.Controls;
using Machine.UI.Communication;

using Caron.Cradle.Control;
using Caron.Cradle.Control.HighLevel;
using Caron.Cradle.Control.HighLevel.Settings;
using System.Threading;
using ProRob.Extensions.Object;

namespace Caron.Cradle.UI
{
    public partial class FormUserSettings : FormCradleBase
    {
        private readonly List<MachineButtonLabel> buttons = new List<MachineButtonLabel>();

        public FormUserSettings()
        {
            InitializeComponent();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            #region Localization           
            labelTitle.Text = Localization.SetupPage;

            cbEncoderEnabled.PropertyName = Localization.EncoderEnabled;
            cbPhotocellAlignment.PropertyName = Localization.PhotocellAlignment;
            cbDanceBarEnabled.PropertyName = Localization.DanceBarEnabled;
            cbPhotocellMaterialPresence.PropertyName = Localization.PhotocellMaterialPresence;
            //GPIx101 3)
            cbEnablePhotocellRollPresence.PropertyName = Localization.EnablePhotocellRollPresence;
            //GPFx101

            mpPreFeed.PropertyName = Localization.PreFeed;
            #endregion

            #region Buttons
            buttons.Add(cbEncoderEnabled);
            buttons.Add(cbPhotocellAlignment);
            buttons.Add(cbDanceBarEnabled);
            buttons.Add(cbPhotocellMaterialPresence);
            //GPIx101 3)
            buttons.Add(cbEnablePhotocellRollPresence);
            //GPFx101
            #endregion

            #region Cutter Velocity Slider
            mlCutterVelocity.Text = Localization.CutterVelocity;
            mpbsCutterVelocity.PropertyName = Localization.CutterVelocity;
            mpbsCutterVelocity.LoadBackgroundImages(
                global::Caron.Cradle.UI.Properties.Resources.cutter_gray_00,
                new List<Image>()
                {
                    global::Caron.Cradle.UI.Properties.Resources.cutter_green_00,
                    global::Caron.Cradle.UI.Properties.Resources.cutter_green_01,
                    global::Caron.Cradle.UI.Properties.Resources.cutter_green_02,
                    global::Caron.Cradle.UI.Properties.Resources.cutter_green_03,
                    global::Caron.Cradle.UI.Properties.Resources.cutter_green_04,
                    global::Caron.Cradle.UI.Properties.Resources.cutter_green_05,
                    global::Caron.Cradle.UI.Properties.Resources.cutter_green_06,
                    global::Caron.Cradle.UI.Properties.Resources.cutter_green_07,
                    global::Caron.Cradle.UI.Properties.Resources.cutter_green_08,
                    global::Caron.Cradle.UI.Properties.Resources.cutter_green_09,
                    global::Caron.Cradle.UI.Properties.Resources.cutter_green_10,
                });

            mpbsCutterVelocity.MinValue = Supervisor.Control.HighLevel.Settings.HighLevel.MachineParameters.MinCutterVelocity.Value;
            mpbsCutterVelocity.MaxValue = 100;

            mpbsCutterVelocity.ValueChangedEventEnabled = true;
            mpbsCutterVelocity.SetValueWithoutEvent(Supervisor.Control.HighLevel.WorkingContext.Parameters.CutterVelocity);
            mpbsCutterVelocity.SliderExit += CutterVelocityValueChanged;
            #endregion

            #region Functions Enabled
            if (Supervisor.Control.HighLevel.Settings.HighLevel.FunctionsEnabled.CutterPresence.Value == false)
            {
                mlCutterVelocity.Visible = false;
                mpbsCutterVelocity.Visible = false;
            }
            //GPIx101 3)
            if (Supervisor.Control.HighLevel.Settings.HighLevel.FunctionsEnabled.EnableFunctionPhotocellRollPresence.Value == false)
            {
                cbEnablePhotocellRollPresence.Visible = false;
                cbEnablePhotocellRollPresence.Active = false;//MMIx05
            }
            else
            {
                cbEnablePhotocellRollPresence.Visible = true;
            }
            //GPFx101
            #endregion
        }

        private void FormUserSettings_Load(object sender, EventArgs e)
        {
            //-------------------------------------------------------
            // Events UI Update
            //-------------------------------------------------------
            Supervisor.Events.RootSettingsChanged += RootSettingsChanged;
            Supervisor.Events.WorkingsSettingsChanged += WorkingsSettingsChanged;

            //-------------------------------------------------------
            // Events Buttons
            //-------------------------------------------------------
            foreach (var b in buttons)
            {
                b.ActiveChanged += Button_ActiveChanged;
            }

            mpPreFeed.PropertyValue = (int)Supervisor.Control.HighLevel.WorkingContext.Parameters.PreFeedMaterial;
            mpPreFeed.ValueChangedEventEnabled = true;
        }

        private void UpdateMachineContext(WorkingContext wc)
        {
            Communicator.SendHttpPostRequest("working_context", wc);
        }

        private void MpPreFeed_ValueChanged(object sender, MachinePropertyNumericEditBox.MachinePropertyBoxEvent e)
        {
            Console.WriteLine("MpPreFeed_ValueChanged");

            var wc = Supervisor.Control.HighLevel.WorkingContext.Clone();
            int value = (int)e.PropertyValue;

            if (value < 0)
            {
                value = 0;
            }

            wc.Parameters.PreFeedMaterial = (uint)value;

            UpdateMachineContext(wc);
        }

        private void FormUserSettings_Shown(object sender, EventArgs e)
        {
            //
        }
    }
}
