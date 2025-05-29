using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caron.FileFormats.CutTicketX
{
    public class CutTicketReport
    {
        private DateTime startDateTime;
        private DateTime stopDateTime;

        public string JobName { get; set; }
        public string SpreaderID { get; set; }
        public string StartDate { get; set; }
        public string StartTime { get; set; }
        public string EndDate { get; set; }
        public string EndTime { get; set; }
        public TimeSpan SpreadTime { get; set; } = TimeSpan.Zero;
        public TimeSpan IdleTime { get; set; } = TimeSpan.Zero;
        public TimeSpan SuspendedTime { get; set; } = TimeSpan.Zero;
        public TimeSpan TotalTime { get => SpreadTime + IdleTime + SuspendedTime; }
        public int PlannedPlies { get; set; } = 0;
        public int PliesSpread { get; set; } = 0;
        public double PlannedMaterialUsage { get; set; } = 0;
        public double MaterialSpread { get; set; } = 0;
        public double MaterialWaste { get; set; } = 0;
        public double TotalMaterialUsed { get => MaterialSpread + MaterialWaste; }

        public CutTicketReport()
        {
            //--
        }

        public void SetStartWorking(DateTime dateTime)
        {
            startDateTime = dateTime;

            StartDate = dateTime.ToShortDateString();
            StartTime = dateTime.ToShortTimeString();
        }

        public void SetEndWorking(DateTime dateTime)
        {
            stopDateTime = dateTime;

            EndDate = dateTime.ToShortDateString();
            EndTime = dateTime.ToShortTimeString();
        }

        public TimeSpan GetWorkingTimeSpan()
        {
            return stopDateTime - stopDateTime;
        }

        public static CutTicketReport BuildFromCSV(string text)
        {
            var report = new CutTicketReport();

            string[] rows = text.Split(new char[] { '\n', '\r' });

            string[] items = rows[rows.Count() == 2 ? 1 : 2].Split(new char[] { ',' });

            report.JobName = items[0];
            report.SpreaderID = items[1];
            report.StartDate = items[2];
            report.StartTime = items[3];
            report.EndDate = items[4];
            report.EndTime = items[5];
            report.SpreadTime = TimeSpan.Parse(items[6]);
            report.IdleTime = TimeSpan.Parse(items[7]);
            report.SuspendedTime = TimeSpan.Parse(items[8]);
            //report.TotalTime = TimeSpan.Parse(items[9]);
            report.PlannedPlies = int.Parse(items[10]);
            report.PliesSpread = int.Parse(items[11]);
            report.PlannedMaterialUsage = double.Parse(items[12].Replace(".", ","));
            report.MaterialSpread = double.Parse(items[13].Replace(".", ","));
            report.MaterialWaste = double.Parse(items[14].Replace(".", ","));
            //report.TotalMaterialUsed = double.Parse(items[15].Replace(".",","));

            return report;
        }
    }
}
