using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Machine.UI.Common;

namespace Caron.Cradle.UI
{
    public class UiPanelsCollection
    {
        public MachinePanel TopBar { get; set; }
        public MachinePanel Menu { get; set; }
        public MachinePanel ViewFull { get; set; }
        public MachinePanel ViewCentered { get; set; }
    }

    public partial class Supervisor
    {
        public void SetPanels(
             MachinePanel topBar,
             MachinePanel menu,
             MachinePanel viewFull,
             MachinePanel viewCentered)
        {
            UI.Panels.TopBar = topBar;
            UI.Panels.Menu = menu;
            UI.Panels.ViewFull = viewFull;
            UI.Panels.ViewCentered = viewCentered;

            UI.PanelsInitilized = true;
        }
    }
}
