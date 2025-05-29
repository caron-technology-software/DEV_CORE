using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Caron.Cradle.Control.LowLevel;
using Caron.Cradle.Control.LowLevel.Communication;

namespace Caron.Cradle.Control.HighLevel.Devices
{
    public class Cradle
    {
        private LowLevel.ControlStatus LowLevel { get; set; }
        private HighLevel.ControlStatus HighLevel { get; set; }

        private Communicator Communicator { get; set; }

        public Cradle(LowLevel.ControlStatus lowLevelControlStatus, HighLevel.ControlStatus highLevelControlStatus, Communicator communicator)
        {
            this.LowLevel = lowLevelControlStatus;
            this.HighLevel = highLevelControlStatus;
            this.Communicator = communicator;
        }

        public void SetLowLevelStateToWaitCommand()
        {
            if (LowLevel.Info.MachineState == (byte)Control.LowLevel.ControlState.WaitCommand)
            {
                return;
            }

            Communicator.SetLowLevelControlState(Control.LowLevel.ControlState.WaitCommand);
        }

        void TestConditionsAndPossiblyEnableSync()
        {
            //GPI12 sostituzione check temporale su PhotocellMaterialPresence:
            //bool bol01 = false;
            //if (LowLevel.IO.DigitalInputs[(byte)DigitalInput.PhotocellMaterialPresence] == false)
            //{
            //    bol01 = true;
            //}
            //else
            //{
            //    bol01 = false;
            //}
            //DateTime checkUntilPhotocellMaterialPresence = DateTime.MinValue;
            //checkUntilPhotocellMaterialPresence = DateTime.UtcNow + TimeSpan.FromMilliseconds(HighLevel.Settings.HighLevel.MachineParameters.CheckUntilPhotocellMaterialPresence);   //TimeSpan.FromMilliseconds(250) parametro da mettere nella Cradle per intervallo di check fotocellula presenza materiale
            ////GPI18
            //while ((DateTime.UtcNow < checkUntilPhotocellMaterialPresence) && (!MachineControllerApplication.NoInitCheckPhotocell))
            ////GPF18
            //{
            //    // code block to be executed
            //    if (LowLevel.IO.DigitalInputs[(byte)DigitalInput.PhotocellMaterialPresence] == false)
            //    {
            //        bol01 = true;
            //    }
            //    else
            //    {
            //        bol01 = false;
            //        break;
            //    }
            //}
            //GPF12
            bool pmpEnabled = HighLevel.WorkingContext.Parameters.PhotocellMaterialPresenceEnabled;
            //GPI25
            //////bool pmp = LowLevel.IO.DigitalInputs[(byte)DigitalInput.PhotocellMaterialPresence];
            bool pmp = HighLevel.Status.PhotocelMaterialPresenceFiltered; 
            //bool pmp = !bol01;
            //GPF25

            HighLevel.Status.CradleInSync = pmpEnabled && pmp;

            Console.WriteLine($"TestConditionsAndPossiblyEnableSync({pmpEnabled && pmp})");

        }
        public void SetEnteringNormalStateSync()
        {
            //GPI12 sostituzione check temporale su PhotocellMaterialPresence:
            //bool bol01 = false;
            //if (LowLevel.IO.DigitalInputs[(byte)DigitalInput.PhotocellMaterialPresence] == false)
            //{
            //    bol01 = true;
            //}
            //else
            //{
            //    bol01 = false;
            //}
            //DateTime checkUntilPhotocellMaterialPresence = DateTime.MinValue;
            //checkUntilPhotocellMaterialPresence = DateTime.UtcNow + TimeSpan.FromMilliseconds(HighLevel.Settings.HighLevel.MachineParameters.CheckUntilPhotocellMaterialPresence);   //TimeSpan.FromMilliseconds(250) parametro da mettere nella Cradle per intervallo di check fotocellula presenza materiale
            ////GPI18
            //while ((DateTime.UtcNow < checkUntilPhotocellMaterialPresence) && (!MachineControllerApplication.NoInitCheckPhotocell))
            ////GPF18
            //{
            //    // code block to be executed
            //    if (LowLevel.IO.DigitalInputs[(byte)DigitalInput.PhotocellMaterialPresence] == false)
            //    {
            //        bol01 = true;
            //    }
            //    else
            //    {
            //        bol01 = false;
            //        break;
            //    }
            //}
            //GPF12
            bool pmpEnabled = HighLevel.WorkingContext.Parameters.PhotocellMaterialPresenceEnabled;
            //GPI25
            //////bool pmp = LowLevel.IO.DigitalInputs[(byte)DigitalInput.PhotocellMaterialPresence];
            bool pmp = HighLevel.Status.PhotocelMaterialPresenceFiltered;
            //bool pmp = !bol01;
            //GPF25

            //MMIx02
            bool prpEnabled = HighLevel.WorkingContext.Parameters.EnablePhotocellRollPresence && HighLevel.Settings.HighLevel.FunctionsEnabled.EnableFunctionPhotocellRollPresence.Value;//MMIx05
            bool prp = HighLevel.Status.PhotocelRollPresenceFiltered;

            //disattivo il sync se non c'è materiale
            if ((pmp == false && pmpEnabled == true) || (prp == false && prpEnabled == true))
            {
                HighLevel.Status.PromiseToDisableCradleInSync = true;
                HighLevel.Status.PromiseToEnableCradleInSync = false;
                HighLevel.Status.PromiseToEnableCradleInSyncAfterClick = false;
            }
            //MMFx02

            if (HighLevel.Status.ForceDisableCradleInSync || HighLevel.Status.PromiseToDisableCradleInSync)
            {
                SetSync(false);

                Console.WriteLine($"SetSync(false) PromiseToDisableCradleInSync:{HighLevel.Status.PromiseToDisableCradleInSync}");
                Console.WriteLine($"SetSync(false) ForceDisableCradleInSync:{HighLevel.Status.ForceDisableCradleInSync}");

                HighLevel.Status.PromiseToDisableCradleInSync = false;
                Console.WriteLine($"SetSync(true) PromiseToDisableCradleInSync:{HighLevel.Status.PromiseToDisableCradleInSync}");
            }
            else if (HighLevel.Status.PromiseToEnableCradleInSync)
            {
                SetSync(true);

                HighLevel.Status.PromiseToEnableCradleInSync = false;
                Console.WriteLine($"SetSync(true) PromiseToDisableCradleInSync:{HighLevel.Status.PromiseToDisableCradleInSync}");
            }
            else
            {
                SetSync(pmpEnabled && pmp);
                Console.WriteLine($"pmpEnabled:{pmpEnabled}, pmp:{pmp}");
            }
        }

