using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ProRob.Extensions.Object;
using ProRob.Extensions.Collections;

using Machine;
using Machine.UI.Common;
using Machine.UI.Controls;
using Machine.UI.Communication;

using Caron.Cradle.Control.LowLevel;

namespace Caron.Cradle.UI
{
    public partial class FormMachineEnduranceLimits : FormCradleBase
    {
        private readonly Point ListBoxLocation = new Point(350, 172);
        private readonly Size ListBoxSize = new Size(645, 527);

        private List<MachineButton> buttons = new List<MachineButton>();

        public FormMachineEnduranceLimits()
        {
            InitializeComponent();
        }

        private void DisactiveAllButtons(List<MachineButton> buttons)
        {
            buttons.ForEach(x => x.Active = false);
        }

        private void FormMachineEnduranceSettings_Load(object sender, EventArgs e)
        {
            mbReturn.StateChangeActivated = false;

            labelTitle.Text = Localization.MachineWorkingStatusSettings;

            mlDigitalInputs.Text = Localization.DigitalInputs;
            mlDigitalOutputs.Text = Localization.DigitalOutputs;
            mlWorkingHours.Text = Localization.WorkingHours;
            mlCutter.Text = Localization.Cutter;
            mlStatistics.Text = Localization.MachineStatus;

            buttons.Add(mbDigitalOutputs);
            buttons.Add(mbDigitalInputs);
            buttons.Add(mbWorkingHours);
            buttons.Add(mbCutter);
            buttons.Add(mbStatistics);

            var currentUser = Supervisor.UI.CurrentUserTypeLogged;

            MachineEditableItem item;

            var limits = Supervisor.Control.HighLevel.Settings.HighLevel.EnduranceLimits.Clone();

            //------------------------------------------
            // DigitalOutput
            //------------------------------------------
            #region DigitalOutput
            int ch = 0;
            listboxDigitalOutputs.Clear();
            Enum.GetNames(typeof(DigitalOutput)).ForEach((x) =>
            {
                item = new MachineNumericEditableItem()
                {
                    MinValue = (int)uint.MinValue,
                    MaxValue = int.MaxValue,
                    PropertyValue = (int)limits.DigitalOutputsToggles[ch],
                    PropertyName = x.Translate(),
                };

                item.SetEditPermission(currentUser, Machine.Common.UserType.Root);
                item.MessageBoxText = Localization.WriteAccessNotAllowed;
                listboxDigitalOutputs.Add(item);

                ch++;
            });
            #endregion

            //------------------------------------------
            // DigitalInput
            //------------------------------------------
            #region DigitalInput
            ch = 0;
            listboxDigitalInputs.Clear();
            Enum.GetNames(typeof(DigitalInput)).ForEach((x) =>
            {
                item = new MachineNumericEditableItem()
                {
                    MinValue = (int)uint.MinValue,
                    MaxValue = int.MaxValue,
                    PropertyValue = (int)limits.DigitalInputsToggles[ch],
                    PropertyName = x.Translate(),
                };

                item.SetEditPermission(currentUser, Machine.Common.UserType.Root);
                item.MessageBoxText = Localization.WriteAccessNotAllowed;
                listboxDigitalInputs.Add(item);

                ch++;
            });
            #endregion

            //------------------------------------------
            // Working Hours
            //------------------------------------------
            #region Working Hours
            listboxWorkingHours.Clear();

            //------------------------------------------
            // Item
            //------------------------------------------
            item = new MachineFloatingEditableItem()
            {
                MinValue = 0,
                MaxValue = 1_000_000,
                PropertyValue = (float)limits.WorkingHours.PowerOnHours,
                PropertyName = nameof(limits.WorkingHours.PowerOnHours).Translate(),
            };

            item.SetEditPermission(currentUser, Machine.Common.UserType.Root);
            item.MessageBoxText = Localization.WriteAccessNotAllowed;
            listboxWorkingHours.Add(item);

            //------------------------------------------
            // Item
            //------------------------------------------
            item = new MachineFloatingEditableItem()
            {
                MinValue = 0,
                MaxValue = 1_000_000,
                PropertyValue = (float)limits.WorkingHours.WorkingWithCradleInSyncHours,
                PropertyName = nameof(limits.WorkingHours.WorkingWithCradleInSyncHours).Translate(),
            };
            item.SetEditPermission(currentUser, Machine.Common.UserType.Root);
            item.MessageBoxText = Localization.WriteAccessNotAllowed;
            listboxWorkingHours.Add(item);

            //------------------------------------------
            // Item
            //------------------------------------------   
            item = new MachineFloatingEditableItem()
            {
                MinValue = 0,
                MaxValue = 1_000_000,
                PropertyValue = (float)limits.WorkingHours.MachineMaintenanceHours,
                PropertyName = nameof(limits.WorkingHours.MachineMaintenanceHours).Translate(),
            };
            item.SetEditPermission(currentUser, Machine.Common.UserType.Root);
            item.MessageBoxText = Localization.WriteAccessNotAllowed;
            listboxWorkingHours.Add(item);

            //------------------------------------------
            // Item
            //------------------------------------------
            if (Supervisor.UI.CurrentUserTypeLogged == Machine.Common.UserType.Root)
            {
                item = new MachineFloatingEditableItem()
                {
                    MinValue = 0,
                    MaxValue = 1_000_000,
                    PropertyValue = (float)limits.WorkingHours.WorkingFakeHours,
                    PropertyName = nameof(limits.WorkingHours.WorkingFakeHours).Translate(),
                };
                listboxWorkingHours.Add(item);
            }
            #endregion

            //------------------------------------------
            // Cutter
            //------------------------------------------
            #region Cutter
            listboxCutter.Clear();

            //------------------------------------------
            // Item
            //------------------------------------------
            item = new MachineNumericEditableItem()
            {
                MinValue = (int)uint.MinValue,
                MaxValue = int.MaxValue,
                PropertyValue = (int)limits.Cutter.NumberOfCutOff,
                PropertyName = nameof(limits.Cutter.NumberOfCutOff).Translate(),
            };
            item.SetEditPermission(currentUser, Machine.Common.UserType.Root);
            item.MessageBoxText = Localization.WriteAccessNotAllowed;
            listboxCutter.Add(item);

            #endregion

            //------------------------------------------
            // Statistics
            //------------------------------------------
            #region Statistics
            listboxStatistics.Clear();

            //------------------------------------------
            // Item
            //------------------------------------------
            item = new MachineNumericEditableItem()
            {
                MinValue = (int)uint.MinValue,
                MaxValue = int.MaxValue,
                PropertyValue = (int)limits.Statistics.NumberPowerOn,
                PropertyName = nameof(limits.Statistics.NumberPowerOn).Translate(),
            };
            item.SetEditPermission(currentUser, Machine.Common.UserType.Root);
            item.MessageBoxText = Localization.WriteAccessNotAllowed;
            listboxStatistics.Add(item);

            //------------------------------------------
            // Item
            //------------------------------------------
            item = new MachineNumericEditableItem()
            {
                MinValue = (int)uint.MinValue,
                MaxValue = int.MaxValue,
                PropertyValue = (int)limits.Statistics.NumberPowerOff,
                PropertyName = nameof(limits.Statistics.NumberPowerOff).Translate(),
            };
            item.SetEditPermission(currentUser, Machine.Common.UserType.Root);
            item.MessageBoxText = Localization.WriteAccessNotAllowed;
            listboxStatistics.Add(item);

            //GPIx243
            item = new MachineNumericEditableItem()
            {
                MinValue = (int)uint.MinValue,
                MaxValue = int.MaxValue,
                PropertyValue = (int)Supervisor.Control.HighLevel.MachineEndurance.Statistics.EthercatCode,
                PropertyName = nameof(Supervisor.Control.HighLevel.MachineEndurance.Statistics.EthercatCode).Translate(),
            };
            item.SetEditPermission(currentUser, Machine.Common.UserType.Root);
            item.SetEditPermission(currentUser, Machine.Common.UserType.Manufacturer);
            item.MessageBoxText = Localization.WriteAccessNotAllowed;
            listboxStatistics.Add(item);
            //GPFx243
            #endregion

            //------------------------------------------
            // UI
            //------------------------------------------
            listboxDigitalOutputs.Visible = false;
            listboxDigitalInputs.Visible = false;
            listboxWorkingHours.Visible = false;
            listboxCutter.Visible = false;
            listboxStatistics.Visible = false;

            listboxDigitalOutputs.Location = ListBoxLocation;
            listboxDigitalOutputs.Size = ListBoxSize;

            listboxDigitalInputs.Location = ListBoxLocation;
            listboxDigitalInputs.Size = ListBoxSize;

            listboxWorkingHours.Location = ListBoxLocation;
            listboxWorkingHours.Size = ListBoxSize;

            listboxCutter.Location = ListBoxLocation;
            listboxCutter.Size = ListBoxSize;

            listboxStatistics.Location = ListBoxLocation;
            listboxStatistics.Size = ListBoxSize;

            //Start with..
            mbDigitalOutputs_Click(this, new EventArgs());
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            Refresh();
        }

