using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;

using ProRob;
using ProRob.Extensions;

using Caron.Cradle.Control;
using Caron.Cradle.Control.LowLevel;
using Caron.Cradle.Control.DataCollections;

namespace Caron.Cradle.Control.HighLevel
{
    public partial class MachineController
    {
        internal void DataCollectionsUpdater()
        {
            TimeSeries.MachineStatusBag.AddData(HighLevel.Status);
            TimeSeries.MachineContextBag.AddData(HighLevel.WorkingContext);
        }
    }
}
