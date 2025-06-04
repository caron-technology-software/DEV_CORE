using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using ProRob;

namespace Caron.Cradle.Control.Api
{
    [ApiController]
    [Route("change_ui_page_permission")]
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
