using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ClientGui.Model
{
    public class SettingsInfo
    {
        public string ip;
        public int port;
        public SettingsInfo(string ipAddr, int portNum)
        {
            ip = ipAddr;
            port = portNum;
        }
    }
}
