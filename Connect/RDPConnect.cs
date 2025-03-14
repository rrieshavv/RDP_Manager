using Connect.Common;
using Connect.Encryption;

namespace Connect
{
    public class RDPConnect
    {
        public static void Connector(string ipaddress, string username, string password, string filePath, string rdppath)
        {
            var info = new LogInfo
            {
                Ipaddress = ipaddress,
                Username = username,
                Password = password
            };
            RDPHandler.Rrocess(info, filePath, rdppath);
        }
    }
}
