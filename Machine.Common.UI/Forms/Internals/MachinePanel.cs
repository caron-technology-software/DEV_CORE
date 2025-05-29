using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Machine.UI.Common
{
    public partial class MachinePanel : Panel
    {
        public string CurrentFormShowing { get; set; } = String.Empty;

        public MachinePanel()
        {
            InitializeComponent();
        }
    }
}
