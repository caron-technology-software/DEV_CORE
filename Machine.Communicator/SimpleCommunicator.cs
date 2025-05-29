using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Apex.Serialization;
using Flurl.Http;

namespace Machine.Communicator
{
    public static class SimpleCommunicator
    {
        private static readonly IBinary serializer = Binary.Create(new Settings());

        public static string GetString(string uri)
        {
            var data = uri
                    .GetJsonAsync<string>()
                    .Result;

            return data;
        }

        public static T GetEncodedData<T>(string uri)
        {
            T data = default;

            try
            {
                //serializer.Precompile<T>();

                string base64 = GetString(uri);
                if (string.IsNullOrEmpty(base64))
                {
                    return data;
                }

                byte[] buffer = System.Convert.FromBase64String(base64);
                if (buffer is null || buffer.Length == 0)
                {
                    return data;
                }

                data = serializer.Read<T>(new System.IO.MemoryStream(buffer));

                return data;
            }
            catch
            {
                return data;
            }
        }
    }
}
