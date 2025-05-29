#define ENABLED

#if ENABLED

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

using ProRob;
using ProRob.Log;
using ProRob.WebApi;
using TicToc = ProRob.WebApi.TicToc;

using Machine.Utility;

using Caron.Cradle.Control.Database;
using Caron.Cradle.Control.LowLevel;

namespace Caron.Cradle.Control.Api
{
    [RoutePrefix("debug")]
    public class DebugController : CradleApiController
    {
        [Route("enable_march")]
        [HttpGet]
        public void EnableMarch()
        {
            ProConsole.WriteLine($"[API] EnableMarch()", ConsoleColor.Yellow);

            MachineController.LowLevel.IO.MachineInputs[(byte)MachineInput.MarchEnabled] = true;
        }

        [Route("disable_march")]
        [HttpGet]
        public void DisableMarch()
        {
            ProConsole.WriteLine($"[API] DisableMarch()", ConsoleColor.Yellow);

            MachineController.LowLevel.IO.MachineInputs[(byte)MachineInput.MarchEnabled] = false;
        }

        [Route("enable_emergency")]
        [HttpGet]
        public void EnableEmergency()
        {
            ProConsole.WriteLine($"[API] EnableEmergency()", ConsoleColor.Yellow);

            MachineController.HighLevel.Errors.EmergencyStatus = true;
        }

        [Route("test_send_commands_to_low_level_control")]
        [HttpGet]
        public void TestSendCommandsToLowLevelControl()
        {
            ProConsole.WriteLine($"[API] TestSendCommandsToLowLevelControl()", ConsoleColor.Yellow);

            int n = 50;

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"Sending command {i}/{n}..");

                MachineController.Communicator.StopJog();
                MachineController.Communicator.SendHelloMessage();
                MachineController.Devices.Cradle.SetEnteringNormalStateSync();
                MachineController.Devices.ElectricDrives.StopAllActions();
            }
        }

        [Route("test_db")]
        [HttpGet]
        [TicToc]
        public void TestDb()
        {
            ProConsole.WriteLine($"[API] TestDb()", ConsoleColor.Yellow);

            int n = 10;

            var tasks = new List<Task>();

            for (int i = 0; i < n; i++)
            {
                var t = Task.Run(() =>
                {
                    DatabaseSettings.Update(MachineController.HighLevel.WorkingsSettings);

                });

                //tasks.Add(t);
            }

            //Task.WaitAll(tasks.ToArray());

            //MachineData.WaitAllOperations();
        }
    }
}
#endif