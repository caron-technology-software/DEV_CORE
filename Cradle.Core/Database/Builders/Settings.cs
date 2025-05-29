using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using Machine;
using Machine.Utility;

using Caron.Cradle.Control.Database;
using Caron.Cradle.Control.HighLevel.Settings;
using Caron.Cradle.Control.LowLevel;

namespace Caron.Cradle.Control.HighLevel
{
    public class BuilderSettings
    {
        public static void CreateDefaultSettings()
        {

            #region MachineConfiguration
            {
                var configuration = new MachineConfiguration
                {
                    MachineType = MachineType.LeftMachine,
                    MachineSerial = "000-000-000"
                };

                DatabaseSettings.Update(configuration);
            }
            #endregion

            #region Localization
            {
                var settings = new LocalizationSettings()
                {
                    MachineLanguage = MachineLanguage.English
                };

                DatabaseSettings.Update(settings);
            }
            #endregion

            #region MachineEndurance
            {
                var machineEndurance = new MachineEndurance();
                DatabaseSettings.Update(machineEndurance);
            }
            #endregion

            #region Endurance Limits
            {
                var settings = new Settings.HighLevelSettings();
                var limits = settings.EnduranceLimits;

                //----------------------
                // DigitalInputs
                //----------------------
                limits.DigitalInputsToggles[(byte)DigitalInput.FuseCheckMotors] = 1_000_000;
                limits.DigitalInputsToggles[(byte)DigitalInput.TitanLimit] = 1_000_000;
                limits.DigitalInputsToggles[(byte)DigitalInput.PhotocellOperatorSide] = 0;
                limits.DigitalInputsToggles[(byte)DigitalInput.PhotocellMotorSide] = 0;
                limits.DigitalInputsToggles[(byte)DigitalInput.PhotocellMaterialPresence] = 0;
                limits.DigitalInputsToggles[(byte)DigitalInput.LimitCutterOperatorSide] = 0;
                limits.DigitalInputsToggles[(byte)DigitalInput.LimitCutterMotorSide] = 0;
                limits.DigitalInputsToggles[(byte)DigitalInput.LimitDancer] = 10_000_000;
                limits.DigitalInputsToggles[(byte)DigitalInput.LimitAlignmentOperatorSide] = 10_000_000;
                limits.DigitalInputsToggles[(byte)DigitalInput.LimitAlignmentMotorSide] = 10_000_000;
                limits.DigitalInputsToggles[(byte)DigitalInput.LimitOverturningOperatorSideLoad] = 10_000_000;
                limits.DigitalInputsToggles[(byte)DigitalInput.LimitOverturningOperatorSideUnload] = 10_000_000;
                limits.DigitalInputsToggles[(byte)DigitalInput.LimitSpoonUp] = 10_000_000;
                limits.DigitalInputsToggles[(byte)DigitalInput.LimitSpoonDown] = 10_000_000;
                limits.DigitalInputsToggles[(byte)DigitalInput.LimitOverturningMotorSideLoad] = 10_000_000;
                limits.DigitalInputsToggles[(byte)DigitalInput.LimitOverturningMotorSideUnload] = 10_000_000;
                limits.DigitalInputsToggles[(byte)DigitalInput.ZundEnable] = 0;
                limits.DigitalInputsToggles[(byte)DigitalInput.ZundCutOff] = 0;

                //----------------------
                // DigitalOutpus
                //----------------------
                limits.DigitalOutputsToggles[(byte)DigitalOutput.MotorOverturningOpSideLoad] = 10_000_000;
                limits.DigitalOutputsToggles[(byte)DigitalOutput.MotorOverturningOpSideUnload] = 10_000_000;
                limits.DigitalOutputsToggles[(byte)DigitalOutput.MotorAlignmentOpSide] = 1_000_000;
                limits.DigitalOutputsToggles[(byte)DigitalOutput.MotorAlignmentMtSide] = 1_000_000;
                limits.DigitalOutputsToggles[(byte)DigitalOutput.TitanUp] = 1_000_000;
                limits.DigitalOutputsToggles[(byte)DigitalOutput.TitanDown] = 1_000_000;
                limits.DigitalOutputsToggles[(byte)DigitalOutput.OutFree01] = 0;
                limits.DigitalOutputsToggles[(byte)DigitalOutput.OutFree02] = 0;
                limits.DigitalOutputsToggles[(byte)DigitalOutput.MotorSpoonUp] = 2 * 1_000_000;
                limits.DigitalOutputsToggles[(byte)DigitalOutput.MotorSpoonDown] = 2 * 1_000_000;
                limits.DigitalOutputsToggles[(byte)DigitalOutput.MotorOverturningMtSideLoad] = 10_000_000;
                limits.DigitalOutputsToggles[(byte)DigitalOutput.MotorOverturningMtSideUnload] = 10_000_000;
                limits.DigitalOutputsToggles[(byte)DigitalOutput.MarchEnabled] = 2 * 1_000_000;
                limits.DigitalOutputsToggles[(byte)DigitalOutput.AxisCradleToCutterExchange] = 2 * 1_000_000;
                limits.DigitalOutputsToggles[(byte)DigitalOutput.OutFree03] = 0;
                limits.DigitalOutputsToggles[(byte)DigitalOutput.OutFree04] = 0;
                limits.DigitalOutputsToggles[(byte)DigitalOutput.ZundStatus] = 0;
                limits.DigitalOutputsToggles[(byte)DigitalOutput.ZundError] = 0;

                limits.WorkingHours.PowerOnHours = 0;
                limits.WorkingHours.WorkingFakeHours = 0;

                limits.Statistics.NumberPowerOn = 0;
                limits.Statistics.NumberPowerOff = 0;

                limits.Cutter.NumberOfCutOff = 0;

                limits.WorkingHours.WorkingWithCradleInSyncHours = 0;
                limits.WorkingHours.MachineMaintenanceHours = 500;

                DatabaseSettings.Update(settings);
            }
            #endregion

            #region LowLevelMotionSettings
            {
                var settings = new LowLevelMotionSettings();

                DatabaseSettings.Update(settings);
            }
            #endregion

            #region UISettings
            {
                var settings = new UISettings();

                DatabaseSettings.Update(settings);
            }
            #endregion

            #region WorkingStatistics
            {
                var statistics = new Working();
                DatabaseSettings.Update(statistics);
            }
            #endregion

            #region WorkingContext / WorkingsSettings
            {
                //Delete file
                if (File.Exists(Cradle.Constants.Path.Settings.DefaultWorkingsSettingsFile))
                {
                    File.Delete(Cradle.Constants.Path.Settings.DefaultWorkingsSettingsFile);
                }

                var context = new WorkingContext();
                var settings = new WorkingsSettings();

                foreach (var name in new string[] { "jeans", "wool", "cotton" })
                {
                    settings.Items.Add(new WorkingSetting()
                    {
                        Name = name
                    });
                }

                var setting = settings.Items.First();
                context.CurrentGuidWorkingParameterSet = setting.Guid;
                context.CurrentNameWorkingParameterSet = setting.Name;
                context.Parameters = setting.Parameters;

                DatabaseSettings.Update(context);
                DatabaseSettings.Update(settings);
            }
            #endregion

            //Attesa termine scritture in background
            MachineData.WaitAllOperations();

            //Default settings file
            //File.Copy(Cradle.Constants.Path.Settings.WorkingsSettingsFile, Cradle.Constants.Path.Settings.DefaultWorkingsSettingsFile);
        }

