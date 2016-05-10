using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using ClientGui.Model;

namespace ClientGui.ViewModel
{
    public static class AppViewModel
    {
        private static SettingsInfo info;
        public static ServerSpeaker speaker;
        private static MazeDimensions dims;

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
            string heightStr = ConfigurationManager.AppSettings["maze height"];
            string lengthStr = ConfigurationManager.AppSettings["maze length"];
            int height = Int32.Parse(heightStr);
            int len = Int32.Parse(lengthStr);
            dims = new MazeDimensions(height, len);
        }

        public static SettingsInfo GetSettingsInfo()
        {
            return info;
        }

        public static ServerSpeaker GetServerSpeaker()
        {
            return speaker;
        }

        public static MazeDimensions GetMazeDimensions()
        {
            return dims;
        }
    }
}
