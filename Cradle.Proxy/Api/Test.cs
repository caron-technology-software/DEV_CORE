#if TEST
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;

//using Markdig;

using ProRob;
using ProRob.Imaging;
using ProRob.WebApi;

namespace Cradle.Proxy
{
    [RoutePrefix("test")]
    public class TestController : ProxyApiController
    {
        static int Counter = 0;

        //[HttpGet]
        //[Route("mark")]
        //public HttpResponseMessage Mark()
        //{

        //    // Configure the pipeline with all advanced extensions active
        //    var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
        //    var result = Markdown.ToHtml("This is a text with some *emphasis*", pipeline);

        //    var response = new HttpResponseMessage();
        //    response.Content = new StringContent(result);
        //    response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
        //    return response;
        //}

        [HttpGet]
        [Route("wait")]
        public IHttpActionResult Wait(int milliseconds)
        {
            Console.WriteLine($"[Wait {milliseconds} ms]");

            Thread.Sleep(milliseconds);

            return Ok();
        }

        [HttpGet]
        [Route("mouse")]
        public IHttpActionResult Mouse(int x, int y)
        {
            Console.WriteLine($"[Mouse] {x} {y}");

            Task.Run(() =>
            {
                ProRob.Devices.Mouse.ExecuteClick(x, y);
            });

            return Ok();
        }

        [SwaggerIgnore]
        [HttpGet]
        [Route("screenshot_function")]
        public async Task<HttpResponseMessage> Screenshot()
        {
            Counter++;
            if (Counter % 25 == 0)
            {
                Console.WriteLine($"[{DateTime.Now}] Counter:{Counter}");
            }

            var imageFormat = ImageFormat.Png;

            return await Task.Run(() =>
            {
                Thread.CurrentThread.Priority = ThreadPriority.Lowest;

                var byteArray = ScreenCapture.CaptureFullScreen(imageFormat, 100L);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(byteArray)
                };
                response.Content.Headers.ContentType = new MediaTypeHeaderValue(ScreenCapture.GetMediaTypeHeader(imageFormat));

                return response;
            });
        }

        [HttpGet]
        [Route("screenshot")]
        public async Task<HttpResponseMessage> Screenshot(long t)
        {
            return await Screenshot();
        }
    }
}
#endif