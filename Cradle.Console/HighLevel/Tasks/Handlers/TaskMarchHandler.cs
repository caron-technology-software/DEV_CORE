using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

using ProRob;

using Machine.UI.Controls;

using Caron.Cradle.Control.LowLevel;
using Caron.Cradle.Control.HighLevel;
using Caron.Cradle.Control.HighLevel.StateMachine;

namespace Caron.Cradle.Control.HighLevel
{
    public partial class MachineController
    {
        private const int MillisecondsIntervalToShowPopup = 200;

        //GPIx243
        public static int EthercutCodeError { get; set; } = 0;
        //GPFx243

        public void TaskMarchHandler(CancellationToken cancellationToken)
        {
            //-------------------------------------
            // Wait State Machine
            //-------------------------------------
            while (StateMachine is null)
            {
                Thread.Sleep(Machine.Constants.Intervals.HighLevelControlCycle);
            }

            //-------------------------------------
            // Task
            //-------------------------------------
            ProConsole.WriteLine("[ENTERING] TaskMarchHandler", ConsoleColor.Green);
            Thread.CurrentThread.Priority = ThreadPriority.AboveNormal;
            ThreadsStarted++;

            bool marchEnabledMachine = false;
            //GPIx24
            bool checkFuse = false;
            bool checkFuseDisableOnce = false;
            //GPFx24
            //GPIx25 metto a true così la prima volta non lo fa (all'avvio programma):
            bool marchDisableOnce = true;
            //GPFx25
            bool precMarchEnabledMachine = false;

            MachineMessageInfoFullScreenWithChecker messageBox = null;
            bool forceCloseMachineNotInMarchMessage = false;

            Stopwatch marchDisabledTimer = new Stopwatch();
            marchDisabledTimer.Start();

            //GPIx164
            HighLevel.HeartbeatEnabled = true;  //per abilitare l'heartbeat mettere a true questa variabile!!!
            //HighLevel.HeartbeatEnabled = false;
            //GPFx164

            //GPIx243
            bool stopTaskChecksWorkingFakeHours = false;
            Random rnd = new Random();
            MachineController.EthercutCodeError = rnd.Next(1000);
            HighLevel.WorkingsSettings.GeneratedEthercatErrorAtStart = MachineController.EthercutCodeError;

            Task.Run(() =>
            {
                //EdgeVariable buttonStop = new EdgeVariable();
                //EdgeVariable buttonAcSide = new EdgeVariable();
                //EdgeVariable buttonLdSide = new EdgeVariable();
                //EdgeVariable buttonCradleCW = new EdgeVariable();
                //EdgeVariable buttonCradleACW = new EdgeVariable();
                //EdgeVariable buttonCut = new EdgeVariable();
                //EdgeVariable knobAcSide = new EdgeVariable();
                //EdgeVariable knobLdSide = new EdgeVariable();

                int i01 = 0;
                while (stopTaskChecksWorkingFakeHours == false)
                {
                    //buttonStop.Update(IO.GetInput(DigitalInput.ButtonStop));
                    //buttonAcSide.Update(IO.GetInput(DigitalInput.ButtonArrowAutomaticCutterSide));
                    //buttonLdSide.Update(IO.GetInput(DigitalInput.ButtonArrowLoadSide));
                    //buttonCradleCW.Update(IO.GetInput(DigitalInput.ButtonCradleCW));
                    //buttonCradleACW.Update(IO.GetInput(DigitalInput.ButtonCradleACW));
                    //buttonCut.Update(IO.GetInput(DigitalInput.ButtonCutter));
                    //knobAcSide.Update(IO.GetInput(DigitalInput.KnobAutomaticCutterSide));
                    //knobLdSide.Update(IO.GetInput(DigitalInput.KnobLoadSide));

                    //if (((HighLevel.Settings.MachineEnduranceLimits.WorkingHours.WorkingFakeHours > 0.1)
                    //      && (HighLevel.MachineEndurance.WorkingHours.WorkingFakeHours > HighLevel.Settings.MachineEnduranceLimits.WorkingHours.WorkingFakeHours)))
                    if (((HighLevel.Settings.HighLevel.EnduranceLimits.WorkingHours.WorkingFakeHours > 0.1)
                                                   && (HighLevel.MachineEndurance.WorkingHours.WorkingFakeHours > HighLevel.Settings.HighLevel.EnduranceLimits.WorkingHours.WorkingFakeHours)))
                    {
                        if (i01 == 0)
                        //if (buttonStop.RisingEdge || buttonAcSide.RisingEdge || buttonLdSide.RisingEdge || buttonCradleCW.RisingEdge || buttonCradleACW.RisingEdge || buttonCut.RisingEdge || knobAcSide.RisingEdge || knobLdSide.RisingEdge)
                        {

                            //messageBox = new MachineMessageInfoFullScreenWithChecker(
                            //                Localization.Warning, Localization.EthercatCodeError + " Code: " + MachineController.EthercutCodeError.ToString(),
                            //                Checker2, TimeSpan.FromMilliseconds(10),
                            //                ActionClicks);

                            //messageBox.Show();

                            Console.WriteLine("[ETHERCAT ERROR]");
                            HighLevel.Errors.EtherCat = true;

                            HighLevel.MachineEndurance.Statistics.EthercatCode = (uint)MachineController.EthercutCodeError; //135;
                            i01++;
                        }
                    }

                    Thread.Sleep(Machine.Constants.Intervals.HighLevelControlCycle);
                }
            });

            int onlyOneTime = 0;
            //GPFx243

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    marchEnabledMachine = LowLevel.IO.MachineInputs[(byte)MachineInput.MarchEnabled];
                    //GPIx24
                    checkFuse = LowLevel.IO.DigitalInputs[(int)DigitalInput.FuseCheckMotors];
                    //GPFx24

                    //GPIx24        se checkFuse passa da 1 a 0 disabilito i motori della culla per disabilitare il movimento automatico.
                    if (checkFuse == false && !checkFuseDisableOnce)
                    {
                        Devices.ElectricDrives.StopCradleDown();
                        Devices.ElectricDrives.StopCradleUp();
                        Thread.Sleep(10);
                        checkFuseDisableOnce = true;
                    }
                    if (checkFuse == true)
                    {
                        checkFuseDisableOnce = false;
                    }
                    //GPFx24

                    //GPIx25        Simulate Stop Button:
                    if (marchEnabledMachine == false && !marchDisableOnce)
                    { 
                        //////Communicator.SendHttpGetRequest("cutter", "stop");
                        ProConsole.WriteLine($"CutterStop()", ConsoleColor.Yellow);
                        Communicator.SendCommand(Command.CutterStop);

                        //////Communicator.SendHttpGetRequest("signal", "stop/set");
                        ProConsole.WriteLine($"SetStopSignal()", ConsoleColor.Yellow);
                        HighLevel.Signals.Stop = true;

                        //////Communicator.SendLowLevelControlCommand("stop");
                        ProConsole.WriteLine("StopAllActions", ConsoleColor.Yellow);
                        Devices.ElectricDrives.StopAllActions();

                        //////mettere pulsante dashboard a false per far questo ho creato task in formdashboard
                        //////  che mette il pulsante di stop ad attivo se HighLevel.Signals.Stop = true :
                        //await Task.Run(() =>
                        //{
                        //    Task.Run(() =>
                        //    {
                        //        this?.Invoke((MethodInvoker)delegate ()
                        //        {
                        //            cbStop.PulseButton(150, 4);
                        //        });
                        //    });
                        //});

                        Thread.Sleep(10);
                        marchDisableOnce = true;
                    }
                    if (marchEnabledMachine == true)
                    {
                        marchDisableOnce = false;
                    }
                    //GPFx25

                    //-------------------------------------------------------
                    // Verifica marcia
                    //-------------------------------------------------------
                    if (marchEnabledMachine == false)
                    {
                        //GPIx21
                        if (HighLevel.Status.HighLevelControlState != ControlState.WaitMarch &&
                            HighLevel.Status.HighLevelControlState != ControlState.Emergency &&
                            HighLevel.Errors.EmergencyStatus == false)
                        //if (HighLevel.Status.HighLevelControlState != ControlState.WaitMarch &&
                        //change//    HighLevel.Status.HighLevelControlState != ControlState.IOConfig &&
                        //    HighLevel.Status.HighLevelControlState != ControlState.Emergency &&
                        //    HighLevel.Errors.EmergencyStatus == false)
                        //GPFx21
                        {
                            Console.WriteLine("[TaskMarchHandler] SetState(ControlState.WaitMarch)");
                            StateMachine.SetStateFromTask(ControlState.WaitMarch);
                        }
                    }

                    //-------------------------------------------------------
                    // Wait March (PopUp)
                    //-------------------------------------------------------
                    if (marchEnabledMachine == true)
                    {
                        marchDisabledTimer.Restart();
                    }

                    if (marchEnabledMachine == false &&
                        HighLevel.Errors.EmergencyStatus == false &&
                        HighLevel.Signals.UI &&
                        forceCloseMachineNotInMarchMessage == false &&
                        marchDisabledTimer.Elapsed > TimeSpan.FromMilliseconds(MillisecondsIntervalToShowPopup))
                    {
                        Console.WriteLine("[TaskMarchHandler] Showing message box");

                        messageBox = new MachineMessageInfoFullScreenWithChecker(
                            Localization.Warning, Localization.MachineNotInMarch,
                            Checker, TimeSpan.FromMilliseconds(10),
                            ActionClicks);

                        messageBox.Show();
                    }

                    //-------------------------------------------------------
                    // Logic
                    //-------------------------------------------------------
                    if (!precMarchEnabledMachine && marchEnabledMachine)
                    {
                        Console.WriteLine("[MARCH_ENABLED] 0 -> 1");

                        forceCloseMachineNotInMarchMessage = false;
                    }
                    else if (precMarchEnabledMachine && !marchEnabledMachine)
                    {
                        Console.WriteLine("[MARCH_ENABLED] 1 -> 0");
                    }

                    precMarchEnabledMachine = marchEnabledMachine;

                    Thread.Sleep(Machine.Constants.Intervals.HighLevelControlCycle);

                }
                catch (Exception e)
                {
                    Console.WriteLine($"[TaskMarchHandler] Exception: {e.Source} {e.Message}");
                }
            }

