using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using ProRob.Extensions.Hashing;

namespace Machine.UI.Communication
{
    public partial class Communicator
    {
        public static string GetHash<T>(string route, string resource = "")
        {
            return Communicator.SendHttpGetRequest(route, resource).GetSHA1Hash();
        }

        public static T GetData<T>(string route, string resource = "")
        {
            var json = Communicator.SendHttpGetRequest(route, resource);

            return ProRob.Json.Deserialize<T>(json);
        }

        public static T GetEncodedData<T>(string route, string resource = "")
        {

            if (Initialized == false)
            {
                Console.WriteLine("[COMMUNICATOR] Communicator must be initialized");
                throw new Exception("Communicator must be initialized");
            }

            T data = default(T);

            try
            {
                //serializer.Precompile<T>();

                string base64 = Communicator.GetData<string>(route, resource);
                if (string.IsNullOrEmpty(base64))
                {
                    return data;
                }

                byte[] buffer = System.Convert.FromBase64String(base64);
                if (buffer is null || buffer.Length == 0)
                {
                    return data;
                }

                lock (instance.lockerSerializer)
                {
                    data = instance.serializer.Read<T>(new System.IO.MemoryStream(buffer));
                }

                return data;
            }
            catch
            {
                Console.WriteLine("ERROR: GetEncodedData");
                return data;
            }
        }

        #region SetVariable(..)
        public static string SetVariable(string route, string routePrefix, string variable, string value)
        {
            return Communicator.SendHttpGetRequest(route, $"{routePrefix}?{variable}={value}");
        }

        public static string SetVariables(string route, string routePrefix, string variable1, string value1, string variable2, string value2)
        {
            return Communicator.SendHttpGetRequest(route, $"{routePrefix}?{variable1}={value1}&{variable2}={value2}");
        }

        public static string SetVariable(string route, string routePrefix, string variable, float value)
        {
            return Communicator.SendHttpGetRequest(route, $"{routePrefix}?{variable}={value.ToString("0.0000", System.Globalization.CultureInfo.InvariantCulture)}");
        }

        public static string SetVariable(string route, string routePrefix, string variable, bool value)
        {
            return Communicator.SendHttpGetRequest(route, $"{routePrefix}?{variable}={value}");
        }

        public static string SetVariable(string route, string routePrefix, string variable, int value)
        {
            return Communicator.SendHttpGetRequest(route, $"{routePrefix}?{variable}={value}");
        }

        public static string SetVariable(string route, string variable, float value)
        {
            return Communicator.SendHttpGetRequest(route, $"?{variable}={value.ToString("0.0000", System.Globalization.CultureInfo.InvariantCulture)}");
        }

        public static string SetVariable(string route, string variable, bool value)
        {
            return Communicator.SendHttpGetRequest(route, $"?{variable}={value}");
        }

        public static string SetVariable(string route, string variable, string value)
        {
            return Communicator.SendHttpGetRequest(route, $"?{variable}={value}");
        }

        public static string SetVariable(string route, string variable, int value)
        {
            return Communicator.SendHttpGetRequest(route, $"?{variable}={value}");
        }
        #endregion
    }
}