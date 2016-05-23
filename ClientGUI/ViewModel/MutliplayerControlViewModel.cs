using ClientGui.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ClientGui.ViewModel
{
    public class MutliplayerControlViewModel
    {
        MultiplayerControlModel model;
        public MutliplayerControlViewModel()
        {
            model = new MultiplayerControlModel();
            mainMenuCommand = new ButtonICommand(model.SinglePlayerOption);
            getHintCommand = new ButtonICommand(model.getHintOnMaze);
        }
        private ButtonICommand mainMenuCommand;
        private ButtonICommand getHintCommand;
        public ICommand goToMainMenu
        {
            get
            {
                return mainMenuCommand;
            }
        }
        public ICommand getHint
        {
            get
            {
                return getHintCommand;
            }
        }
    }
}
