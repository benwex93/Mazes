using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientGui
{
    public class ServerSpeaker
    {
        private SettingsInfo configs;

        public ServerSpeaker(SettingsInfo info)
        {
            configs = info;
        }

        public void SetInfo(SettingsInfo info)
        {
            configs = info;
        }
    }
}
