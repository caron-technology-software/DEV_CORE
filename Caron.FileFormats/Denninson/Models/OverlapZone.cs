using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Caron.FileFormats.Denninson
{
    //000 --------------------------------------------------------------
    //000 Number of overlapzone
    //000 ³   Cut point(mm)
    //000 ³   ³     Spread point(mm)
    //000 V V     V
    //014 001 00588 00421

    public class OverlapZone : IBuildFromParameters<OverlapZone>
    {
        public int OverlapZoneNumber { get; set; }
        public int CutPoint { get; set; }
        public int SpreadPoint { get; set; }

        public OverlapZone()
        {
            //--
        }

        public OverlapZone(int overlapZoneNumber, int cutPoint, int spreadPoint)
        {
            OverlapZoneNumber = overlapZoneNumber;
            CutPoint = cutPoint;
            SpreadPoint = spreadPoint;
        }

        public override string ToString()
        {
            return $"[OVERLAP ZONE]\nOverlapZoneNumber={OverlapZoneNumber} CutPoint={CutPoint} SpreadPoint={SpreadPoint}\n";
        }

        public OverlapZone BuildFromParameters(string[] parameters)
        {
            OverlapZone overlapZone = new OverlapZone();

            overlapZone.OverlapZoneNumber = int.Parse(parameters[0]);
            overlapZone.CutPoint = int.Parse(parameters[1]);
            overlapZone.SpreadPoint = int.Parse(parameters[2]);

            return overlapZone;
        }
    }
}

