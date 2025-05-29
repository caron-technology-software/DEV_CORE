using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Machine.Models;
using Machine.UI.Controls;

namespace Caron.Cradle.Control.Api
{
    [RoutePrefix("message")]
    public class MessageBoxController : CradleApiController
    {
        [HttpGet]
        [Route("")]
        public IHttpActionResult Show(string title, string message)
        {
            var dialogOpened = DateTime.Now;

            var msgBox = new MachineMessageBoxFullScreen(title, message);
            msgBox.ShowDialog();

            var dialogClosed = DateTime.Now;

            var result = new MessageBoxResult { DialogOpened = dialogOpened, DialogClosed = dialogClosed, DialogResult = msgBox.DialogResult.ToString() };

            return Ok(result);
        }
    }
}
