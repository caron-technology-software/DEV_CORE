using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using ProRob;

using Caron.Cradle.Control;
using Caron.Cradle.Control.LowLevel;
using Caron.Cradle.Control.HighLevel.StateMachine;

namespace Caron.Cradle.Control.HighLevel
{
    public partial class MachineController
    {
        public void TaskStateNormal(CancellationToken cancellationToken)
        {
            //-------------------------------------
            // RegisterCurrentThread
            //-------------------------------------
            while (StateMachine is null)
            {
                Thread.Sleep(Machine.Constants.Intervals.HighLevelControlCycle);
            }

            StateMachineHelper.RegisterCurrentThread(StateMachine, Thread.CurrentThread);

            //-------------------------------------
            // Task
            //-------------------------------------
            ProConsole.WriteLine("[ENTERING] TaskStateNormal", ConsoleColor.Green);
            Thread.CurrentThread.Priority = ThreadPriority.AboveNormal;

            bool promiseToEnableCradleSyncForMaterial = false;
            DateTime timestampCradleSyncEnabled = DateTime.UtcNow;

            bool promiseToDisableCradleSyncForMaterial = false;
            DateTime timestampCradleSyncDisabled = DateTime.UtcNow;

            //MMIx02
            bool promiseToEnableCradleSyncForRoll = false;
            bool promiseToDisableCradleSyncForRoll = false;
            //MMFx02

#pragma warning disable CS0219
            const float MinimumVelocityToConsiderDeviceInMovement = Machine.Constants.Kinematics.MinVelocityToConsiderDeviceInMovement;
            const float MinimumVelocityToConsiderDeviceStationary = Machine.Constants.Kinematics.MaxVelocityToConsiderDeviceStationary;
#pragma warning restore CS0219

#pragma warning disable CS0219
            bool pmpEnabled = false;
            bool pmp = false;
            bool sync = false;
            bool precPmp = HighLevel.Status.PhotocelMaterialPresenceFiltered;
            //GPF25
            //MMIx02
            bool prpEnabled = false;
            bool prp = false;
            bool precPrp = HighLevel.Status.PhotocelRollPresenceFiltered;
            //MMFx02
#pragma warning restore CS0219

            while (!cancellationToken.IsCancellationRequested)
            {
                pmpEnabled = HighLevel.WorkingContext.Parameters.PhotocellMaterialPresenceEnabled;
                pmp = HighLevel.Status.PhotocelMaterialPresenceFiltered;
                //GPF25

                //MMIx02
                prpEnabled = HighLevel.WorkingContext.Parameters.EnablePhotocellRollPresence && HighLevel.Settings.HighLevel.FunctionsEnabled.EnableFunctionPhotocellRollPresence.Value;//MMIx05
                prp = HighLevel.Status.PhotocelRollPresenceFiltered;
                //MMFx02

                //---------------------------------------------------------
                // Gestione Spoon
                //---------------------------------------------------------
                if (Math.Abs(LowLevel.Axes.Cradle.Velocity) > MinimumVelocityToConsiderDeviceStationary)
                {
                    if (HighLevel.Status.CradleInSync)
                    {
                        if (LowLevel.IO.DigitalInputs[(byte)DigitalInput.LimitSpoonUp] == false && LowLevel.Actions.SpoonUp == false)
                        {
                            Devices.ElectricDrives.SpoonUp();
                        }
                    }
                }

                //---------------------------------------------------------
                // Gestione Sync (Presenza materiale)
                //---------------------------------------------------------
                if (HighLevel.Status.JogState == JogState.Stopped)
                {
                    if (pmpEnabled)
                    {
                        if (pmp && !precPmp)
                        {
                            timestampCradleSyncEnabled = DateTime.UtcNow;
                            promiseToEnableCradleSyncForMaterial = true;
                        }
                        else if (!pmp && precPmp)
                        {
                            timestampCradleSyncDisabled = DateTime.UtcNow;
                            promiseToDisableCradleSyncForMaterial = true;
                        }else if (!pmp && !precPmp)//MMIx02
                        {
                            promiseToDisableCradleSyncForMaterial = true;
                        }
                    }

                    //MMIx02
                    if (prpEnabled)
                    {
                        if (prp && !precPrp)
                        {
                            promiseToEnableCradleSyncForRoll = true;
                        }
                        else if (!prp && precPrp)
                        {
                            promiseToDisableCradleSyncForRoll = true;
                        }
                        else if (!prp && !precPrp)
                        {
                            promiseToDisableCradleSyncForRoll = true;
                        }
                    }

                    precPmp = pmp;
                    precPrp = prp;
                    var checkPromise = false;

                    if (promiseToEnableCradleSyncForMaterial)
                    {
                        promiseToEnableCradleSyncForMaterial = false;

                        Devices.Cradle.SetSync(true);
                    }

                    if (promiseToDisableCradleSyncForMaterial)
                    {
                        promiseToDisableCradleSyncForMaterial = false;

                        if (HighLevel.Status.CradleInSync == true)
                        {
                            Devices.Cradle.SetSync(false);
                            Machine.UI.Controls.MachineMessageBox.Show(Localization.Warning, Localization.MaterialNotPresent);
                            ProConsole.WriteLine("MaterialNotPresent");
                        }
                        checkPromise = true;
                    }

                    if (promiseToEnableCradleSyncForRoll)
                    {
                        promiseToEnableCradleSyncForRoll = false;

                        Devices.Cradle.SetSync(true);
                    }

                    if (promiseToDisableCradleSyncForRoll)
                    {
                        promiseToDisableCradleSyncForRoll = false;

                        if (HighLevel.Status.CradleInSync == true)
                        {
                            Devices.Cradle.SetSync(false);
                            Machine.UI.Controls.MachineMessageBox.Show(Localization.Warning, Localization.RollNotPresent);
                            ProConsole.WriteLine("RollNotPresent");
                        }
                        checkPromise = true;
                    }

                    if (HighLevel.Status.CradleInSync == false && HighLevel.Status.PromiseToEnableCradleInSyncAfterClick == true && checkPromise == false)
                    {
                        HighLevel.Status.PromiseToEnableCradleInSyncAfterClick = false;
                        Devices.Cradle.SetSync(true);
                    }
                    else if (HighLevel.Status.ForceDisableCradleInSync == true && HighLevel.Status.PromiseToEnableCradleInSyncAfterClick == false)
                    {
                        Devices.Cradle.SetSync(false);
                        HighLevel.Status.ForceDisableCradleInSync = false;
                    }
                    //MMFx02
                }

                //------------------------------------------------
                // Wait
                //------------------------------------------------
                Thread.Sleep(Machine.Constants.Intervals.HighLevelControlCycle);

            } //while()

            ProConsole.WriteLine("[EXITING] TaskStateNormal", ConsoleColor.Red);

        } //task

    } //class
} //namespace