using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Caron.FileFormats.Denninson
{
    //000 --------------------------------------------------------------
    //000 Order ID
    //000 ³        Splice Allowance Begin of spread
    //000 ³        ³    Splice Allowance end of spread
    //000 V V    V
    //118 PIPPO    0025 0025
    public class SpliceAllowance : IBuildFromParameters<SpliceAllowance>
    {
        public string OrderId { get; set; }
        public int BeginOfSpreader { get; set; }
        public int EndOfSpreader { get; set; }

        public SpliceAllowance()
        {
            //--
        }

        public SpliceAllowance(string orderId, int beginOfSpreader, int endOfSpreader)
        {
            OrderId = orderId;
            BeginOfSpreader = beginOfSpreader;
            EndOfSpreader = endOfSpreader;
        }

        public override string ToString()
        {
            return $"[SPLICE ALLOWANCE]\nOrderId={OrderId} BeginOfSpreader={BeginOfSpreader} EndOfSpreader={EndOfSpreader}\n";
        }

        public SpliceAllowance BuildFromParameters(string[] parameters)
        {
            SpliceAllowance spliceAllowance = new SpliceAllowance();

            spliceAllowance.OrderId = parameters[0];
            spliceAllowance.BeginOfSpreader = int.Parse(parameters[1]);
            spliceAllowance.EndOfSpreader = int.Parse(parameters[2]);

            return spliceAllowance;
        }
    }
}

