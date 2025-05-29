#undef FORMS_CREATE_CONTROL_ON_INITIALIZATION
#undef FORMS_UPDATE_ON_INITIALIZATION
#define FORMS_PRE_JIT_CONTROLS

using System;
using System.Linq;
using System.Text;
using System.Threading;

using Machine.UI.Common;
using Machine.UI.Controls;

namespace Caron.Cradle.UI
{
    public class UiFormsCollection
    {
        public FormMachineBase Transparent { get; set; }

        public FormMachineBase Main { get; set; }
        public FormMachineBase Menu { get; set; }
        public FormMachineBase Actions { get; set; }
        public FormMachineBase TopBar { get; set; }

        public FormMachineBase Dashboard { get; set; }
        public FormMachineBase UserSettings { get; set; }
        public FormMachineBase ManualOperations { get; set; }
        public FormMachineBase RootSettings { get; set; }
        public FormMachineBase WorkingsSettings { get; set; }
        public FormMachineBase LoadUnload { get; set; }
        public FormMachineBase WorkingsStatistics { get; set; }
        public FormMachineBase BroswerInterface { get; set; }
        public FormMachineBase Licenses { get; set; }
        public FormMachineBase Messages { get; set; }
    }

    public partial class Supervisor
    {
        public void SetForms(
           FormMachineBase transparent,

           FormMachineBase main,
           FormMachineBase menu,
           FormMachineBase actions,
           FormMachineBase topBar,

           FormMachineBase dashboard,
           FormMachineBase enableDisableFunctions,
           FormMachineBase manualOperations,
           FormMachineBase rootSettings,
           FormMachineBase userSettings,
           FormMachineBase loadUnload,
           FormMachineBase workingsStatistics,
           FormMachineBase broswerInterface,

           FormMachineBase licenses,
           FormMachineBase messages)
        {
            //-------------------------------
            // Set Forms
            //-------------------------------
            #region Set Forms
            UI.Forms.Transparent = transparent;

            UI.Forms.Main = main;
            UI.Forms.Menu = menu;
            UI.Forms.Actions = actions;
            UI.Forms.TopBar = topBar;

            UI.Forms.Dashboard = dashboard;
            UI.Forms.UserSettings = enableDisableFunctions;
            UI.Forms.ManualOperations = manualOperations;
            UI.Forms.RootSettings = rootSettings;
            UI.Forms.WorkingsSettings = userSettings;
            UI.Forms.LoadUnload = loadUnload;
            UI.Forms.WorkingsStatistics = workingsStatistics;
            UI.Forms.BroswerInterface = broswerInterface;

            UI.Forms.Licenses = licenses;
            UI.Forms.Messages = messages;
            #endregion

            //-------------------------------
            // Set Supervisor
            //-------------------------------
            FormCradleBase.SetSupervisor(this);

            //-------------------------------
            // Complete initialization
            //-------------------------------
            UI.FormsInitilized = true;
        }
    }
}
