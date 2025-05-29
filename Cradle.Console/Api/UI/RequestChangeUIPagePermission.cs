using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

using ProRob;

namespace Caron.Cradle.Control.Api
{
    [RoutePrefix("change_ui_page_permission")]
    public class RequestChangeUIPagePermissionController : CradleApiController
    {
        [HttpGet]
        [Route("")]
        public bool ChangeUIPagePermission()
        {
            return MachineController.StateMachine.ChangeUIPagePermission;
        }
    }
}
