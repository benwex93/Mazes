using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;

namespace ClientGui
{
    public static class AppViewModel
    {
        private static SettingsInfo info;
        private static ServerSpeaker speaker;

        public static void ConfigureInfo()
        {
            string ip = null;
            string portStr = null;
            int port;
            string file = @"../settings.txt";
            if (File.Exists(file))
            {
                try
                {
                    StreamReader sr = new StreamReader(file);
                    ip = sr.ReadLine();
                    portStr = sr.ReadLine();
                    sr.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }
            }
            else
            {
                ip = ConfigurationManager.AppSettings["IP address"];
                portStr = ConfigurationManager.AppSettings["port number"];
            }
            if (ip != null && portStr != null)
            {
                port = Int32.Parse(portStr);
                info = new SettingsInfo(ip, port);
                speaker = new ServerSpeaker(info);
            }
        }

        public static SettingsInfo GetSettingsInfo()
        {
            return info;
        }

        public static ServerSpeaker GetServerSpeaker()
        {
            return speaker;
        }
    }
}
