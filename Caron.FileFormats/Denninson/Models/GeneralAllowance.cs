using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Caron.FileFormats.Denninson
{
    //000 --------------------------------------------------------------
    //000 Order ID
    //000 ³        General Allowance Begin of spread
    //000 ³        ³    General Allowance end of spread
    //000 V V    V
    //007 PIPPO    0025 0025
    public class GeneralAllowance : IBuildFromParameters<GeneralAllowance>
    {
        public string OrderId { get; set; }
        public int BeginOfSpreader { get; set; }
        public int EndOfSpreader { get; set; }

        public GeneralAllowance()
        {
            //--
        }

        public GeneralAllowance(string orderId, int beginOfSpreader, int endOfSpreader)
        {
            OrderId = orderId;
            BeginOfSpreader = beginOfSpreader;
            EndOfSpreader = endOfSpreader;
        }

        public override string ToString()
        {
            return $"[GENERAL ALLOWANCE]\nOrderId={OrderId} BeginOfSpreader={BeginOfSpreader} EndOfSpreader={EndOfSpreader}\n";
        }

        public GeneralAllowance BuildFromParameters(string[] parameters)
        {
            GeneralAllowance generalAllowance = new GeneralAllowance();

            generalAllowance.OrderId = parameters[0];
            generalAllowance.BeginOfSpreader = int.Parse(parameters[1]);
            generalAllowance.EndOfSpreader = int.Parse(parameters[2]);

            return generalAllowance;
        }
    }
}

