using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Machine.Models
{
    public class MessageBoxResult
    {
        public DateTime DialogOpened { get; set; }
        public DateTime DialogClosed { get; set; }
        public string DialogResult { get; set; }

        public MessageBoxResult()
        {
            //--
        }
    }
}
