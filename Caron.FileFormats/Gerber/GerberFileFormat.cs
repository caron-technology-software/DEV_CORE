using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using OpenCvSharp;

namespace Caron.FileFormats.Gerber
{
    public partial class GerberFileFormat
    {
        //public (List<Piece> knifePoints, List<Piece> penPoints, bool internationSystemOfUnits) ParseFile(string path)
        public (List<Piece> knifePoints, List<Piece> penPoints, GerberMeasurementSystem internationSystemOfUnits) ParseFile(string path)
        {
            var srcGerber = File.ReadAllText(path);
            var gerberCommands = srcGerber.Split('*');

            var cutFile = new CutFile();

            //MMIx16
            //cutFile.ParseGerberCommands(gerberCommands);//←originale
            var gerberFile = cutFile.ParseGerberCommands(gerberCommands);//parse come CPlanner
            //MMFx16

            // File gerber: 1/10 mm
            cutFile.RescaleAllPoints(10);

            (List<Piece> knifePoints, List<Piece> penPoints) = cutFile.GetKnifeAndPenPoints();
           
            //MMIx18
            if (gerberFile.UnitOfMeasureSystem == GerberMeasurementSystem.Imperial_1_100_Inch)//standard
            {
                GerberHelper.ApplyScale(knifePoints, 2.54f);//0.254
                GerberHelper.ApplyScale(penPoints, 2.54f);
            }
            else if (gerberFile.UnitOfMeasureSystem == GerberMeasurementSystem.Imperial_1_1000_Inch)//G70
            {
                GerberHelper.ApplyScale(knifePoints, 0.254f);//00.254
                GerberHelper.ApplyScale(penPoints, 0.254f);
            }
            else if (gerberFile.UnitOfMeasureSystem == GerberMeasurementSystem.Metric_1_10_Millimeters)//G71
            {
                GerberHelper.ApplyScale(knifePoints, 1f);//0.1 metri
                GerberHelper.ApplyScale(penPoints, 1f);
            }

            return (knifePoints, penPoints, gerberFile.UnitOfMeasureSystem);
            //MMFx18
        }

        public static List<LabelPoint> GetLabelPointsFromFile(string path)
        {
            var srcGerber = File.ReadAllText(path);
            var points = new List<LabelPoint>();

            //*X7292Y4676M31*Zone C*
            string pattern = @"X(\d+)Y(\d+)M31\*([\w ]+)\*";
            //string pattern = @"X(\d+)Y(\d+)\*M31\*([\w ]+)\*";

            var matches = Regex.Matches(srcGerber, pattern, RegexOptions.None);

            for (int i = 0; i < matches.Count; i++)
            {
                var groups = matches[i].Groups;

                int x = int.Parse(groups[1].ToString());
                int y = int.Parse(groups[2].ToString());
                string text = groups[3].ToString();

                if (string.IsNullOrEmpty(text) == false && string.IsNullOrWhiteSpace(text) == false)
                {
                    //Console.WriteLine($"[1]{x} [2]{y} [3]\"{text}\"");

                    points.Add(new LabelPoint(x / 10, y / 10, text));
                }
            }

            return points;
        }
    }
}