        private void cbReturn_Click(object sender, EventArgs e)
        {
            mbReturn.PulseButton();
            mbReturn.Active = false;

            var limits = Supervisor.Control.HighLevel.Settings.HighLevel.EnduranceLimits.Clone();

            for (int i = 0; i < limits.DigitalOutputsToggles.Count(); i++)
            {
                limits.DigitalOutputsToggles[i] = (uint)listboxDigitalOutputs.GetValue(i);
            }

            for (int i = 0; i < limits.DigitalInputsToggles.Count(); i++)
            {
                limits.DigitalInputsToggles[i] = (uint)listboxDigitalInputs.GetValue(i);
            }

            //--------------------------
            // MachineWorkingHours
            //--------------------------
            limits.WorkingHours.PowerOnHours = listboxWorkingHours.GetValue(0);
            limits.WorkingHours.WorkingWithCradleInSyncHours = listboxWorkingHours.GetValue(1);
            limits.WorkingHours.MachineMaintenanceHours = listboxWorkingHours.GetValue(2);

            if (Supervisor.UI.CurrentUserTypeLogged == Machine.Common.UserType.Root)
            {
                limits.WorkingHours.WorkingFakeHours = listboxWorkingHours.GetValue(3);
            }

            //--------------------------
            // Cutter
            //--------------------------
            limits.Cutter.NumberOfCutOff = (uint)listboxCutter.GetValue(0);

            //--------------------------
            // MachineStatistics
            //--------------------------
            limits.Statistics.NumberPowerOn = (uint)listboxStatistics.GetValue(0);
            limits.Statistics.NumberPowerOff = (uint)listboxStatistics.GetValue(1);

            //GPIx243
            limits.Statistics.EthercatCode = (uint)listboxStatistics.GetValue(2);

            //mi stanno chiudendo la form quindi vedo se mi hanno messo il codice di sblocco:
            int result = 0;
            int varCodeAll = Supervisor.Control.HighLevel.WorkingsSettings.GeneratedEthercatErrorAtStart;
            int varCodeKey = listboxStatistics.GetValue(2);
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


            if (result == 115) //sblocco totale
            {
                limits.WorkingHours.WorkingFakeHours = 0;
                limits.Statistics.EthercatCode = 0;
            }
            if (result == 119) //sblocco totale
            {
                var me = Supervisor.Control.HighLevel.MachineEndurance.Clone();
                me.WorkingHours.WorkingFakeHours = 0;
                Communicator.SendHttpPostRequest("endurance", me);
                limits.Statistics.EthercatCode = 0;
            }

            //GPFx243

            Communicator.SendHttpPostRequest("settings/root/endurance_limits", limits);
            Close();
        }

