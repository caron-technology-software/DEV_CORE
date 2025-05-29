using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

using ProRob.WebApi.Auth;

using Caron.Cradle.Control.HighLevel.Settings;
using System.Net;
using System.Net.Http;
using ProRob.WebApi;

namespace Cradle.Proxy.Api
{
    [BasicAuthorization]
    [RoutePrefix("workings_settings")]
    public class WorkingsSettingsController : ProxyApiController
    {
        [HttpGet]
        [Route("")]
        public HttpResponseMessage GetWorkingsSettings()
        {
            return ProxyGetRequest("workings_settings");

        }

        [HttpGet]
        [Route("current")]
        public HttpResponseMessage GetCurrentWorkingsSettings()
        {
            return ProxyGetRequest("workings_settings/current");
        }

        [HttpGet]
        [Route("names")]
        public HttpResponseMessage GetWorkingsSettingsNames()
        {
            return ProxyGetRequest("workings_settings/names");
        }

        [HttpGet]
        [Route("by_name")]
        public HttpResponseMessage GetWorkingsSettingsByName(string name)
        {
            return ProxyGetRequest("workings_settings", QueryParam.Build("name", name));
        }

        [HttpDelete]
        [Route("")]
        public HttpResponseMessage DeleteWorkingSettings(string name)
        {
            return ProxyGetRequest("workings_settings/delete", QueryParam.Build("name", name));
        }

        [HttpGet]
        [Route("rename")]
        public HttpResponseMessage RenameWorkingsSettings(string name, string newName)
        {
            return ProxyGetRequest("workings_settings/rename", QueryParam.Build("name", name), QueryParam.Build("newName", newName));
        }

        [HttpPost]
        [Route("")]
        public HttpResponseMessage AddWorkingSetting([FromBody] WorkingSetting workingSetting)
        {
            return ProxyPostRequest("workings_settings/add", workingSetting);
        }

        [HttpGet]
        [Route("save")]
        public HttpResponseMessage SaveWorkingSettings()
        {
            return ProxyGetRequest("workings_settings/save");
        }

        [HttpGet]
        [Route("apply")]
        public HttpResponseMessage ApplyWorkingSettings(string name)
        {
            return ProxyGetRequest("workings_settings/apply", QueryParam.Build("name", name));
        }

        [HttpGet]
        [Route("reset")]
        public HttpResponseMessage ResetCurrentWorkingSettings()
        {
            return ProxyGetRequest("workings_settings/reset");
        }
    }
}
