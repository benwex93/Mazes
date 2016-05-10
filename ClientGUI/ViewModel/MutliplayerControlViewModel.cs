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
        //  public event PropertyChangedEventHandler PropertyChanged;
        private ButtonICommand mainMenuCommand;
        private ButtonICommand getHintCommand;
        private string gameName;
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
        public string GameName
        {
            get
            {
                return gameName;
            }
            set
            {

            }
        }
    }
}
