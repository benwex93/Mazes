using ClientGui.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientGui.ViewModel;

namespace ClientGui.Model
{
    public class MultiplayerControlModel
    {
        public void SinglePlayerOption()
        {
            ServerSpeaker speaker = AppViewModel.GetServerSpeaker();
            speaker.CloseMultiGame();
            AppModel.SwitchCurrentView(new MainMenuControl());
        }
        public void getHintOnMaze()
        {
            HintShow.DisplayHint();
        }
    }
}
