using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using Machine.UI.Controls;
using Machine.UI.Communication;

using Caron.Cradle.Control;
using System.Windows.Forms;

namespace Caron.Cradle.UI
{
    public partial class FormMenu : FormCradleBase
    {
        private const int ChangeUIStateMillisecondsTimeout = 100; //[ms]

        private List<MachineButton> buttons = new List<MachineButton>();

        private int selectedButton = 0;
        public int SelectedButton
        {
            get => selectedButton;
            set
            {
                if (selectedButton != value)
                {
                    selectedButton = value;
                    UpdateUIMenuButtons();
                }
            }
        }

        protected override void UpdateUIForm()
        {
            UpdateUIMenuButtons();
        }

        private void UpdateUIMenuButtons()
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                if (i == selectedButton)
                {
                    buttons[i].Active = true;
                }
                else
                {
                    buttons[i].Active = false;
                }
            }
        }

        public FormMenu()
        {
            InitializeComponent();
        }

        public void SetActiveButton(StateUI state)
        {
            switch (state)
            {
                case StateUI.Dashboard:
                    SelectedButton = 0;
                    break;

                case StateUI.ManualOperations:
                    SelectedButton = 1;
                    break;

                case StateUI.UserSettings:
                    SelectedButton = 2;
                    break;

                case StateUI.WorkingsSettings:
                    SelectedButton = 3;
                    break;

                case StateUI.LoadUnload:
                    SelectedButton = 4;
                    break;
            }
        }

        private void FormMenu_Load(object sender, EventArgs e)
        {
            buttons.Add(mbDashboard);
            buttons.Add(cbManualOperations);
            buttons.Add(cbUserSettings);
            buttons.Add(cbWorkingsSettings);
            buttons.Add(cbLoadUnload);

            //Trick per eliminazione bordo pulsanti, state change
            foreach (var b in buttons)
            {
                b.FlatAppearance.MouseDownBackColor = Machine.UI.Constants.Colors.MenuBackground;
                b.FlatAppearance.BorderColor = Machine.UI.Constants.Colors.MenuBackground;
                b.StateChangeActivated = false;
            }

            #region Machine Configuration (Icons)
            if (Supervisor.Control.HighLevel.Configuration.IsLeftMachine)
            {
                cbLoadUnload.ActiveBackgroundImage = global::Caron.Cradle.UI.Properties.Resources.load_unload_green_SX;
                cbLoadUnload.InactiveBackgroundImage = global::Caron.Cradle.UI.Properties.Resources.load_unload_gray_SX;
            }
            else
            {
                cbLoadUnload.ActiveBackgroundImage = global::Caron.Cradle.UI.Properties.Resources.load_unload_green_DX;
                cbLoadUnload.InactiveBackgroundImage = global::Caron.Cradle.UI.Properties.Resources.load_unload_gray_DX;
            }
            #endregion

            UpdateUIForm();

            //---------------------------------------------
            // Set UI State
            //---------------------------------------------
            Supervisor.SetUIState(StateUI.Dashboard);
        }

        #region Pages
        private void cbDashboard_Click(object sender, EventArgs e)
        {
            if (mbDashboard.Active)
            {
                return;
            }

            if (CheckPageTransition() == false)
            {
                return;
            }

            //-------------------------------------------------
            // Set state control / UI
            //-------------------------------------------------
            Thread.Sleep(ChangeUIStateMillisecondsTimeout);
            Communicator.SetHighLevelControlState("normal");
            Supervisor.SetUIState(StateUI.Dashboard);
        }

        private void cbManualOperations_Click(object sender, EventArgs e)
        {
            if (cbManualOperations.Active)
            {
                return;
            }

            if (CheckPageTransition() == false)
            {
                return;
            }

            //-------------------------------------------------
            // Set state control / UI
            //-------------------------------------------------
            Thread.Sleep(ChangeUIStateMillisecondsTimeout);
            Communicator.SetHighLevelControlState("manual_operations");
            Supervisor.SetUIState(StateUI.ManualOperations);
        }

        private void cbUserSettings_Click(object sender, EventArgs e)
        {
            if (cbUserSettings.Active)
            {
                return;
            }

            if (CheckPageTransition() == false)
            {
                return;
            }

            //-------------------------------------------------
            // Set state control / UI
            //-------------------------------------------------
            Thread.Sleep(ChangeUIStateMillisecondsTimeout);
            Communicator.SetHighLevelControlState("normal");
            Supervisor.SetUIState(StateUI.UserSettings);
        }

        private void cbWorkingsSettings_Click(object sender, EventArgs e)
        {
            if (cbWorkingsSettings.Active)
            {
                return;
            }

            if (CheckPageTransition() == false)
            {
                return;
            }

            //-------------------------------------------------
            // Set state control / UI
            //-------------------------------------------------
            Thread.Sleep(ChangeUIStateMillisecondsTimeout);
            Communicator.SetHighLevelControlState("normal");
            Supervisor.SetUIState(StateUI.WorkingsSettings);
        }

        private void cbLoadUnload_Click(object sender, EventArgs e)
        {
            if (cbLoadUnload.Active)
            {
                return;
            }

            if (CheckPageTransition() == false)
            {
                return;
            }

            //-------------------------------------------------
            // Set state control / UI
            //-------------------------------------------------
            Thread.Sleep(ChangeUIStateMillisecondsTimeout);
            Communicator.SetHighLevelControlState("load_unload");
            Supervisor.SetUIState(StateUI.LoadUnload);
        }
        #endregion

        //------------------------------------------------
        // Functions
        //------------------------------------------------
        private bool CheckPageTransition()
        {
            if (Supervisor.Control.HighLevel.Errors.EtherCat)
            {
                return false;
            }

            if (Supervisor.Control.HighLevel.Errors.EmergencyStatus)
            {
                return false;
            }

#if !TEST
            if (Supervisor.CradleHelper.IsCradleJoggingOrCutting())
            {
                return false;
            }

            if (Supervisor.CradleHelper.CheckIfCutterIsOutOfPositionAndShowPopUp())
            {
                return false;
            }
#endif
            return true;
        }
    }
}
