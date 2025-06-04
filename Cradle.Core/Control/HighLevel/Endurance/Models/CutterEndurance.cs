using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Machine.Common;

namespace Caron.Cradle.Control.HighLevel.Settings
{
    public class CutterEndurance
    {
        [UserAccess(UserType.Manufacturer, UserType.Root)]
        public uint NumberOfCutOff { get; set; }

        public CutterEndurance()
        {
            //--
        }
    }
}
