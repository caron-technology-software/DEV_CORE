using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Machine.Common;
using Machine.UI.Controls;

namespace Machine
{
    public class AccessControl
    {
        public static void SetEditPermission(MachineEditableItem item, UserType currentUser, UserType userType)
        {
            if (currentUser >= userType)
            {
                item.IsPropertyEditable = true;
            }
            else
            {
                item.IsPropertyEditable = false;
            }
        }
    }

    public static class AccessControlExtensions
    {
        public static void SetEditPermission(this MachineEditableItem item, UserType currentUser, UserType userType)
        {
            AccessControl.SetEditPermission(item, currentUser, userType);
        }
    }
}
