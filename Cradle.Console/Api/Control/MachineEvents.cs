using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;

using ProRob;

using Caron.Cradle.Control.HighLevel;
using Caron.Cradle.Control.HighLevel.Settings;

using TicToc = ProRob.WebApi.TicToc;

namespace Caron.Cradle.Control.Api
{
    [ApiController]
    [Route("machine_events")]
    public class MachineEventsController : CradleApiController
    {
        [Route("")]
        [HttpGet]
        public IEnumerable<MachineEvent> GetMachineEvents(string fromDate = null, string toDate = null)
        {
            if (string.IsNullOrEmpty(fromDate))
            {
                fromDate = DateTime.Now.Date.ToString();
            }

            if (string.IsNullOrEmpty(toDate))
            {
                toDate = DateTime.Now.Date.ToString();
            }

            ProConsole.WriteLine($"[API] GetMachineEvents({fromDate},{toDate})", ConsoleColor.Yellow);

            var events = DatabaseWorkings.MachineEventsCollection
                .Query()
                .Where(x => x.Timestamp >= DateTime.Parse(fromDate).Date)
                .Where(x => x.Timestamp < DateTime.Parse(toDate).Date.AddDays(1))
                .OrderBy(x => x.Timestamp)
                .ToEnumerable();

            return events;
        }


        [Route("stats")]
        [HttpGet]
        public object GetMachineEventsStats(string fromDate = null, string toDate = null)
        {
            if (string.IsNullOrEmpty(fromDate))
            {
                fromDate = DateTime.Now.Date.ToString();
            }

            if (string.IsNullOrEmpty(toDate))
            {
                toDate = DateTime.Now.Date.ToString();
            }

            ProConsole.WriteLine($"[API] GetMachineEventsStats({fromDate},{toDate})", ConsoleColor.Yellow);

            var events = DatabaseWorkings.MachineEventsCollection
                .Query()
                .Where(x => x.Timestamp >= DateTime.Parse(fromDate).Date)
                .Where(x => x.Timestamp < DateTime.Parse(toDate).Date.AddDays(1))
                .ToEnumerable();


            //TODO: se ho START-START => START-END-START

            //-------------------------------------------------
            // MachinePowerUp - MachinePowerDown
            //-------------------------------------------------
            var power = events.Where(x => x.EventType == MachineEventType.MachinePowerUp ||
                                          x.EventType == MachineEventType.MachinePowerDown)
                                    .OrderBy(x => x.Timestamp);

            var totalPower = power.Zip(power.Skip(1), (x, y) => (y.Timestamp - x.Timestamp))
                                  .Aggregate(TimeSpan.Zero, (x, y) => x + y).TotalHours;

            //-------------------------------------------------
            // EnterSync - ExitSync
            //-------------------------------------------------
            var sync = events.Where(x => x.EventType == MachineEventType.EnterSync ||
                                         x.EventType == MachineEventType.ExitSync)
                             .OrderBy(x => x.Timestamp);

            var totalSync = sync.Zip(sync.Skip(1), (x, y) => y.Timestamp - x.Timestamp)
                                .Aggregate(TimeSpan.Zero, (x, y) => x + y).TotalHours;



            //-------------------------------------------------
            // StartCutOff - StopCutOff
            //-------------------------------------------------
            var cut = events.Where(x => x.EventType == MachineEventType.StartCutOff ||
                                        x.EventType == MachineEventType.StopCutOff)
                            .OrderBy(x => x.Timestamp);

            var totalCut = cut.Zip(cut.Skip(1), (x, y) => y.Timestamp - x.Timestamp)
                              .Aggregate(TimeSpan.Zero, (x, y) => x + y).TotalHours;


            //-------------------------------------------------
            // EnterLoadUnload - ExitLoadUnload
            //-------------------------------------------------
            var loadUnload = events.Where(x => x.EventType == MachineEventType.EnterLoadUnload ||
                                               x.EventType == MachineEventType.ExitLoadUnload)
                                   .OrderBy(x => x.Timestamp);

            var totalLoadUnload = loadUnload.Zip(loadUnload.Skip(1), (x, y) => y.Timestamp - x.Timestamp)
                                            .Aggregate(TimeSpan.Zero, (x, y) => x + y).TotalHours;


            //-------------------------------------------------
            // StartMovementInSync - StopMovementInSync
            //-------------------------------------------------
            var movementsInSync = events.Where(x => x.EventType == MachineEventType.StartMovementInSync ||
                                                    x.EventType == MachineEventType.StopMovementInSync)
                                        .OrderBy(x => x.Timestamp);

            var totalMovementsInSync = movementsInSync.Zip(movementsInSync.Skip(1), (x, y) => y.Timestamp - x.Timestamp)
                                                      .Aggregate(TimeSpan.Zero, (x, y) => x + y).TotalHours;

            return new MachineEventsStatistics()
            {
                TotalPowerOnTime = totalPower,
                TotalSyncTime = totalSync,
                TotalCutTime = totalCut,
                TotalLoadUnloadTime = totalLoadUnload,
                TotalMovementsInSyncTime = totalMovementsInSync
            };
        }
    }
}