        private void mbDigitalOutputs_Click(object sender, EventArgs e)
        {
            DisactiveAllButtons(buttons);
            mbDigitalOutputs.Active = true;

            listboxDigitalOutputs.Visible = true;
            listboxDigitalInputs.Visible = false;
            listboxWorkingHours.Visible = false;
            listboxCutter.Visible = false;
            listboxStatistics.Visible = false;
        }

        private void mbDigitalInputs_Click(object sender, EventArgs e)
        {
            DisactiveAllButtons(buttons);
            mbDigitalInputs.Active = true;

            listboxDigitalOutputs.Visible = false;
            listboxDigitalInputs.Visible = true;
            listboxWorkingHours.Visible = false;
            listboxCutter.Visible = false;
            listboxStatistics.Visible = false;
        }

        private void mbWorkingHours_Click(object sender, EventArgs e)
        {
            DisactiveAllButtons(buttons);
            mbWorkingHours.Active = true;

            listboxDigitalOutputs.Visible = false;
            listboxDigitalInputs.Visible = false;
            listboxWorkingHours.Visible = true;
            listboxCutter.Visible = false;
            listboxStatistics.Visible = false;
        }

        private void mbCutter_Click(object sender, EventArgs e)
        {
            DisactiveAllButtons(buttons);
            mbCutter.Active = true;

            listboxDigitalOutputs.Visible = false;
            listboxDigitalInputs.Visible = false;
            listboxWorkingHours.Visible = false;
            listboxCutter.Visible = true;
            listboxStatistics.Visible = false;
        }

        private void mbStatistics_Click(object sender, EventArgs e)
        {
            DisactiveAllButtons(buttons);

            mbStatistics.Active = true;
            listboxDigitalOutputs.Visible = false;
            listboxDigitalInputs.Visible = false;
            listboxWorkingHours.Visible = false;
            listboxCutter.Visible = false;
            listboxStatistics.Visible = true;
        }
    }
}
