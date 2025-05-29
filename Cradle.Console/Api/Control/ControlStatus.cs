using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

using ProRob.WebApi;

using Caron.Cradle.Control;
using Caron.Cradle.Control.LowLevel;
using ProRob.Extensions.Object;

namespace Caron.Cradle.Control.Api
{
    [RoutePrefix("control_status")]
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
                LowLevel = MachineController.LowLevel.Clone(),
                HighLevel = MachineController.HighLevel.Clone()
            };

            return EncodeData<ControlStatus>(cs);
        }
    }

    [RoutePrefix("high_level_control_status")]
    public class HighLevelStatusController : CradleApiController
    {
        [HttpGet]
        [Route("")]
        public Control.HighLevel.ControlStatus GetHighLevelControlStatus()
        {
            return MachineController.HighLevel;
        }
    }

    [RoutePrefix("low_level_control_status")]
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