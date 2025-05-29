using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

using Flurl;
using Flurl.Http;

namespace Machine.Control.HighLevel
{
    public static class SimpleCommunicator
    {
        public static int Errors { get; private set; } = 0;

        public static bool CheckControlReady()
        {
            try
            {
                string result = new Url(Constants.Networking.WebApiUri)
                    .AppendPathSegment("signal")
                    .AppendPathSegment("control").GetJsonAsync<string>().Result;

                return String.Equals(result, "true");
            }
            catch
            {
                Errors++;

                return false;
            }
        }

        public static bool CheckUI()
        {
            try
            {
                string result = new Url(Constants.Networking.WebApiUri)
                    .AppendPathSegment("signal")
                    .AppendPathSegment("ui").GetJsonAsync<string>().Result;

                return String.Equals(result, "true");
            }
            catch
            {
                Errors++;

                return false;
            }
        }
    }
}
