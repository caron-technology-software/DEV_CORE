using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Caron.FileFormats.Denninson
{
    //000 --------------------------------------------------------------
    //000 Order ID
    //000 ³        Spread Nr.
    //000 ³        ³   Step Nr.
    //000 ³        ³   ³   Section from
    //000 ³        ³   ³   ³   Section to
    //000 ³        ³   ³   ³   ³   Nr.of ply
    //000 ³        ³   ³   ³   ³   ³   Color ID
    //000 ³        ³   ³   ³   ³   ³   ³                         Roll ID
    //000 ³        ³   ³   ³   ³   ³   ³                         ³                    Roll Destination
    //000 ³        ³   ³   ³   ³   ³   ³                         ³                    ³   Parameter-Set
    //000 ³        ³   ³   ³   ³   ³   ³                         ³                    ³   ³ Cloth Allowance
    //000 ³        ³   ³   ³   ³   ³   ³                         ³                    ³   ³ ³    Spreading method
    //000 ³        ³   ³   ³   ³   ³   ³                         ³                    ³   ³ ³    ³  Roll Length
    //000 ³        ³   ³   ³   ³   ³   ³                         ³                    ³   ³ ³    ³  ³      All.Beg
    //000 ³        ³   ³   ³   ³   ³   ³                         ³                    ³   ³ ³    ³  ³      ³    Al.E
    //000 V        V   V   V   V   V   V   V        V            V   V V    V V       V
    //002 PIPPO    001 001 001 040 001 RED R123     1     0000 EW 120000 0000 0000
    public class Step : IBuildFromParameters<Step>
    {
        public string OrderID { get; set; }
        public int SpreaderNumber { get; set; }
        public int StepNumber { get; set; }
        public int SectionFrom { get; set; }
        public int SectionTo { get; set; }
        public int NumberOfPly { get; set; }
        //GPIx223
        public int NumberOfPlyesDone { get; set; }
        //GPFx223
        public string ColorId { get; set; }
        public string RollId { get; set; }

        public Step()
        {
            //--
        }

        public Step(string orderId, int spreaderNumber, int stepNumber, int sectionFrom, int sectionTo, int numberOfPly, string colorId, string rollId)
        {
            OrderID = orderId;
            SpreaderNumber = spreaderNumber;
            StepNumber = stepNumber;
            SectionFrom = sectionFrom;
            SectionTo = sectionTo;
            NumberOfPly = numberOfPly;
            ColorId = colorId;
            RollId = rollId;
        }

        public override string ToString()
        {
            return $"[STEP]\nOrderID={OrderID} SpreaderNumber={SpreaderNumber} SectionFrom={SectionFrom} SectionTo={SectionTo} NumberOfPly={NumberOfPly} ColorId={ColorId} RollId={RollId}\n";
        }

        public Step BuildFromParameters(string[] parameters)
        {
            Step step = new Step();

            step.OrderID = parameters[0];
            step.SpreaderNumber = int.Parse(parameters[1]);
            step.StepNumber = int.Parse(parameters[2]);
            step.SectionFrom = int.Parse(parameters[3]);
            step.SectionTo = int.Parse(parameters[4]);
            step.NumberOfPly = int.Parse(parameters[5]);
            //GPIx223
            //public int NumberOfPlyesDone { get; set; }
            step.NumberOfPlyesDone = 0;
            //GPFx223
            step.ColorId = parameters[6];
            step.RollId = parameters[7];

            return step;
        }
    }
}

