using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web.Http;

using ProRob.WebApi;
using ProRob.Extensions.Object;

using Machine.DataCollections;

using Caron.Cradle.Control.DataCollections;
using Caron.Cradle.Control.HighLevel;

namespace Caron.Cradle.Control.Api
{
    [RoutePrefix("time_series")]
    public class TimeSeriesController : CradleApiController
    {
        public TimeSeriesController()
        {
            Thread.CurrentThread.Priority = ThreadPriority.Lowest;
        }

        [HttpGet]
        [Route("")]
        public TimeSeries GetDataCollections()
        {
            return MachineController.TimeSeries.Clone();
        }

        [HttpGet]
        [Route("encoded")]
        public byte[] GetDataCollectionsEncoded()
        {
            return EncodeData(MachineController.TimeSeries);
        }

        [HttpGet]
        [Route("machine_status")]
        public List<MachineDataElement<MachineStatus>> GetMachineStatus()
        {
            return MachineController.TimeSeries.MachineStatus.Clone();
        }

        [HttpGet]
        [DeflateCompression]
        [Route("machine_status/compressed")]
        public List<MachineDataElement<MachineStatus>> GetMachineStatusCompressed()
        {
            return MachineController.TimeSeries.MachineStatus.Clone();
        }

        [HttpGet]
        [Route("machine_status/encoded")]
        public byte[] GetMachineStatusEncoded()
        {
            return EncodeData(MachineController.TimeSeries.MachineStatus);
        }

        [HttpGet]
        [Route("working_context")]
        public List<MachineDataElement<WorkingContext>> GetMachineContext()
        {
            return MachineController.TimeSeries.MachineContext.Clone();
        }

        [HttpGet]
        [Route("working_context/encoded")]
        public byte[] GetMachineContextEncoded()
        {
            return EncodeData(MachineController.TimeSeries.MachineContext);
        }

        [HttpGet]
        [Route("low_level_control_status")]
        public List<MachineDataElement<LowLevel.ControlStatus>> GetLowLevelControlStatus()
        {
            return MachineController.TimeSeries.LowLevelControlStatus.Clone();
        }

        [HttpGet]
        [Route("low_level_control_status/encoded")]
        public byte[] GetLowLevelControlStatusEncoded()
        {
            return EncodeData(MachineController.TimeSeries.LowLevelControlStatus);
        }

        [HttpGet]
        [Route("plot_time_series")]
        public PlotsTimeSeries GetPlotsTimeSeries()
        {
            var dc = MachineController.TimeSeries.LowLevelControlStatus;

            var data = new PlotsTimeSeries()
            {
                Timestamp = dc.Select(x => x.Timestamp).ToArray(),

                CradlePosition = dc.Select(x => x.Value.Axes.Cradle.Position).ToArray(),
                CradleVelocity = dc.Select(x => x.Value.Axes.Cradle.Velocity).ToArray(),
                CradlePositionError = dc.Select(x => x.Value.Axes.Cradle.PositionError).ToArray(),
                CradleMotorSpeedCommand = dc.Select(x => (float)x.Value.Axes.Cradle.DriverCommandSpeed).ToArray(),
                CradleProportionalAction = dc.Select(x => x.Value.Axes.Cradle.ProportionalAction).ToArray(),
                CradleIntegralAction = dc.Select(x => x.Value.Axes.Cradle.IntegrativeAction).ToArray(),
                CradleDerivativeAction = dc.Select(x => x.Value.Axes.Cradle.DerivativeAction).ToArray(),
                CradleFeedForwardAction = dc.Select(x => x.Value.Axes.Cradle.FeedForwardAction).ToArray(),

                TablePosition = dc.Select(x => x.Value.Axes.Table.Position).ToArray(),

                TableVelocity = dc.Select(x => x.Value.Axes.Table.Velocity).ToArray(),

                DancerNormalizedValue = dc.Select(x => x.Value.Axes.Dancer.NormalizedValue).ToArray(),
            };

            return data;
        }
    }
}