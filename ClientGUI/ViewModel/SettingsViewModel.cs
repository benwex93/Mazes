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
    public class SettingsViewModel
    {
        private SettingsInfo info = AppViewModel.GetSettingsInfo();
        private ServerSpeaker speaker = AppViewModel.GetServerSpeaker();

        public SettingsViewModel()
        {
        }

        public string txtIPAddress
        {
            get { return info.ip; }
            set
            {
                if (IsValidIP(value))
                {
                    info.ip = value;
                    speaker.SetInfo(info);
                    WriteToFile();
                }
            }
        }
        public string txtPort
        {
            get { return Convert.ToString(info.port); }
            set
            {
                if (IsDigitsOnly(value))
                {
                    info.port = Int32.Parse(value);
                    speaker.SetInfo(info);
                    WriteToFile();
                }
            }
        }
        private bool IsDigitsOnly(string str)
        {
            if (str.Length > 0)
            {
                foreach (char c in str)
                {
                    if (c < '0' || c > '9')
                        return false;
                }
                return true;
            }
            return false;
        }
        private bool IsValidIP(string str)
        {
            if (str.Length > 0)
            {
                int count = str.Count(x => x == '.');
                if (count == 3)
                {
                    string noPeriods = str.Replace(".", "");
                    if (IsDigitsOnly(noPeriods))
                        return true;
                }
            }
            return false;
        }
        private void WriteToFile()
        {
            try
            {
                StreamWriter sw = new StreamWriter(@"../settings.txt", false, Encoding.ASCII);
                sw.WriteLine(info.ip);
                sw.WriteLine(info.port);
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
    }
}
