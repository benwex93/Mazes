using ClientGui.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ClientGui.ViewModel
{
    public class SinglePlayerControlViewModel
    {
        SinglePlayerControlModel model;
        public SinglePlayerControlViewModel()
        {
            model = new SinglePlayerControlModel();
            mainMenuCommand = new ButtonICommand(model.SinglePlayerOption);  
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
