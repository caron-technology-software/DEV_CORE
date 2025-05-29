using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using FontAwesome.Sharp;

using Machine.UI.Controls;
using Machine.UI.Communication;

using Caron.Cradle.Control.HighLevel.Settings;

namespace Caron.Cradle.UI
{
    public partial class FormWorkingsSettings : FormCradleBase
    {
        private const int CellWidth = 465;
        private const int CellHeight = 48;
        private const int CellsNumberInTable = 9;
        private const int ButtonArrowSize = 105;

        public FormWorkingsSettings()
        {
            InitializeComponent();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            #region Buttons
            clSettings.ItemSelected += WorkingSettingSelected;

            mbApply.StateChangeActivated = false;
            mbNew.StateChangeActivated = false;
            mbDelete.StateChangeActivated = false;
            mbSave.StateChangeActivated = false;
            mbSaveWithName.StateChangeActivated = false;
            mbRename.StateChangeActivated = false;

            mbUp.StateChangeActivated = false;
            mbUp.InactiveBackgroundImage = IconChar.ArrowCircleUp.ToBitmap(Color.Gray, ButtonArrowSize);
            mbUp.ActiveBackgroundImage = IconChar.ArrowCircleUp.ToBitmap(Color.Gray, ButtonArrowSize);

            mbDown.StateChangeActivated = false;
            mbDown.InactiveBackgroundImage = IconChar.ArrowCircleDown.ToBitmap(Color.Gray, ButtonArrowSize);
            mbDown.ActiveBackgroundImage = IconChar.ArrowCircleDown.ToBitmap(Color.Gray, ButtonArrowSize);
            #endregion

            #region Listbox
            var settingListBoxSize = new Size(CellWidth, CellHeight * CellsNumberInTable);
            clSettings.Size = settingListBoxSize;
            clSettings.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            clSettings.BorderStyle = BorderStyle.None;
            #endregion
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            if (Visible)
            {
                //Machine.UI.FormHelper.FadeInEffect(this.Handle, TimeSpan.FromMilliseconds(150));
                clSettings.Refresh();
            }
            else
            {
                //Machine.UI.FormHelper.FadeOutEffect(this.Handle, TimeSpan.FromMilliseconds(50));
            }
        }

        private void FormWorkingsSettingsManager_Load(object sender, EventArgs e)
        {
            //-------------------------------------------------------
            // Events UI Update
            //-------------------------------------------------------
            Supervisor.Events.WorkingsSettingsChanged += WorkingsSettingsChanged;
        }

        private void WorkingsSettingsChanged(object sender, EventArgs e)
        {
            Console.WriteLine("WorkingsSettingsChanged");

            UpdateUIForm();
        }

        private void WorkingSettingSelected(object sender, EventArgs e)
        {
            Console.WriteLine($"WorkingSettingSelected({clSettings.LastSelectedControlIndex})");

            int index = clSettings.LastSelectedControlIndex;

            if (index >= 0)
            {
                var settings = Supervisor.Control.HighLevel.WorkingsSettings.Items.ElementAt(index);
                UpdatePanelShowSettings(settings);
            }
        }

        private void MbUp_Click(object sender, EventArgs e)
        {
            mbUp.PulseButton();

            int index = clSettings.LastSelectedControlIndex - CellsNumberInTable;
            index = CheckIndex(index);
            clSettings.ScrollPanelTo(index);

            Console.WriteLine($"ScrollPanelTo:{index}");
        }

        private void MbDown_Click(object sender, EventArgs e)
        {
            mbDown.PulseButton();

            int index = clSettings.LastSelectedControlIndex + CellsNumberInTable;
            index = CheckIndex(index);
            clSettings.ScrollPanelTo(index);

            Console.WriteLine($"ScrollPanelTo:{index}");
        }

        private int CheckIndex(int index)
        {
            if (index < 0)
            {
                index = 0;
            }
            else if (index >= clSettings.NumberOfElements)
            {
                index = clSettings.NumberOfElements - 1;
            }

            return index;
        }

        private void MbNew_Click(object sender, EventArgs e)
        {
            mbNew.PulseButton();

            Communicator.SendHttpGetRequest($"workings_settings/add_default");
        }

        private void MbApply_Click(object sender, EventArgs e)
        {
            mbApply.PulseButton();

            int index = clSettings.LastSelectedControlIndex;
            var name = clSettings.GetControl(index)?.PropertyValue;
            Communicator.SendHttpGetRequest($"workings_settings", $"apply?name={name}");
        }
        private void MbSave_Click(object sender, EventArgs e)
        {
            mbSave.PulseButton();

            Communicator.SendHttpGetRequest("workings_settings/save");
        }

        private void MbSaveWithName_Click(object sender, EventArgs e)
        {
            mbSaveWithName.PulseButton();

            string name = String.Empty;

            using (var keyb = new TouchAlphaNumericKeyboard(Localization.SaveWithName, ""))
            {
                keyb.ShowDialog();

                name = keyb.StringValue;
            }

            var workingSetting = new WorkingSetting()
            {
                Guid = Guid.NewGuid(),
                Name = name,
                Timestamp = DateTime.Now,
                Parameters = Supervisor.Control.HighLevel.WorkingContext.Parameters
            };

            Communicator.SendHttpPostRequest("workings_settings/add", workingSetting);
        }

        private void MbDelete_Click(object sender, EventArgs e)
        {
            mbDelete.PulseButton();

            if (clSettings.NumberOfElements == 1)
            {
                MachineMessageBox.Show(Localization.Warning, Localization.DatabaseCannotBeCompletelyDeleted);
                return;
            }

            int index = clSettings.LastSelectedControlIndex;

            if (index >= 0)
            {
                var name = clSettings.GetControl(index)?.PropertyValue;

                clSettings.DeleteItem(index);
                Communicator.SendHttpGetRequest($"workings_settings", $"delete?name={name}");
            }
        }

        private void MbRename_Click(object sender, EventArgs e)
        {
            mbRename.PulseButton();

            int index = clSettings.LastSelectedControlIndex;
            var name = clSettings.GetControl(index)?.PropertyValue;

            clSettings.GetControl(index)?.HandlePropertyValue();

            var newName = clSettings.GetControl(index)?.PropertyValue;

            var res = Communicator.SendHttpGetRequest($"workings_settings", $"rename?name={name}&new_name={newName}");

            if (res == "false")
            {
                MachineMessageBox.Show(Localization.Warning, Localization.DatabaseItemsCannotHaveSameNames);
            }
        }
    }
}