        //GPIx101 NEW DIGITAL INPUT aggiorna il database permanente con i nuovi digital input!!!
        public static void CreateDefaultSettingsNewDigitalInput()
        {
            Console.WriteLine($"Loading machine endurance..");
            var machineActualEndurance = MachineData.Read<MachineEndurance>(Constants.Path.Settings.MachineEndurance);
            var machineOldActualEndurance = MachineData.Read<MachineEndurance>(Constants.Path.Settings.MachineEndurance);
            if (!(machineActualEndurance.DigitalInputsToggles.Length > 18))
            {
                #region Machine Endurance
                {
                    //var machineEndurance = new MachineEndurance();
                    //DatabaseSettings.Update(machineEndurance);                  
                    machineActualEndurance.DigitalInputsToggles = new uint[Enum.GetNames(typeof(DigitalInput)).Count()];

                    //----------------------
                    // Copy old DigitalInputs
                    //----------------------
                    machineActualEndurance.DigitalInputsToggles[(byte)DigitalInput.FuseCheckMotors] = machineOldActualEndurance.DigitalInputsToggles[(byte)DigitalInput.FuseCheckMotors];
                    machineActualEndurance.DigitalInputsToggles[(byte)DigitalInput.TitanLimit] = machineOldActualEndurance.DigitalInputsToggles[(byte)DigitalInput.TitanLimit];
                    machineActualEndurance.DigitalInputsToggles[(byte)DigitalInput.PhotocellOperatorSide] = machineOldActualEndurance.DigitalInputsToggles[(byte)DigitalInput.PhotocellOperatorSide];
                    machineActualEndurance.DigitalInputsToggles[(byte)DigitalInput.PhotocellMotorSide] = machineOldActualEndurance.DigitalInputsToggles[(byte)DigitalInput.PhotocellMotorSide];
                    machineActualEndurance.DigitalInputsToggles[(byte)DigitalInput.PhotocellMaterialPresence] = machineOldActualEndurance.DigitalInputsToggles[(byte)DigitalInput.PhotocellMaterialPresence];
                    machineActualEndurance.DigitalInputsToggles[(byte)DigitalInput.LimitCutterOperatorSide] = machineOldActualEndurance.DigitalInputsToggles[(byte)DigitalInput.LimitCutterOperatorSide];
                    machineActualEndurance.DigitalInputsToggles[(byte)DigitalInput.LimitCutterMotorSide] = machineOldActualEndurance.DigitalInputsToggles[(byte)DigitalInput.LimitCutterMotorSide];
                    machineActualEndurance.DigitalInputsToggles[(byte)DigitalInput.LimitDancer] = machineOldActualEndurance.DigitalInputsToggles[(byte)DigitalInput.LimitDancer];
                    machineActualEndurance.DigitalInputsToggles[(byte)DigitalInput.LimitAlignmentOperatorSide] = machineOldActualEndurance.DigitalInputsToggles[(byte)DigitalInput.LimitAlignmentOperatorSide];
                    machineActualEndurance.DigitalInputsToggles[(byte)DigitalInput.LimitAlignmentMotorSide] = machineOldActualEndurance.DigitalInputsToggles[(byte)DigitalInput.LimitAlignmentMotorSide];
                    machineActualEndurance.DigitalInputsToggles[(byte)DigitalInput.LimitOverturningOperatorSideLoad] = machineOldActualEndurance.DigitalInputsToggles[(byte)DigitalInput.LimitOverturningOperatorSideLoad];
                    machineActualEndurance.DigitalInputsToggles[(byte)DigitalInput.LimitOverturningOperatorSideUnload] = machineOldActualEndurance.DigitalInputsToggles[(byte)DigitalInput.LimitOverturningOperatorSideUnload];
                    machineActualEndurance.DigitalInputsToggles[(byte)DigitalInput.LimitSpoonUp] = machineOldActualEndurance.DigitalInputsToggles[(byte)DigitalInput.LimitSpoonUp];
                    machineActualEndurance.DigitalInputsToggles[(byte)DigitalInput.LimitSpoonDown] = machineOldActualEndurance.DigitalInputsToggles[(byte)DigitalInput.LimitSpoonDown];
                    machineActualEndurance.DigitalInputsToggles[(byte)DigitalInput.LimitOverturningMotorSideLoad] = machineOldActualEndurance.DigitalInputsToggles[(byte)DigitalInput.LimitOverturningMotorSideLoad];
                    machineActualEndurance.DigitalInputsToggles[(byte)DigitalInput.LimitOverturningMotorSideUnload] = machineOldActualEndurance.DigitalInputsToggles[(byte)DigitalInput.LimitOverturningMotorSideUnload];
                    machineActualEndurance.DigitalInputsToggles[(byte)DigitalInput.ZundEnable] = machineOldActualEndurance.DigitalInputsToggles[(byte)DigitalInput.ZundEnable];
                    machineActualEndurance.DigitalInputsToggles[(byte)DigitalInput.ZundCutOff] = machineOldActualEndurance.DigitalInputsToggles[(byte)DigitalInput.ZundCutOff];

                    DatabaseSettings.Update(machineActualEndurance);
                }
                #endregion

                //#region Machine Endurance Limits
                //{
                //      CreateDefaultMachineEnduranceLimitsNewDigitalInput();     //GPIx246  remmato per check esterno
                //}
                //#endregion
            }

            //GPIx246
            #region controllare che il setting contenga i giusti digital input e output per "EnduranceLimits.DigitalInputsToggles" 
            //controllare che il setting contenga i giusti digital input e output: //GPIx246 da aggiungere!!!
            //      per "HighLevel.Settings.HighLevel.EnduranceLimits.DigitalInputsToggles.Count()"
            CreateDefaultMachineEnduranceLimitsNewDigitalInput();
            //
            #endregion
            //GPFx246

        }
        //GPFx101 

