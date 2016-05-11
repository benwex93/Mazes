using ClientGui.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientGui.Model
{
    public class SinglePlayerControlModel
    {
        public void SinglePlayerOption()
        {
            AppModel.SwitchCurrentView(new MainMenuControl());
        }
        public void GetHintOnMaze()
        {
            AppModel.SwitchCurrentView(new MainMenuControl());
        }
    }
}
