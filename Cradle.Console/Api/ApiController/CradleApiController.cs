using Microsoft.AspNetCore.Mvc;
using ProRob;
using System;
using System.IO;
using Caron.Cradle.Control.LowLevel;
using Apex.Serialization;
using Caron.Cradle.Control.HighLevel;
using System.Text;
using System.Text.Json;

namespace Caron.Cradle.Control.Api
{
    public class CradleApiController : ControllerBase
    {
        protected static MachineController MachineController { get; private set; }

        private readonly object lockerEncoding = new object();
        protected static readonly IBinary serializer;

        public static volatile int NumberOfRequests = 0;
        public static readonly DateTime StartUp;

        static CradleApiController()
        {
            StartUp = DateTime.Now;

            var settings = new Settings();
            settings.MarkSerializable(typeof(ControlStatus));
            settings.MarkSerializable(typeof(Caron.Cradle.Control.HighLevel.ControlStatus));
            settings.MarkSerializable(typeof(Caron.Cradle.Control.LowLevel.ControlStatus));
            settings.MarkSerializable(typeof(ProRob.Extensions.Object.ObjectExtensions));
            settings.MarkSerializable(typeof(Caron.MachineConfiguration));

            serializer = Binary.Create(settings);

            ProConsole.WriteLine("[CradleApiController] Initialization completed", ConsoleColor.DarkYellow);

            // GPI18
            MachineControllerApplication.NoInitCheckPhotocell = false;
            // GPF18
        }

        public CradleApiController()
        {
            NumberOfRequests++;
        }

        public static void SetMachineController(MachineController machineController)
        {
            MachineController = machineController;
        }

        protected byte[] JEncodeData<T>(T obj)
        {
            try
            {
                var json = JsonSerializer.Serialize(obj);
                return Encoding.UTF8.GetBytes(json);
            }
            catch (Exception ex)
            {
                ProConsole.WriteLine("[ENCODED DATA] " + ex.Message);
                return null;
            }
        }

        protected byte[] EncodeData<T>(T obj)
        {
            try
            {
                lock (lockerEncoding)
                {
                    var stream = new MemoryStream();
                    serializer.Write(obj, stream);
                    return stream.ToArray();
                }
            }
            catch(Exception ex)
            {
                ProConsole.WriteLine(@"[ENCODED DATA]" + ex.Message);
                return null;
            }
        }
    }
}