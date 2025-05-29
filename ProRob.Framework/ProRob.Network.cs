using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Net;
using System.Net.Sockets;

namespace ProRob.Networking
{
    public class Network
    {
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                //Checks: InterNetwork, VirtualBox, VPN, NO Address
                if ((ip.AddressFamily == AddressFamily.InterNetwork) &&
                    (ip.ToString().Contains("192.168.56") == false) &&
                    (ip.ToString().Contains("10.5.0") == false) &&
                    (ip.ToString().Contains("192.168.5") == false) &&
                    (ip.ToString().Contains("10.8.0") == false) &&
                    (ip.ToString().Contains("169.") == false))
                {
                    return ip.ToString();
                }
            }

            throw new Exception("No network adapters with an IPv4 address in the system");
        }
    }
}