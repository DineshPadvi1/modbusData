using System.Net.NetworkInformation;

namespace Uniproject.Classes.Model
{
    public static class NetworkHelper
    {
        public static bool IsInternetAvailable()
        {
            try
            {
                using (Ping ping = new Ping())
                {
                    PingReply reply = ping.Send("8.8.8.8", 3000); // Google's public DNS server
                    return reply.Status == IPStatus.Success;
                }
            }
            catch (PingException)
            {
                return false;
            }
        }
    }


}
