using System;
using System.Linq;
using System.Web.Http;
using System.Threading;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Contexts;

using Apex.Serialization;

using ProRob;
using ProRob.WebApi;

using Caron.Cradle.Control.HighLevel;

namespace Caron.Cradle.Control.Api
{
#if !TEST
    [IpAuthorize(Machine.Constants.Networking.IPAddressHighLevelControl)]
#endif

    [Synchronization]
    public class CradleApiController : ApiController
    {
        protected static MachineController MachineController { get; private set; }

        private readonly object lockerEncoding = new object();
        protected static readonly IBinary serializer;

        public static volatile int NumberOfRequests = 0;
        public static readonly DateTime StartUp;

        static CradleApiController()
        {
            StartUp = DateTime.Now;
            serializer = Binary.Create(new Settings());
            ProConsole.WriteLine("[CradleApiController] Initialization completed", ConsoleColor.DarkYellow);

            //GPI18
            MachineControllerApplication.NoInitCheckPhotocell = false;
            //GPF18

        }

        public CradleApiController()
        {
            NumberOfRequests++;
        }

        public static void SetMachineController(MachineController machineController)
        {
            MachineController = machineController;
        }

        protected byte[] EncodeData<T>(T obj)
        {
            try
            {

                lock (lockerEncoding)
                {
                    var stream = new System.IO.MemoryStream();

                    serializer.Write(obj, stream);

                    return stream.ToArray();
                }
            }
            catch
            {
                return null;
            }
        }
    }
}