        public void SetSync(bool sync)
        {
            if (HighLevel.Status.ForceDisableCradleInSync)
            {
                sync = false;
            }

            HighLevel.Status.CradleInSync = sync;

            SetLowLevelWorkingState();
        }

        public void SetLowLevelWorkingState()
        {
            bool sync = HighLevel.Status.CradleInSync;

            Console.WriteLine($"SetLowLevelWorkingState(sync={sync})");
            SetLowLevelStateToWaitCommand();

            //Invio parametri
            Communicator.SetMachineLowLevelSettings(
                HighLevel.Settings.LowLevelMotion,
                HighLevel.Settings.HighLevel.FunctionsEnabled,
                HighLevel.Settings.HighLevel.MachineParameters);
            Thread.Sleep(50);

            Communicator.SetScalingFactor(HighLevel.WorkingContext.Parameters.CradleScalingFactor);
            Thread.Sleep(50);

            Communicator.SetStraightRoller(HighLevel.WorkingContext.Parameters.StraightRoller);
            Thread.Sleep(50);

            var lowLevelState = Control.LowLevel.ControlState.WaitCommand;

            //MMIx10
            var precLowLevelState = lowLevelState;
            //MMFx10

            //Working Mode
            switch (HighLevel.WorkingContext.Parameters.WorkingMode)
            {
                case WorkingMode.Encoder:
                    lowLevelState = Control.LowLevel.ControlState.MotionEncoder;
                    break;

                case WorkingMode.EncoderDancebar:
                    lowLevelState = Control.LowLevel.ControlState.MotionEncoderDancer;
                    break;

                case WorkingMode.Dancebar:
                    lowLevelState = Control.LowLevel.ControlState.MotionDancer;
                    break;
            }

            //MMIx10
            if (lowLevelState != precLowLevelState)
            {
                Console.WriteLine($"CradleDevice.SetLowLevelState({lowLevelState.ToString()})");
            }
            //MMFx10

            //Setto stato
            Console.WriteLine($"CradleDevice.SetLowLevelState({lowLevelState.ToString()})");
            Communicator.SetLowLevelControlState(lowLevelState);
            Thread.Sleep(50);

            //Se sincronismo disabilitato, rimetto in stato di attesa comandi il controllore a basso livello
            if (HighLevel.Status.CradleInSync == false)
            {
                SetLowLevelStateToWaitCommand();
            }
        }
    }
}