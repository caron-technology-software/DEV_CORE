#undef CONSOLE_LOG_EXCEPTION

using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using RestSharp;

using ProRob;

namespace Machine.UI.Communication
{
    public partial class Communicator
    {
        private static readonly object lockerInstance = new object();
        private static Communicator instance;

        private volatile object lockerSerializer = new object();

        private volatile Apex.Serialization.IBinary serializer;

        public static string IpAddress { get; private set; } = String.Empty;
        public static int Port { get; private set; } = 0;
        public static string ServerUri { get; private set; }
        public static bool Initialized { get; private set; } = false;
        public static TimeSpan DefaultTimeout { get; private set; }
        public static int Errors { get; private set; } = 0;

        static Communicator()
        {
            instance = new Communicator();
        }

        private Communicator()
        {
            var settings = new Apex.Serialization.Settings();
            serializer = Apex.Serialization.Binary.Create(settings);
        }

        public static Communicator Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockerInstance)
                    {
                        if (instance == null)
                        {
                            instance = new Communicator();
                        }
                    }
                }

                return instance;
            }
        }

        public static void Initialize(string ipAddress, int port, TimeSpan defaultTimeout)
        {
            IpAddress = ipAddress;
            Port = port;
            ServerUri = $"http://{IpAddress}:{Port}";
            DefaultTimeout = defaultTimeout;

            ProConsole.WriteLine("Communicator: initialized", ConsoleColor.Blue);

            Thread.Sleep(250);

            Initialized = true;
        }

        public static string SendHttpGetRequest(string route, string resource = "")
        {
            if (TrySendHttpGetRequest(route, out string json, resource, DefaultTimeout))
            {
                return json;
            }
            else
            {
                return string.Empty;
            }
        }

        public static void SendHttpGetRequestWithoutReturn(string route, string resource = "")
        {
            TrySendHttpGetRequest(route, out string json, resource, DefaultTimeout);        
        }

        public static bool TrySendHttpGetRequest(string route)
        {
            return TrySendHttpGetRequest(route, out string _, "", DefaultTimeout);
        }

        public static bool TrySendHttpGetRequest(string route, TimeSpan timeout)
        {
            return TrySendHttpGetRequest(route, out string _, "", timeout);
        }

        public static bool TrySendHttpGetRequest(string route, out string content, string resource, TimeSpan timeout)
        {
            if (Initialized == false)
            {
                string message = "[COMMUNICATOR] Communicator must be initialized";

                Console.WriteLine(message);
                throw new Exception(message);
            }

            content = String.Empty;

            try
            {
                //MMIx67 DISABILITA LA VERIFICA SSL SOLO PER TEST
                ServicePointManager.ServerCertificateValidationCallback +=
                    (sender, certificate, chain, sslPolicyErrors) => true;
                //MMFx67

                var options = new RestClientOptions($"http://{IpAddress}:{Port}/{route}/")
                {
                    Timeout = timeout
                };

                var client = new RestClient(options);

                var request = new RestRequest(resource, Method.Get);
                request.RequestFormat = DataFormat.Json;

                RestResponse response = client.Execute(request);

                if ((response.StatusCode != HttpStatusCode.OK) && (response.StatusCode != HttpStatusCode.NoContent))
                {
#if CONSOLE_LOG_EXCEPTION
                    Console.WriteLine($"GetData({route})->StatusCode: {response.ErrorMessage}");
#endif
                    return false;
                }

                content = response.Content;

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string SendHttpPostRequest(string route, object obj)
        {
            if (Initialized == false)
            {
                throw new Exception("Communicator must be initialized");
            }

            string json = string.Empty;

            try
            {
                // Usa RestClientOptions per specificare il timeout se necessario
                var client = new RestClient(new RestClientOptions($"http://{IpAddress}:{Port}/{route}/")
                {
                    Timeout = TimeSpan.FromSeconds(30) // puoi parametrizzare questo valore
                });

                // Crea la richiesta POST e specifica l'endpoint/resource
                var request = new RestRequest("", Method.Post); // "" indica la root del route

                // Aggiungi il corpo come oggetto, non come stringa JSON
                request.AddJsonBody(obj);

                // Esegui la richiesta
                var response = client.Execute(request);

                if ((response.StatusCode != HttpStatusCode.OK) && (response.StatusCode != HttpStatusCode.NoContent))
                {
#if CONSOLE_LOG_EXCEPTION
                    Console.WriteLine($"GetData({route})->StatusCode: {response.ErrorMessage}");
#endif
                    return json;
                }

                json = response.Content;
            }
            catch
            {
                //--
            }

            return json;
        }
    }
}

