using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Flurl;
using Flurl.Http;
using Flurl.Http.Configuration;

using ProRob;

namespace Machine.Tester
{
    public class UntrustedCertClientFactory : DefaultHttpClientFactory
    {
        public override HttpMessageHandler CreateMessageHandler()
        {
            return new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (a, b, c, d) => true
            };
        }
    }

    class Program
    {
        internal class QueryParam
        {
            public string Name { get; set; }
            public object Value { get; set; }

            public QueryParam(string name, object value)
            {
                Name = name;
                Value = value;
            }

            public static QueryParam Build(string name, object value)
            {
                return new QueryParam(name, value);
            }
        }

        internal static async Task<HttpResponseMessage> ProxyGetRequestAsync(string route, params QueryParam[] queryParams)
        {
            var uri = Machine.Constants.Networking.WebApiUri.AppendPathSegment(route);

            foreach (var param in queryParams)
            {
                uri = uri.SetQueryParam(param.Name, param.Value);
            };

            return (await uri.AllowAnyHttpStatus().GetAsync()).ResponseMessage;
        }

        internal static HttpResponseMessage ProxyGetRequest(string route, params QueryParam[] queryParams)
        {
            var uri = Machine.Constants.Networking.WebApiUri.AppendPathSegment(route);

            foreach (var param in queryParams)
            {
                uri = uri.SetQueryParam(param.Name, param.Value);
            };

            return uri.AllowAnyHttpStatus().GetAsync().Result.ResponseMessage;
        }

        static void Main(string[] args)
        {
            ProConsole.WriteLine("Machine Tester", ConsoleColor.Red);

            FlurlHttp.ConfigureClient(Machine.Constants.Networking.WebApiUri, cli => cli.Settings.HttpClientFactory = new UntrustedCertClientFactory());

            int test = 0;
            while (true)
            {
                ProConsole.WriteLine($"[{DateTime.Now}] TEST {++test}", ConsoleColor.Red);

                Console.WriteLine($"[{DateTime.Now}] Waiting machine control..");
                while (WaitUntilMachineControlIsUp() == false)
                {
                    Thread.Sleep(1000);
                }

                Console.WriteLine($"[{DateTime.Now}] Machine control is up..");
                Thread.Sleep(10000);

                Console.WriteLine($"[{DateTime.Now}] Restarting machine..");
                RestartMachine();
                Thread.Sleep(15000);

                Console.WriteLine($"\n\n");
            }
        }

        public static bool RestartMachine()
        {
            try
            {
                var response = Machine.Constants.Networking.WebApiProxyUriHttps
                    .WithBasicAuth("root", "31033")
                    .AppendPathSegment("application/reboot")
                    .GetAsync().Result;

                return response.ResponseMessage.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public static bool WaitUntilMachineControlIsUp()
        {
            try
            {
                bool res = Machine.Constants.Networking.WebApiProxyUriHttps
                    .WithBasicAuth("root", "31033")
                    .AppendPathSegment("signal/ui")
                    .GetJsonAsync<bool>().Result;

                return res;
            }
            catch
            {
                return false;
            }
        }
    }
}
