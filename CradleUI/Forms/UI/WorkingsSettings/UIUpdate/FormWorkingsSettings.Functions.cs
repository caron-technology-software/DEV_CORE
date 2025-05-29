using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Machine;
using Machine.UI.Controls;

using Caron.Cradle.Control.HighLevel;
using Caron.Cradle.Control.HighLevel.Settings;

namespace Caron.Cradle.UI
{
    public partial class FormWorkingsSettings : FormCradleBase
    {
        protected override void UpdateUIForm()
        {
            Console.WriteLine("[FormWorkingsSettingsManager] UpdateUIForm");

            UpdateUIControls();
        }

        private void UpdateUIControls()
        {
            Console.WriteLine("[FormWorkingsSettingsManager] UpdateUIControls()");

            this?.Invoke((MethodInvoker)delegate ()
            {
                try
                {
                    RefreshItemsToListbox();
                    UpdatePanelShowSettings();
                    UpdateLocalizations();
                    UpdatePanelCurrentSettings();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"[EXCEPTION] message:{e.Message} source:{e.Source}");
                }
            });
        }

        public void UpdatePanelShowSettings()
        {
            int index = clSettings.LastSelectedControlIndex;

            if (index >= 0)
            {
                var settings = Supervisor.Control.HighLevel.WorkingsSettings.Items.ElementAt(index);
                UpdatePanelShowSettings(settings);
            }
        }

        public void UpdatePanelShowSettings(WorkingSetting setting)
        {
            var p = setting.Parameters;

            mlSelectedWorkingsSettingsVisualization.Text = $"{Localization.SelectedParametersVisualization}";

            mlDate.Text = setting.Timestamp.ToString();
            mlWorkingMode.Text = $"{Localization.WorkingMode}: {(((WorkingMode)p.WorkingMode).Translate())}";
            mlCutterVelocity.Text = $"{Localization.CutterVelocity}: {(p.CutterVelocity * 100.0).ToString("0.0")} %";
            mlCradleVelocity.Text = $"{Localization.CradleVelocity}: {((p.CradleScalingFactor - 1.0) * 100.0).ToString("0.0")} %";

            mlPreFeed.Text = $"{Localization.PreFeed}: {p.PreFeedMaterial: 0} mm";

            string loc = p.StraightRoller ? "Straight" : "Reverse";
            mlStraightRollerUp.Text = $"{Localization.StraightRollerUp}: {loc.Translate()}";

            mlPhotocellAlignment.Text = $"{Localization.PhotocellAlignment}: {p.PhotocellAlignmentEnabled.Translate()}";
            mlPhotocellMaterialPresence.Text = $"{Localization.PhotocellMaterialPresence}: {p.PhotocellMaterialPresenceEnabled.Translate()}";
            //mlPhotocellRollPresence.Text = $"{Localization.CheckUntilPhotocelRollPresence}: {p.EnablePhotocellRollPresence.Translate()}";//MMIx05
        }

        public void UpdatePanelCurrentSettings()
        {
            var p = Supervisor.Control.HighLevel.WorkingContext.Parameters;

            mlCurrentParameters.Text = $"{Localization.CurrentParameters}";

            mlCurrentWorkingMode.Text = $"{Localization.WorkingMode}: {(((WorkingMode)p.WorkingMode).Translate())}";
            mlCurrentCutterVelocity.Text = $"{Localization.CutterVelocity}: {(p.CutterVelocity * 100.0).ToString("0.0")} %";
            mlCurrentCradleVelocity.Text = $"{Localization.CradleVelocity}: {((p.CradleScalingFactor - 1.0) * 100.0).ToString("0.0")} %";

            mlCurrentPreFeed.Text = $"{Localization.PreFeed}: {p.PreFeedMaterial: 0} mm";

            string loc = p.StraightRoller ? "Straight" : "Reverse";
            mlCurrentStraightRollerUp.Text = $"{Localization.StraightRollerUp}: {loc.Translate()}";

            mlCurrentPhotocellAlignment.Text = $"{Localization.PhotocellAlignment}: {p.PhotocellAlignmentEnabled.Translate()}";
            mlCurrentPhotocellMaterialPresence.Text = $"{Localization.PhotocellMaterialPresence}: {p.PhotocellMaterialPresenceEnabled.Translate()}";
            //mlCurrentPhotocellRollPresence.Text = $"{Localization.CheckUntilPhotocelRollPresence}: {p.EnablePhotocellRollPresence.Translate()}";//MMIx05
        }

        private void RefreshItemsToListbox()
        {
            var settings = Supervisor.Control.HighLevel.WorkingsSettings.Items;
            var items = settings.Select(x => new MachineStringEditableItem() { PropertyValue = x.Name });

            var workingsSettings = Supervisor.Control.HighLevel.WorkingsSettings;
            var context = Supervisor.Control.HighLevel.WorkingContext;
            int index = workingsSettings.Items.FindIndex(x => x.Guid == context.CurrentGuidWorkingParameterSet);

            clSettings.RefreshItems(items.ToList(), index);
        }

        public void UpdateLocalizations()
        {
            mlApply.Text = Localization.Apply;
            mlSave.Text = Localization.Save;
            mlSaveWithName.Text = Localization.SaveWithName;
            mlRename.Text = Localization.Rename;
            mlNew.Text = Localization.New;
            mlDelete.Text = Localization.Delete;
        }
    }
}
