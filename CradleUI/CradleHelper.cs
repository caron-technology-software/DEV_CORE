using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Machine.UI.Controls;

using Caron.Cradle.Control.LowLevel;
using Caron.Cradle.Control.HighLevel;

namespace Caron.Cradle.UI
{
    public class CradleHelper
    {
        private Supervisor Supervisor { get; set; }
        private Control.HighLevel.ControlStatus HighLevel => Supervisor.Control.HighLevel;
        private Control.LowLevel.ControlStatus LowLevel => Supervisor.Control.LowLevel;

        private bool[] DigitalInputs => LowLevel.IO.DigitalInputs;
        private bool[] MachineInputs => LowLevel.IO.MachineInputs;

        public CradleHelper(Supervisor supervisor)
        {
            Supervisor = supervisor;
        }

        public bool IsInMarch()
        {
            if (MachineInputs[(byte)MachineInput.MarchEnabled])
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsNotInMarch()
        {
            return !IsInMarch();
        }

        public bool IsCradleOutOfPosition()
        {
            if (HighLevel.Settings.HighLevel.MachineParameters.CradleOperationsWhenOutOfPosition.Value)
            {
                return false;
            }

            if (DigitalInputs[(byte)DigitalInput.LimitOverturningMotorSideLoad] == false ||
                DigitalInputs[(byte)DigitalInput.LimitOverturningOperatorSideLoad] == false)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckRewindConditions()
        {
#if TEST
            return true;
#else
            bool c1 = HighLevel.Settings.HighLevel.MachineParameters.FastRewindLockWithCradleOpen.Value == true;
            bool c2 = LowLevel.IO.DigitalInputs[(byte)DigitalInput.LimitOverturningMotorSideLoad] == false;
            bool c3 = LowLevel.IO.DigitalInputs[(byte)DigitalInput.LimitOverturningOperatorSideLoad] == false;

            if (c1 && (c2 || c3))
            {
                return false;
            }
            else
            {
                return true;
            }
#endif
        }

        public bool CheckCradleIsOutOfPositionAndShowPopUp()
        {
#if TEST
            return false;
#else
            bool cond = IsCradleOutOfPosition();

            if (cond)
            {
                MachineMessageBox.Show(Localization.Warning, Localization.CradleOutOfPosition);
            }

            return cond;
#endif

        }

        public bool CheckAutoAlignmentConditions()
        {
#if TEST
            return true;
#else
            //MMIx03
            if (!HighLevel.Settings.HighLevel.MachineParameters.AutoCenteringWithoutPhotocellAllineament)
            {
                if (HighLevel.WorkingContext.Parameters.PhotocellAlignmentEnabled == false)
                {
                    return false;
                }
            }
            //MMFx03

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
            //while (DateTime.UtcNow < checkUntilPhotocellMaterialPresence)
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
            //GPI25
            //////if (LowLevel.IO.DigitalInputs[(byte)DigitalInput.PhotocellMaterialPresence] == false)
            if (HighLevel.Status.PhotocelMaterialPresenceFiltered == false)
            //if (bol01)
            //GPF25
            {
                return false;
            }

            return true;
#endif
        }

        public bool CheckAutoCenteringConditions()
        {
#if TEST
            return true;
#else
            bool en = HighLevel.Settings.HighLevel.MachineParameters.AutoCenteringEnabled.Value == true;
            bool pcpme = HighLevel.WorkingContext.Parameters.PhotocellMaterialPresenceEnabled;
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
            //while (DateTime.UtcNow < checkUntilPhotocellMaterialPresence)
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
            //GPI25
            //////bool pcm = LowLevel.IO.DigitalInputs[(byte)DigitalInput.PhotocellMaterialPresence] == false;
            bool pcm = HighLevel.Status.PhotocelMaterialPresenceFiltered == false;
            //bool pcm = bol01;
            //GPF25

            return (en && pcpme && pcm);
#endif
        }

        public bool CheckOverturningConditions()
        {
#if TEST
            return true;
#else
            int en = HighLevel.Settings.HighLevel.MachineParameters.OverturningEnabledWithMaterialPresence.Value;
            bool pcpme = HighLevel.WorkingContext.Parameters.PhotocellMaterialPresenceEnabled;
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
            //while (DateTime.UtcNow < checkUntilPhotocellMaterialPresence)
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
            //GPI25
            //////bool pcm = DigitalInputs[(byte)DigitalInput.PhotocellMaterialPresence];
            bool pcm = HighLevel.Status.PhotocelMaterialPresenceFiltered;
            //bool pcm = !bol01;
            //GPF25

            bool cond = false;

            if (en == 0)
            {
                cond = true;
            }
            else if (en == 1 && pcpme == false)
            {
                cond = true;
            }
            else if (en == 1 && pcpme == true && pcm == false)
            {
                cond = true;
            }

            return cond;
#endif
        }

        public bool IsCutterOutOfPosition()
        {
#if TEST
            return true;
#else
            bool cond = true;

            Console.WriteLine($"IsCutterOutOfPosition: {DigitalInputs[(byte)DigitalInput.LimitCutterOperatorSide]} - {DigitalInputs[(byte)DigitalInput.LimitCutterMotorSide]}");

            if (Supervisor.Control.HighLevel.Settings.HighLevel.FunctionsEnabled.CutterPresence.Value == false)
            {
                return false;
            }

            if (DigitalInputs[(byte)DigitalInput.LimitCutterOperatorSide])
            {
                cond = false;
            }

            return cond;
#endif
        }

        public bool IsCradleJoggingOrCutting()
        {
            var cs = HighLevel.Status.HighLevelControlState;

            if (cs == Control.HighLevel.ControlState.CradleJog ||
                cs == Control.HighLevel.ControlState.CradleJogLoadUnload ||
                cs == Control.HighLevel.ControlState.CradleJogManualOperations ||
                cs == Control.HighLevel.ControlState.CradleRewind ||

                cs == Control.HighLevel.ControlState.CutOff ||
                cs == Control.HighLevel.ControlState.Sharpening)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckIfIsNotInMarchAndShowPopUp()
        {
            bool cond = IsNotInMarch();

            if (cond)
            {
                MachineMessageBox.Show(Localization.Warning, Localization.MachineNotInMarch);
            }

            return cond;
        }

        public bool CheckIfCutterIsOutOfPositionAndShowPopUp()
        {
            bool cond = IsCutterOutOfPosition();

            if (cond)
            {
                MachineMessageBox.Show(Localization.Warning, Localization.CutterOutOfPosition);
            }

            return cond;
        }
    }
}
