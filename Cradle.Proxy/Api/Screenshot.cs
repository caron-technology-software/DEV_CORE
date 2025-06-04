using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

using ProRob;
using ProRob.Imaging;
using ProRob.WebApi;
using ProRob.WebApi.Auth;

namespace Cradle.Proxy
{
    [Route("screenshot")]
    public class ScreenshotController : ProxyApiController
    {
        [SwaggerIgnore]
        [HttpGet]
        [Route("grab_function")]
        public async Task<HttpResponseMessage> GrabScreenshot()
        {
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
        [Route("grab")]
        public async Task<HttpResponseMessage> GrabScreenshot(long t)
        {
            return await GrabScreenshot();
        }
    }
}