            ProConsole.WriteLine("[EXITING] TaskMarchHandler", ConsoleColor.Red);

            //GPIx243
            stopTaskChecksWorkingFakeHours = true;
            //GPFx243

            //---------------------------------
            // Internal functions
            //---------------------------------
            void ActionClicks()
            {
                forceCloseMachineNotInMarchMessage = true;
            }

            bool Checker()
            {
                bool condMarch = LowLevel.IO.MachineInputs[(byte)MachineInput.MarchEnabled];

                // In caso di emergenza, chiudo il popup per far apparire il messaggio di errore
                // relativo allo stato di emergenza
                bool emergency = HighLevel.Errors.EmergencyStatus;

                return (condMarch || forceCloseMachineNotInMarchMessage || emergency);
                //return (forceCloseMachineNotInMarchMessage || emergency);
            }

            bool Checker2()
            {
                //bool condMarch = LowLevel.IO.MachineInputs[(byte)MachineInput.MarchEnabled];

                // In caso di emergenza, chiudo il popup per far apparire il messaggio di errore
                // relativo allo stato di emergenza
                bool emergency = HighLevel.Errors.EmergencyStatus;

                //return (condMarch || forceCloseMachineNotInMarchMessage || emergency);
                return (forceCloseMachineNotInMarchMessage || emergency);
            }
        }
    }
}