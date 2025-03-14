using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect.Common
{
    public class RDPConstant
    {
        public static readonly string IpaddressPatten = @"^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$";
        public static readonly string FilePath = "rdp.rdp";
        public static readonly string templatePath = @"D:\Practice\PasswordManagement\RDPConnector\Encryption\TemplateRDP.txt";
    }
}