        //GPIx101 NUOVI DIGITAL INPUT
        public static void CreateDefaultMachineEnduranceLimitsNewDigitalInput()
        {
            //var settings = new Settings.HighLevelSettings();
            //var limits = settings.MachineEnduranceLimits;
            var settingsActual = MachineData.Read<HighLevelSettings>(Constants.Path.Settings.SettingsFile);
            var settingsActualOld = MachineData.Read<HighLevelSettings>(Constants.Path.Settings.SettingsFile);
            var limits = settingsActual.EnduranceLimits;
            var limitsActual = settingsActualOld.EnduranceLimits;
            var len = limits.DigitalInputsToggles.Length;

            if (!(settingsActual.EnduranceLimits.DigitalInputsToggles.Length > 18))    //GPIx246   aggiunta
            {
                limits.DigitalInputsToggles = new uint[Enum.GetNames(typeof(DigitalInput)).Count()];

                //----------------------
                // DigitalInputs
                //----------------------
                limits.DigitalInputsToggles[(byte)DigitalInput.FuseCheckMotors] = limitsActual.DigitalInputsToggles[(byte)DigitalInput.FuseCheckMotors];
                limits.DigitalInputsToggles[(byte)DigitalInput.TitanLimit] = limitsActual.DigitalInputsToggles[(byte)DigitalInput.TitanLimit];
                limits.DigitalInputsToggles[(byte)DigitalInput.PhotocellOperatorSide] = limitsActual.DigitalInputsToggles[(byte)DigitalInput.PhotocellOperatorSide];
                limits.DigitalInputsToggles[(byte)DigitalInput.PhotocellMotorSide] = limitsActual.DigitalInputsToggles[(byte)DigitalInput.PhotocellMotorSide];
                limits.DigitalInputsToggles[(byte)DigitalInput.PhotocellMaterialPresence] = limitsActual.DigitalInputsToggles[(byte)DigitalInput.PhotocellMaterialPresence];
                limits.DigitalInputsToggles[(byte)DigitalInput.LimitCutterOperatorSide] = limitsActual.DigitalInputsToggles[(byte)DigitalInput.LimitCutterOperatorSide];
                limits.DigitalInputsToggles[(byte)DigitalInput.LimitCutterMotorSide] = limitsActual.DigitalInputsToggles[(byte)DigitalInput.LimitCutterMotorSide];
                limits.DigitalInputsToggles[(byte)DigitalInput.LimitDancer] = limitsActual.DigitalInputsToggles[(byte)DigitalInput.LimitDancer];
                limits.DigitalInputsToggles[(byte)DigitalInput.LimitAlignmentOperatorSide] = limitsActual.DigitalInputsToggles[(byte)DigitalInput.LimitAlignmentOperatorSide];
                limits.DigitalInputsToggles[(byte)DigitalInput.LimitAlignmentMotorSide] = limitsActual.DigitalInputsToggles[(byte)DigitalInput.LimitAlignmentMotorSide];
                limits.DigitalInputsToggles[(byte)DigitalInput.LimitOverturningOperatorSideLoad] = limitsActual.DigitalInputsToggles[(byte)DigitalInput.LimitOverturningOperatorSideLoad];
                limits.DigitalInputsToggles[(byte)DigitalInput.LimitOverturningOperatorSideUnload] = limitsActual.DigitalInputsToggles[(byte)DigitalInput.LimitOverturningOperatorSideUnload];
                limits.DigitalInputsToggles[(byte)DigitalInput.LimitSpoonUp] = limitsActual.DigitalInputsToggles[(byte)DigitalInput.LimitSpoonUp];
                limits.DigitalInputsToggles[(byte)DigitalInput.LimitSpoonDown] = limitsActual.DigitalInputsToggles[(byte)DigitalInput.LimitSpoonDown];
                limits.DigitalInputsToggles[(byte)DigitalInput.LimitOverturningMotorSideLoad] = limitsActual.DigitalInputsToggles[(byte)DigitalInput.LimitOverturningMotorSideLoad];
                limits.DigitalInputsToggles[(byte)DigitalInput.LimitOverturningMotorSideUnload] = limitsActual.DigitalInputsToggles[(byte)DigitalInput.LimitOverturningMotorSideUnload];
                limits.DigitalInputsToggles[(byte)DigitalInput.ZundEnable] = limitsActual.DigitalInputsToggles[(byte)DigitalInput.ZundEnable];
                limits.DigitalInputsToggles[(byte)DigitalInput.ZundCutOff] = limitsActual.DigitalInputsToggles[(byte)DigitalInput.ZundCutOff];
                //GPIx101 da aggiungere
                limits.DigitalInputsToggles[(byte)DigitalInput.PhotocellRollPresence] = 0;
                //GPFx101

                //DatabaseSettings.Update(settings);
                DatabaseSettings.Update(settingsActual);
            }
        }
        //GPFx101

