using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Machine.DataCollections;

namespace Caron.Cradle.Control.DataCollections
{
    public class TimeSeries
    {
        protected internal MachineDataBag<long> LowLevelCycleTicksBag { get; set; } = new MachineDataBag<long>(Machine.Constants.Intervals.DataCollections.RefreshRate, Machine.Constants.Intervals.DataCollections.SamplingWindow);
        protected internal MachineDataBag<HighLevel.MachineStatus> MachineStatusBag { get; set; } = new MachineDataBag<HighLevel.MachineStatus>(Machine.Constants.Intervals.DataCollections.RefreshRate, Machine.Constants.Intervals.DataCollections.SamplingWindow);
        protected internal MachineDataBag<HighLevel.WorkingContext> MachineContextBag { get; set; } = new MachineDataBag<HighLevel.WorkingContext>(Machine.Constants.Intervals.DataCollections.RefreshRate, Machine.Constants.Intervals.DataCollections.SamplingWindow);
        protected internal MachineDataBag<LowLevel.ControlStatus> LowLevelControlStatusBag { get; set; } = new MachineDataBag<LowLevel.ControlStatus>(Machine.Constants.Intervals.DataCollections.RefreshRate, Machine.Constants.Intervals.DataCollections.SamplingWindow);

        public List<MachineDataElement<long>> LowLevelCycleTicks
        {
            get
            {
                var ticks = LowLevelCycleTicksBag.GetData();

                return ticks.Skip(1).Select((x, index) =>
                    new MachineDataElement<long>()
                    {
                        Timestamp = x.Timestamp,
                        Value = x.Value - ticks.ElementAt(index).Value
                    }).ToList();
            }
        }

        public List<MachineDataElement<HighLevel.MachineStatus>> MachineStatus => MachineStatusBag.GetData();
        public List<MachineDataElement<HighLevel.WorkingContext>> MachineContext => MachineContextBag.GetData();
        public List<MachineDataElement<LowLevel.ControlStatus>> LowLevelControlStatus => LowLevelControlStatusBag.GetData();
    }
}