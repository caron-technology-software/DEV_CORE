using Machine.Security;
using ProRob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Machine
{
    public class License
    {
        public string MachineType { get; set; }
        public int LicenseType { get; set; }
        public string MachineSerial { get; set; }
        public string LicenseHash { get; set; }

        public License()
        {
            // --
        }

        public static string CreateLicenseHash(string machineSerial)
        {
            var stringToHash = $"::CARON_LICENSE_HEADER::{machineSerial}::HNGT-9473-kdj3-1369";

            return ProRob.Security.Hashing.ComputeSHA512(Encoding.UTF8.GetBytes(stringToHash));
        }

        public static bool CheckLicense(string licensePath, string machineSerial)
        {
            var buffer = File.ReadAllBytes(licensePath);

            return CheckLicense(buffer, machineSerial);
        }

        public static bool CheckLicense(byte[] buffer, string machineSerial)
        {
            try
            {
                var licenseHash = CreateLicenseHash(machineSerial);

                string jsonDecrypted = Encoding.UTF8.GetString(Encryption.Decrypt(buffer, licenseHash));

                var license = Json.Deserialize<License>(jsonDecrypted);

                return licenseHash == license.LicenseHash;
            }
            catch
            {
                return false;
            }
        }
    }
}
