using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Machine.Models;
using Machine.UI.Controls;

namespace Caron.Cradle.Control.Api
{
    [ApiController]
    [Route("message")]
    public class MessageBoxController : CradleApiController
    {
        [HttpGet]
        [Route("")]
        public IActionResult Show(string title, string message)
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
