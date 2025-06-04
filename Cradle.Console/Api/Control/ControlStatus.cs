using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using ProRob.WebApi;

using Caron.Cradle.Control;
using Caron.Cradle.Control.LowLevel;
using ProRob.Extensions.Object;

namespace Caron.Cradle.Control.Api
{
    [ApiController]
    [Route("control_status")]
    [Serializable]
    public class ControlStatusController : CradleApiController
    {
        [HttpGet]
        [Route("")]
        public ControlStatus GetControlStatus()
        {
            return new ControlStatus()
            {
                LowLevel = MachineController.LowLevel,
                HighLevel = MachineController.HighLevel
            };
        }

        [HttpGet]
        [Route("encoded")]
        public byte[] GetControlStatusEncoded()
        {
            var cs = new ControlStatus()
            {
                LowLevel = MachineController.LowLevel.JClone(),
                HighLevel = MachineController.HighLevel.JClone()
            };

            return JEncodeData<ControlStatus>(cs);
        }
    }

    [Route("high_level_control_status")]
    public class HighLevelStatusController : CradleApiController
    {
        [HttpGet]
        [Route("")]
        public Control.HighLevel.ControlStatus GetHighLevelControlStatus()
        {
            return MachineController.HighLevel;
        }
    }

    [Route("low_level_control_status")]
    public class LowLevelStatusController : CradleApiController
    {
        [HttpGet]
        [Route("")]
        public LowLevel.ControlStatus GetLowLevelControlStatus()
        {
            return MachineController.LowLevel;
        }

        [HttpGet]
        [Route("axes")]
        public MachineAxes GetLowLevelControlAxesStatus()
        {
            return MachineController.LowLevel.Axes;
        }
    }
}