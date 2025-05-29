using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caron.FileFormats.CutTicketX
{  
    public partial class CutTicket
    {
        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"[CutTicket]");
            sb.AppendLine($"\tJobFile: {JobFile}");
            sb.AppendLine($"\t (...)");
            return sb.ToString();
        }
    }
}