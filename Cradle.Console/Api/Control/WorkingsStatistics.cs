using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;

using ProRob;
using ProRob.Extensions.Object;
using ProRob.WebApi;

using Machine.Utility;

using Caron.Cradle.Control.HighLevel;
using Caron.Cradle.Control.HighLevel.Settings;

using TicToc = ProRob.WebApi.TicToc;
using Machine;

//------------------------------------------------
// XLS
//------------------------------------------------
//// var response = new HttpResponseMessage();
//// response.Content = new ByteArrayContent(ProRob.Documents.SpreadSheet.WriteXls());
//// response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

namespace Caron.Cradle.Control.Api
{
    [ApiController]
    [Route("workings_statistics")]
    public class WorkingsStatisticsController : CradleApiController
    {
        [Route("")]
        [HttpGet]
        public IEnumerable<Working> GetWorkingsStatistics(string fromDate = null, string toDate = null)
        {
            if (string.IsNullOrEmpty(fromDate))
            {
                fromDate = DateTime.Now.Date.ToString();
            }

            if (string.IsNullOrEmpty(toDate))
            {
                toDate = DateTime.Now.Date.ToString();
            }

            ProConsole.WriteLine($"[API] GetWorkingsStatistics({fromDate} {toDate})", ConsoleColor.Yellow);

            var fromDateTime = DateTime.Parse(fromDate).Date;
            var toDateTime = DateTime.Parse(fromDate).Date;

            var query = DatabaseWorkings.WorkingsCollection.Query()
                .Where(x => x.StartDateTime >= DateTime.Parse(fromDate).Date)
                .Where(x => x.StartDateTime < DateTime.Parse(toDate).Date.AddDays(1))
                .ToEnumerable();

            return query;
        }

        [Route("daily")]
        [TicToc]
        [HttpGet]
        public object GetDailyWorkingsStatistics(string fromDate = null, string toDate = null)
        {
            if (string.IsNullOrEmpty(fromDate))
            {
                fromDate = DateTime.Now.Date.ToString();
            }

            if (string.IsNullOrEmpty(toDate))
            {
                toDate = DateTime.Now.Date.ToString();
            }

            ProConsole.WriteLine($"[API] GetGroupByMaterialCode({fromDate} {toDate})", ConsoleColor.Yellow);

            var fromDateTime = DateTime.Parse(fromDate).Date;
            var toDateTime = DateTime.Parse(fromDate).Date;

            var res = DatabaseWorkings.WorkingsCollection
                    .Query()
                    .Where(x => x.StartDateTime >= DateTime.Parse(fromDate).Date)
                    .Where(x => x.StartDateTime < DateTime.Parse(toDate).Date.AddDays(1))
                    .ToEnumerable()
                    .GroupBy(
                    workings => workings.StartDateTime.Date,
                    workings => workings,
                    (day, workings) => new DailyWorkingsStatistics()
                    {
                        Day = day,

                        TotalMaterialsSpread = workings.Sum(x => x.MaterialSpread),
                        TotalTimeCounter = workings.Sum(x => x.TotalTimeCounter.TotalHours),
                        TotalCradleInSyncTimeCounter = workings.Sum(x => x.TotalCradleInSyncAndInMovementTimeCounter.TotalHours),
                        TotalTimeLoadUnloadCounter = workings.Sum(x => x.TotalTimeLoadUnloadCounter.TotalHours),

                        DistinctMaterials = workings.Select(x => x.Material).Distinct().Count(),
                        DistinctMaterialsCode = workings.Select(x => x.MaterialCode).Distinct().Count(),

                        NumberOfWorkings = workings.Count(),

                    }).OrderBy(x => x.Day).ToList();

            return res;
        }

        [Route("material")]
        [TicToc]
        [HttpGet]
        public object GetMaterialGroupByDay(string fromDate = null, string toDate = null)
        {
            if (string.IsNullOrEmpty(fromDate))
            {
                fromDate = DateTime.Now.Date.ToString();
            }

            if (string.IsNullOrEmpty(toDate))
            {
                toDate = DateTime.Now.Date.ToString();
            }

            ProConsole.WriteLine($"[API] GetMaterialGroupByDay({fromDate} {toDate})", ConsoleColor.Yellow);

            var fromDateTime = DateTime.Parse(fromDate).Date;
            var toDateTime = DateTime.Parse(fromDate).Date;

            var res = DatabaseWorkings.WorkingsCollection
                    .Query()
                    .Where(x => x.StartDateTime >= DateTime.Parse(fromDate).Date)
                    .Where(x => x.StartDateTime < DateTime.Parse(toDate).Date.AddDays(1))
                    .ToEnumerable()
                    .GroupBy(
                    workings => workings.Material,
                    workings => workings,
                    (material, workings) => new MaterialWorkingsStatistics()
                    {
                        Material = material,

                        TotalMaterialsSpread = workings.Sum(x => x.MaterialSpread),
                        TotalTimeCounter = workings.Sum(x => x.TotalTimeCounter.TotalHours),
                        TotalCradleInSyncTimeCounter = workings.Sum(x => x.TotalCradleInSyncAndInMovementTimeCounter.TotalHours),
                        TotalTimeLoadUnloadCounter = workings.Sum(x => x.TotalTimeLoadUnloadCounter.TotalHours),

                        DistinctMaterials = workings.Select(x => x.Material).Distinct().Count(),
                        DistinctMaterialsCode = workings.Select(x => x.MaterialCode).Distinct().Count(),

                        NumberOfWorkings = workings.Count(),

                    }).OrderBy(x => x.Material).ToList();

            return res;
        }

        [Route("material_code")]
        [TicToc]
        [HttpGet]
        public object GetMaterialCodeGroupByDay(string fromDate = null, string toDate = null)
        {
            if (string.IsNullOrEmpty(fromDate))
            {
                fromDate = DateTime.Now.Date.ToString();
            }

            if (string.IsNullOrEmpty(toDate))
            {
                toDate = DateTime.Now.Date.ToString();
            }

            ProConsole.WriteLine($"[API] GetMaterialCodeGroupByDay({fromDate} {toDate})", ConsoleColor.Yellow);

            var fromDateTime = DateTime.Parse(fromDate).Date;
            var toDateTime = DateTime.Parse(fromDate).Date;

            var res = DatabaseWorkings.WorkingsCollection
                    .Query()
                    .Where(x => x.StartDateTime >= DateTime.Parse(fromDate).Date)
                    .Where(x => x.StartDateTime < DateTime.Parse(toDate).Date.AddDays(1))
                    .ToEnumerable()
                    .GroupBy(
                    workings => workings.MaterialCode,
                    workings => workings,
                    (material, workings) => new MaterialCodeWorkingsStatistics
                    {
                        MaterialCode = material,

                        TotalMaterialsSpread = workings.Sum(x => x.MaterialSpread),
                        TotalTimeCounter = workings.Sum(x => x.TotalTimeCounter.TotalHours),
                        TotalCradleInSyncTimeCounter = workings.Sum(x => x.TotalCradleInSyncAndInMovementTimeCounter.TotalHours),
                        TotalTimeLoadUnloadCounter = workings.Sum(x => x.TotalTimeLoadUnloadCounter.TotalHours),

                        DistinctMaterials = workings.Select(x => x.Material).Distinct().Count(),
                        DistinctMaterialsCode = workings.Select(x => x.MaterialCode).Distinct().Count(),

                        NumberOfWorkings = workings.Count(),

                    }).OrderBy(x => x.MaterialCode).ToList();

            return res;
        }
    }
}

