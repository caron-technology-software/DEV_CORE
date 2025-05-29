using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

using ProRob.Extensions.Collections;
using ProRob.Extensions.Object;

using Machine;
using Machine.UI.Controls;
using Machine.UI.Communication;

using Caron.Cradle.Control.LowLevel;

namespace Caron.Cradle.UI
{
    public partial class FormMachineEndurance : FormCradleBase
    {
        private readonly Point ListBoxLocation = new Point(350, 172);
        private readonly Size ListBoxSize = new Size(645, 527);

        private List<MachineButton> buttons = new List<MachineButton>();

        public FormMachineEndurance()
        {
            InitializeComponent();
        }

        private void DisactiveAllButtons(List<MachineButton> buttons)
        {
            buttons.ForEach(x => x.Active = false);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            var currentUser = Supervisor.UI.CurrentUserTypeLogged;

            //------------------------------------------
            // Localization
            //------------------------------------------
            #region Localization
            labelTitle.Text = Localization.MachineWorkingStatus;

            mlDigitalInputs.Text = Localization.DigitalInputs;
            mlDigitalOutputs.Text = Localization.DigitalOutputs;
            mlWorkingHours.Text = Localization.WorkingHours;
            mlCutter.Text = Localization.Cutter;
            mlStatistics.Text = Localization.MachineStatus;
            #endregion

            //------------------------------------------
            // Buttons
            //------------------------------------------
            #region Buttons
            cbReturn.StateChangeActivated = false;

            buttons.Add(mbDigitalOutputs);
            buttons.Add(mbDigitalInputs);
            buttons.Add(mbWorkingHours);
            buttons.Add(mbCutter);
            buttons.Add(mbStatistics);
            #endregion

            MachineEditableItem item;

            //------------------------------------------
            // DigitalOutput
            //------------------------------------------
            #region DigitalOutput
            int ch = 0;
            listboxDigitalOutputs.Clear();
            Enum.GetNames(typeof(DigitalOutput)).ForEach((x) =>
            {
                int value = (int)Supervisor.Control.HighLevel.MachineEndurance.DigitalOutputsToggles[ch];

                item = new MachineNumericEditableItem()
                {
                    MinValue = (int)uint.MinValue,
                    MaxValue = int.MaxValue,
                    PropertyValue = value,
                    PropertyName = x.Translate(),
                };

                item.SetEditPermission(currentUser, Machine.Common.UserType.Manufacturer);
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
                int value = (int)Supervisor.Control.HighLevel.MachineEndurance.DigitalInputsToggles[ch];

                item = new MachineNumericEditableItem()
                {
                    MinValue = (int)uint.MinValue,
                    MaxValue = int.MaxValue,
                    PropertyValue = value,
                    PropertyName = x.Translate(),
                };

                item.SetEditPermission(currentUser, Machine.Common.UserType.Manufacturer);
                item.MessageBoxText = Localization.WriteAccessNotAllowed;
                listboxDigitalInputs.Add(item);

                ch++;
            });
            #endregion

            //------------------------------------------
            // Machine Working Hours
            //------------------------------------------
            #region Machine Working Hours
            listboxWorkingHours.Clear();

            item = new MachineFloatingEditableItem()
            {
                MinValue = 0,
                MaxValue = int.MaxValue,
                PropertyValue = (float)Supervisor.Control.HighLevel.MachineEndurance.WorkingHours.PowerOnHours,
                PropertyName = nameof(Supervisor.Control.HighLevel.MachineEndurance.WorkingHours.PowerOnHours).Translate(),
            };

            item.SetEditPermission(currentUser, Machine.Common.UserType.Root);
            item.MessageBoxText = Localization.WriteAccessNotAllowed;
            listboxWorkingHours.Add(item);

            item = new MachineFloatingEditableItem()
            {
                MinValue = 0,
                MaxValue = int.MaxValue,
                PropertyValue = (float)Supervisor.Control.HighLevel.MachineEndurance.WorkingHours.WorkingWithCradleInSyncHours,
                PropertyName = nameof(Supervisor.Control.HighLevel.MachineEndurance.WorkingHours.WorkingWithCradleInSyncHours).Translate(),
            };
            item.SetEditPermission(currentUser, Machine.Common.UserType.Root);
            item.MessageBoxText = Localization.WriteAccessNotAllowed;
            listboxWorkingHours.Add(item);

            item = new MachineFloatingEditableItem()
            {
                MinValue = 0,
                MaxValue = int.MaxValue,
                PropertyValue = (float)Supervisor.Control.HighLevel.MachineEndurance.WorkingHours.MachineMaintenanceHours,
                PropertyName = nameof(Supervisor.Control.HighLevel.MachineEndurance.WorkingHours.MachineMaintenanceHours).Translate(),
            };
            item.SetEditPermission(currentUser, Machine.Common.UserType.Manufacturer);
            item.MessageBoxText = Localization.WriteAccessNotAllowed;
            listboxWorkingHours.Add(item);

            if (Supervisor.UI.CurrentUserTypeLogged == Machine.Common.UserType.Root)
            {
                item = new MachineFloatingEditableItem()
                {
                    MinValue = 0,
                    MaxValue = int.MaxValue,
                    PropertyValue = (float)Supervisor.Control.HighLevel.MachineEndurance.WorkingHours.WorkingFakeHours,
                    PropertyName = nameof(Supervisor.Control.HighLevel.MachineEndurance.WorkingHours.WorkingFakeHours).Translate(),
                };
                listboxWorkingHours.Add(item);
            }
            #endregion

            //------------------------------------------
            // Cutter
            //------------------------------------------
            #region Cutter
            listboxCutter.Clear();

            item = new MachineNumericEditableItem()
            {
                MinValue = (int)uint.MinValue,
                MaxValue = int.MaxValue,
                PropertyValue = (int)Supervisor.Control.HighLevel.MachineEndurance.Cutter.NumberOfCutOff,
                PropertyName = nameof(Supervisor.Control.HighLevel.MachineEndurance.Cutter.NumberOfCutOff).Translate(),
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

            item = new MachineNumericEditableItem()
            {
                MinValue = (int)uint.MinValue,
                MaxValue = int.MaxValue,
                PropertyValue = (int)Supervisor.Control.HighLevel.MachineEndurance.Statistics.NumberPowerOn,
                PropertyName = nameof(Supervisor.Control.HighLevel.MachineEndurance.Statistics.NumberPowerOn).Translate(),
            };
            item.SetEditPermission(currentUser, Machine.Common.UserType.Root);
            item.MessageBoxText = Localization.WriteAccessNotAllowed;
            listboxStatistics.Add(item);

            item = new MachineNumericEditableItem()
            {
                MinValue = (int)uint.MinValue,
                MaxValue = int.MaxValue,
                PropertyValue = (int)Supervisor.Control.HighLevel.MachineEndurance.Statistics.NumberPowerOff,
                PropertyName = nameof(Supervisor.Control.HighLevel.MachineEndurance.Statistics.NumberPowerOff).Translate(),
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
        }

        private void FormMachineEndurance_Load(object sender, EventArgs e)
        {
            //Start with..
            mbDigitalOutputs_Click(this, new EventArgs());
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            Refresh();
        }

        private void cbReturn_Click(object sender, EventArgs e)
        {
            cbReturn.PulseButton();
            cbReturn.Active = false;

            var me = Supervisor.Control.HighLevel.MachineEndurance.Clone();

            //--------------------------
            // DigitalOutputsToggles
            //--------------------------
            for (int i = 0; i < me.DigitalOutputsToggles.Count(); i++)
            {
                me.DigitalOutputsToggles[i] = (uint)listboxDigitalOutputs.GetValue(i);
            }

            //--------------------------
            // DigitalInputsToggles
            //--------------------------
            for (int i = 0; i < me.DigitalInputsToggles.Count(); i++)
            {
                me.DigitalInputsToggles[i] = (uint)listboxDigitalInputs.GetValue(i);
            }

            //--------------------------
            // MachineWorkingHours
            //--------------------------
            me.WorkingHours.PowerOnHours = listboxWorkingHours.GetValue(0);
            me.WorkingHours.WorkingWithCradleInSyncHours = listboxWorkingHours.GetValue(1);
            me.WorkingHours.MachineMaintenanceHours = listboxWorkingHours.GetValue(2);

            if (Supervisor.UI.CurrentUserTypeLogged == Machine.Common.UserType.Root)
            {
                me.WorkingHours.WorkingFakeHours = listboxWorkingHours.GetValue(3);
            }

            //--------------------------
            // Cutter
            //--------------------------
            me.Cutter.NumberOfCutOff = (uint)listboxCutter.GetValue(0);

            //--------------------------
            // MachineStatistics
            //--------------------------
            me.Statistics.NumberPowerOn = (uint)listboxStatistics.GetValue(0);
            me.Statistics.NumberPowerOff = (uint)listboxStatistics.GetValue(1);

            //GPIx243
            me.Statistics.EthercatCode = (uint)listboxStatistics.GetValue(2);

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
                var limits = Supervisor.Control.HighLevel.Settings.HighLevel.EnduranceLimits.Clone();
                //var limits = Supervisor.Control.HighLevel.Settings.MachineEnduranceLimits.Clone();
                limits.WorkingHours.WorkingFakeHours = 0;
                //Communicator.SendHttpPostRequest("endurance_limits", limits);
                Communicator.SendHttpPostRequest("settings/root/endurance_limits", limits);
                me.Statistics.EthercatCode = 0;
            }
            if (result == 119) //sblocco parziale
            {
                me.WorkingHours.WorkingFakeHours = 0;
                me.Statistics.EthercatCode = 0;
            }
            //GPfx243

            Communicator.SendHttpPostRequest("endurance", me);

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
