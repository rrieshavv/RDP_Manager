using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Connect.Common;

namespace Connect.Encryption
{
    public class RDPHandler
    {
        public static void Rrocess(LogInfo info, string filePath, string rdpPath)
        {
            if (string.IsNullOrEmpty(info.Username) || string.IsNullOrEmpty(info.Password))
            {
                throw new ArgumentNullException("username and password can't be empty");
            }
            var pwstr = BitConverter.ToString(DataProtection.ProtectData(Encoding.Unicode.GetBytes(info.Password), "")).Replace("-", "");
            var rdpInfo = string.Format(File.ReadAllText(filePath), info.Ipaddress, info.Username, pwstr);
            File.WriteAllText(rdpPath, rdpInfo);
            //  _mstsc("mstsc " + RDPConstant.FilePath);
            _mstsc("mstsc " + rdpPath);
        }

        private static void _mstsc(string cmd)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.StandardInput.WriteLine(cmd);
        }
    }
}
