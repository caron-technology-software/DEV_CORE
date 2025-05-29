using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Machine.UI;
using Machine.UI.Common;

using Caron.Cradle.Control;
//GPI19
using Caron.Cradle.Control.LowLevel;
using Machine.UI.Communication;
//GPF19

namespace Caron.Cradle.UI
{
    public partial class Supervisor
    {
        public void SetPrecedentUIState()
        {
            SetUIState(UI.PrecedentState);
        }

        public void SetUIState(StateUI stateUI, object parameter = null)
        {
            //-----------------------------------
            // Gestione chiusura forms flottanti
            //-----------------------------------
            switch (UI.State)
            {
                case StateUI.RootSettings:
                    {
                        //---------------------
                        // Logout
                        //---------------------
                        UI.CurrentUserTypeLogged = Machine.Common.UserType.Null;

                        UI.Forms.RootSettings.Visible = false;
                        UI.Forms.Transparent.Visible = false;
                    }
                    break;

                case StateUI.WorkingsStatistics:
                    {
                        UI.Forms.WorkingsStatistics.Visible = false;
                        UI.Forms.Transparent.Visible = false;
                    }
                    break;
            }

            UI.PrecedentState = UI.State;
            UI.State = stateUI;

            //-----------------------------------
            // Gestione forms
            //-----------------------------------
            switch (UI.State)
            {
                case StateUI.Dashboard:
                    {
                        //TopBar
                        FormMachineBase.SetPanelFromBaseMachineForm(UI.Panels.TopBar, UI.Forms.TopBar);

                        //Menu
                        FormMachineBase.SetPanelFromBaseMachineForm(UI.Panels.Menu, UI.Forms.Menu);

                        //Centered
                        FormMachineBase.SetPanelFromBaseMachineForm(UI.Panels.ViewCentered, UI.Forms.Dashboard);
                        //GPI19
                        FormDashboard formDashboard = (FormDashboard)(UI.Forms.Dashboard);
                        //////formDashboard.cbCradleSync.Active = false;
                        bool c1 = Control.HighLevel.WorkingContext.Parameters.PhotocellMaterialPresenceEnabled;
                        bool c2 = Control.LowLevel.IO.DigitalInputs[(byte)DigitalInput.PhotocellMaterialPresence];
                        bool cond = false;
                        if (c1 && c2)
                        {
                            cond = true;
                        }
                        else if (c1 && !c2)
                        {
                            cond = false;

                        }
                        else if (!c1 && c2)
                        {
                            cond = true;
                        }
                        else if (!c1 && !c2)
                        {
                            cond = true;
                        }

                        //MMIx02
                        bool c3 = Control.HighLevel.WorkingContext.Parameters.EnablePhotocellRollPresence;
                        bool c4 = Control.LowLevel.IO.DigitalInputs[(byte)DigitalInput.PhotocellRollPresence];
                        bool cond2 = false;
                        if (c3 && c4)
                        {
                            cond2 = true;
                        }
                        else if (c3 && !c4)
                        {
                            cond2 = false;

                        }
                        else if (!c3 && c4)
                        {
                            cond2 = true;
                        }
                        else if (!c3 && !c4)
                        {
                            cond2 = true;
                        }
                        //MMFx02
                        if (formDashboard.cbCradleSync.Active)
                        {
                            //MMIx02
                            if (cond && cond2)
                            {
                                //sync attivo e fotocellule true
                            }
                            else if (!cond && cond2)
                            {
                                formDashboard.cbCradleSync.Active = false;
                                Communicator.SetVariable($"working_mode/set_cradle_sync", "value", formDashboard.cbCradleSync.Active);
                                Communicator.SendHttpGetRequest("working_mode/cradle_sync/force_disable_is_true");
                            }
                            else if (cond && !cond2)
                            {
                                formDashboard.cbCradleSync.Active = false;
                                Communicator.SetVariable($"working_mode/set_cradle_sync", "value", formDashboard.cbCradleSync.Active);
                                Communicator.SendHttpGetRequest("working_mode/cradle_sync/force_disable_is_true");
                            }
                            else if (!cond && !cond2)
                            {
                                formDashboard.cbCradleSync.Active = false;
                                Communicator.SetVariable($"working_mode/set_cradle_sync", "value", formDashboard.cbCradleSync.Active);
                                Communicator.SendHttpGetRequest("working_mode/cradle_sync/force_disable_is_true");
                            }
                            //MMFx02
                        }
                        else
                        {
                            //Communicator.SetVariable($"working_mode/cradle_sync", "value", formDashboard.cbCradleSync.Active);
                            //Communicator.SendHttpGetRequest("working_mode/cradle_sync/force_disable_is_true");
                        }
                        //GPF19

                        //Form Full
                        FormMachineBase.DisablePanel(UI.Panels.ViewFull);
                    }
                    break;

                case StateUI.UserSettings:
                    {
                        //TopBar
                        FormMachineBase.SetPanelFromBaseMachineForm(UI.Panels.TopBar, UI.Forms.TopBar);

                        //Menu
                        FormMachineBase.SetPanelFromBaseMachineForm(UI.Panels.Menu, UI.Forms.Menu);

                        //Centered
                        FormMachineBase.SetPanelFromBaseMachineForm(UI.Panels.ViewCentered, UI.Forms.UserSettings);

                        //Form Full
                        FormMachineBase.DisablePanel(UI.Panels.ViewFull);
                    }
                    break;

                case StateUI.ManualOperations:
                    {
                        //TopBar
                        FormMachineBase.SetPanelFromBaseMachineForm(UI.Panels.TopBar, UI.Forms.TopBar);

                        //Menu
                        FormMachineBase.SetPanelFromBaseMachineForm(UI.Panels.Menu, UI.Forms.Menu);

                        //Centered
                        FormMachineBase.SetPanelFromBaseMachineForm(UI.Panels.ViewCentered, UI.Forms.ManualOperations);

                        //Form Full
                        FormMachineBase.DisablePanel(UI.Panels.ViewFull);
                    }
                    break;

                case StateUI.LoadUnload:
                    {
                        //TopBar
                        FormMachineBase.SetPanelFromBaseMachineForm(UI.Panels.TopBar, UI.Forms.TopBar);

                        //Menu
                        FormMachineBase.SetPanelFromBaseMachineForm(UI.Panels.Menu, UI.Forms.Menu);

                        //Centered
                        FormMachineBase.SetPanelFromBaseMachineForm(UI.Panels.ViewCentered, UI.Forms.LoadUnload);

                        //Form Full
                        FormMachineBase.DisablePanel(UI.Panels.ViewFull);
                    }
                    break;

                case StateUI.WorkingsSettings:
                    {
                        //TopBar
                        FormMachineBase.SetPanelFromBaseMachineForm(UI.Panels.TopBar, UI.Forms.TopBar);

                        //Menu
                        FormMachineBase.SetPanelFromBaseMachineForm(UI.Panels.Menu, UI.Forms.Menu);

                        //Centered
                        FormMachineBase.SetPanelFromBaseMachineForm(UI.Panels.ViewCentered, UI.Forms.WorkingsSettings);

                        //Form Full
                        FormMachineBase.DisablePanel(UI.Panels.ViewFull);
                    }
                    break;

                case StateUI.BroswerInterface:
                    {
                        //TopBar
                        FormMachineBase.SetPanelFromBaseMachineForm(UI.Panels.TopBar, UI.Forms.TopBar);

                        //Menu
                        FormMachineBase.DisablePanel(UI.Panels.Menu);

                        //Centered
                        FormMachineBase.DisablePanel(UI.Panels.ViewCentered);

                        //Form Full
                        FormMachineBase.SetPanelFromBaseMachineForm(UI.Panels.ViewFull, UI.Forms.BroswerInterface);
                    }
                    break;

                case StateUI.Licenses:
                    {
                        //TopBar
                        FormMachineBase.SetPanelFromBaseMachineForm(UI.Panels.TopBar, UI.Forms.TopBar);

                        //Menu
                        FormMachineBase.SetPanelFromBaseMachineForm(UI.Panels.Menu, UI.Forms.Menu);

                        //Centered
                        FormMachineBase.SetPanelFromBaseMachineForm(UI.Panels.ViewCentered, UI.Forms.Licenses);

                        //Form Full
                        FormMachineBase.DisablePanel(UI.Panels.ViewFull);
                    }
                    break;

                case StateUI.Messages:
                    {
                        //TopBar
                        FormMachineBase.SetPanelFromBaseMachineForm(UI.Panels.TopBar, UI.Forms.TopBar);

                        //Menu
                        FormMachineBase.SetPanelFromBaseMachineForm(UI.Panels.Menu, UI.Forms.Menu);

                        //Centered
                        FormMachineBase.SetPanelFromBaseMachineForm(UI.Panels.ViewCentered, UI.Forms.Messages);

                        //Form Full
                        FormMachineBase.DisablePanel(UI.Panels.ViewFull);
                    }
                    break;

                //-----------------------------------
                // Gestione forms flottanti
                //-----------------------------------
                case StateUI.RootSettings:
                    {
                        if (parameter != null)
                        {
                            UI.Forms.RootSettings.UIStateParameter = parameter;
                        }

                        //---------------------
                        // Login
                        //---------------------
                        UI.CurrentUserTypeLogged = (Machine.Common.UserType)parameter;

                        UI.Forms.Transparent.Visible = true;
                        UI.Forms.RootSettings.ShowAndBringToFront();
                    }
                    break;

                case StateUI.WorkingsStatistics:
                    {
                        UI.Forms.Transparent.Visible = true;
                        UI.Forms.WorkingsStatistics.ShowAndBringToFront();
                    }
                    break;
            }//Swicth(Gui.State)

            //-----------------------------------
            // Gestione pulsanti Menu
            //-----------------------------------
            (UI.Forms.Menu as FormMenu).SetActiveButton(UI.State);
        }
    }
}
