using System;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

using DeviceId;
using DeviceId.Formatters;
using DeviceId.Encoders;

namespace ProRob.SystemInfo
{
    public class SystemId
    {
        public static string GetSystemId()
        {
            string deviceId = new DeviceIdBuilder()
                    .AddMacAddress(excludeWireless: true)
                    .UseFormatter(new HashDeviceIdFormatter(() => SHA512.Create(), new Base64UrlByteArrayEncoder()))
                    .ToString();

            return ProRob.Security.Hashing.ComputeFixedLengthHashWithAlphabeticCharacters(deviceId);
        }
    }
}
