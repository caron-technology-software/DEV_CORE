using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LiteDB;

namespace Caron.Cradle.Control.HighLevel
{
    public class Working
    {
        [BsonId]
        public Guid Guid { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime StopDateTime { get; set; }
        public string WorkingName { get; set; } = string.Empty;
        public string Material { get; set; } = string.Empty;
        public string MaterialCode { get; set; } = string.Empty;

        public double MaterialSpread { get; set; } //[mm]
        public TimeSpan TotalCradleInSyncAndInMovementTimeCounter { get; set; }
        public TimeSpan TotalTimeCounter { get; set; }
        public TimeSpan TotalTimeLoadUnloadCounter { get; set; }

        public Working()
        {
            Guid = Guid.NewGuid();
        }

        public override string ToString()
        {
            var materialSpread = (MaterialSpread / 1000.0).ToString("0.00");

            var tc = TotalTimeCounter;
            var stc = $"{tc.Hours}:{tc.Minutes}:{tc.Seconds}";

            var tcsm = TotalCradleInSyncAndInMovementTimeCounter;
            var stcsm = $"{tcsm.Hours}:{tcsm.Minutes}:{tcsm.Seconds}";

            var tclu = TotalTimeLoadUnloadCounter;
            var stclu = $"{tclu.Hours}:{tclu.Minutes}:{tclu.Seconds}";

            var sb = new StringBuilder();

            sb.AppendLine($"{Localization.Code}: {Guid}");
            sb.AppendLine($"{Localization.WorkingName}: {WorkingName}");
            sb.AppendLine($"{Localization.Material}: {Material}");
            sb.AppendLine($"{Localization.MaterialCode}: {MaterialCode}");
            sb.AppendLine($"{Localization.StartDate}: {StartDateTime}");

            if (StopDateTime != default)
            {
                sb.AppendLine($"{Localization.StopDate}: {StopDateTime}");
            }

            sb.AppendLine($"{Localization.MaterialSpread}: {materialSpread} m");
            sb.AppendLine($"{Localization.TotalTime}: {stc}");
            sb.AppendLine($"{Localization.TotalWorkingTime}: {stcsm}");
            sb.AppendLine($"{Localization.TotalLoadUnloadTime}: {stclu}");

            return sb.ToString();
        }
    }
}
