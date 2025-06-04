using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Machine.Common;

namespace Caron.Cradle.Control.HighLevel.Settings
{
    public class MachineWorkingHours
    {
        [UserAccess(UserType.Manufacturer, UserType.Root)]
        public double PowerOnHours { get; set; }

        [UserAccess(UserType.Root)]
        public double WorkingFakeHours { get; set; }

        [UserAccess(UserType.Manufacturer, UserType.Root)]
        public double WorkingWithCradleInSyncHours { get; set; }

        [UserAccess(UserType.Manufacturer, UserType.Manufacturer)]
        public double MachineMaintenanceHours { get; set; }
    }
}
