using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Uniproject.Classes
{
    public class NetworkInfo
    {
        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        public static string GetIpAddress()
        {
            string ipAddress = string.Empty;
            foreach (IPAddress ip in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    ipAddress = ip.ToString();
                    break;
                }
            }
            return ipAddress;
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------

        public static string GetMacAddress()
        {
            string macAddress = string.Empty;
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                // Only consider physical network interfaces
                if (nic.NetworkInterfaceType != NetworkInterfaceType.Loopback &&
                    nic.NetworkInterfaceType != NetworkInterfaceType.Tunnel &&
                    !nic.Description.ToLowerInvariant().Contains("virtual") &&
                    !nic.Description.ToLowerInvariant().Contains("pseudo"))
                {
                    macAddress = nic.GetPhysicalAddress().ToString();
                    break;
                }
            }
            return macAddress;
        }
    }
}