        public static void ResetToDefaultSettings()
        {

            //#region MachineConfiguration
            //{
            //    var configuration = new MachineConfiguration
            //    {
            //        MachineType = CuttingRoomMachine.MachineType.LeftMachine,
            //        MachineSerial = "000-000-000"
            //    };

            //    DatabaseSettings.Update(configuration);
            //}
            //#endregion

            #region Localization
            {
                var settings = new LocalizationSettings()
                {
                    MachineLanguage = MachineLanguage.English
                };

                DatabaseSettings.Update(settings);
            }
            #endregion

            //NON RESETTO MACHINE ENDURANCE
            //#region MachineEndurance
            //{
            //    var machineEndurance = new MachineEndurance();
            //    DatabaseSettings.Update(machineEndurance);
            //}
            //#endregion

            #region Endurance Limits
            {
                var settings = new Settings.HighLevelSettings();
                var limits = settings.EnduranceLimits;

                //----------------------
                // DigitalInputs
                //----------------------
                limits.DigitalInputsToggles[(byte)DigitalInput.FuseCheckMotors] = 1_000_000;
                limits.DigitalInputsToggles[(byte)DigitalInput.TitanLimit] = 1_000_000;
                limits.DigitalInputsToggles[(byte)DigitalInput.PhotocellOperatorSide] = 0;
                limits.DigitalInputsToggles[(byte)DigitalInput.PhotocellMotorSide] = 0;
                limits.DigitalInputsToggles[(byte)DigitalInput.PhotocellMaterialPresence] = 0;
                limits.DigitalInputsToggles[(byte)DigitalInput.LimitCutterOperatorSide] = 0;
                limits.DigitalInputsToggles[(byte)DigitalInput.LimitCutterMotorSide] = 0;
                limits.DigitalInputsToggles[(byte)DigitalInput.LimitDancer] = 10_000_000;
                limits.DigitalInputsToggles[(byte)DigitalInput.LimitAlignmentOperatorSide] = 10_000_000;
                limits.DigitalInputsToggles[(byte)DigitalInput.LimitAlignmentMotorSide] = 10_000_000;
                limits.DigitalInputsToggles[(byte)DigitalInput.LimitOverturningOperatorSideLoad] = 10_000_000;
                limits.DigitalInputsToggles[(byte)DigitalInput.LimitOverturningOperatorSideUnload] = 10_000_000;
                limits.DigitalInputsToggles[(byte)DigitalInput.LimitSpoonUp] = 10_000_000;
                limits.DigitalInputsToggles[(byte)DigitalInput.LimitSpoonDown] = 10_000_000;
                limits.DigitalInputsToggles[(byte)DigitalInput.LimitOverturningMotorSideLoad] = 10_000_000;
                limits.DigitalInputsToggles[(byte)DigitalInput.LimitOverturningMotorSideUnload] = 10_000_000;
                limits.DigitalInputsToggles[(byte)DigitalInput.ZundEnable] = 0;
                limits.DigitalInputsToggles[(byte)DigitalInput.ZundCutOff] = 0;

                //----------------------
                // DigitalOutpus
                //----------------------
                limits.DigitalOutputsToggles[(byte)DigitalOutput.MotorOverturningOpSideLoad] = 10_000_000;
                limits.DigitalOutputsToggles[(byte)DigitalOutput.MotorOverturningOpSideUnload] = 10_000_000;
                limits.DigitalOutputsToggles[(byte)DigitalOutput.MotorAlignmentOpSide] = 1_000_000;
                limits.DigitalOutputsToggles[(byte)DigitalOutput.MotorAlignmentMtSide] = 1_000_000;
                limits.DigitalOutputsToggles[(byte)DigitalOutput.TitanUp] = 1_000_000;
                limits.DigitalOutputsToggles[(byte)DigitalOutput.TitanDown] = 1_000_000;
                limits.DigitalOutputsToggles[(byte)DigitalOutput.OutFree01] = 0;
                limits.DigitalOutputsToggles[(byte)DigitalOutput.OutFree02] = 0;
                limits.DigitalOutputsToggles[(byte)DigitalOutput.MotorSpoonUp] = 2 * 1_000_000;
                limits.DigitalOutputsToggles[(byte)DigitalOutput.MotorSpoonDown] = 2 * 1_000_000;
                limits.DigitalOutputsToggles[(byte)DigitalOutput.MotorOverturningMtSideLoad] = 10_000_000;
                limits.DigitalOutputsToggles[(byte)DigitalOutput.MotorOverturningMtSideUnload] = 10_000_000;
                limits.DigitalOutputsToggles[(byte)DigitalOutput.MarchEnabled] = 2 * 1_000_000;
                limits.DigitalOutputsToggles[(byte)DigitalOutput.AxisCradleToCutterExchange] = 2 * 1_000_000;
                limits.DigitalOutputsToggles[(byte)DigitalOutput.OutFree03] = 0;
                limits.DigitalOutputsToggles[(byte)DigitalOutput.OutFree04] = 0;
                limits.DigitalOutputsToggles[(byte)DigitalOutput.ZundStatus] = 0;
                limits.DigitalOutputsToggles[(byte)DigitalOutput.ZundError] = 0;

                limits.WorkingHours.PowerOnHours = 0;
                limits.WorkingHours.WorkingFakeHours = 0;

                limits.Statistics.NumberPowerOn = 0;
                limits.Statistics.NumberPowerOff = 0;

                limits.Cutter.NumberOfCutOff = 0;

                limits.WorkingHours.WorkingWithCradleInSyncHours = 0;
                limits.WorkingHours.MachineMaintenanceHours = 500;

                DatabaseSettings.Update(settings);
            }
            #endregion

            #region LowLevelMotionSettings
            {
                var settings = new LowLevelMotionSettings();

                DatabaseSettings.Update(settings);
            }
            #endregion

            #region UISettings
            {
                var settings = new UISettings();

                DatabaseSettings.Update(settings);
            }
            #endregion

            #region WorkingStatistics
            {
                var statistics = new Working();
                DatabaseSettings.Update(statistics);
            }
            #endregion

            #region WorkingContext / WorkingsSettings
            {
                //Delete file
                if (File.Exists(Cradle.Constants.Path.Settings.DefaultWorkingsSettingsFile))
                {
                    File.Delete(Cradle.Constants.Path.Settings.DefaultWorkingsSettingsFile);
                }

                var context = new WorkingContext();
                var settings = new WorkingsSettings();

                foreach (var name in new string[] { "jeans", "wool", "cotton" })
                {
                    settings.Items.Add(new WorkingSetting()
                    {
                        Name = name,
                    });
                }

                var setting = settings.Items.First();

                context.CurrentGuidWorkingParameterSet = setting.Guid;
                context.CurrentNameWorkingParameterSet = setting.Name;
                context.Parameters = setting.Parameters;

                DatabaseSettings.Update(context);
                DatabaseSettings.Update(settings);
            }
            #endregion

            //Attesa termine scritture in background
            MachineData.WaitAllOperations();

            //Default settings file
            //File.Copy(Cradle.Constants.Path.Settings.WorkingsSettingsFile, Cradle.Constants.Path.Settings.DefaultWorkingsSettingsFile);
        }
    }
}