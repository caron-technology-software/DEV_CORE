using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Machine.UI.Controls
{
    public partial class MachineLabel : Label
    {
        public MachineLabel()
        {
            Font = new Font("Arial Rounded MT Bold", 16);

            InitializeComponent();
        }
    }
}
