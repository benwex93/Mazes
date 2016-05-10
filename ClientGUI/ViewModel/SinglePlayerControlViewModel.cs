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
            getHintCommand = new ButtonICommand(model.GetHintOnMaze);
            focusAndStartCommand = new ButtonICommand(model.FocusAndStart);
        }
      //  public event PropertyChangedEventHandler PropertyChanged;
        private ButtonICommand mainMenuCommand;
        private ButtonICommand getHintCommand;
        private ButtonICommand focusAndStartCommand;

        //  public SolidColorBrush backgroundColor { get; set; } = new SolidColorBrush(Color.FromRgb(0, 0, 0));
        //public Visibility menuSelectionVisibility3 { get; set; } = Visibility.Hidden;

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
        public ICommand focusAndStart
        {
            get
            {
                return focusAndStartCommand;
            }
        }
        /** public void MainMenuVM_PropertyChanged(PropertyChangedEventArgs e)
         {
             if (PropertyChanged != null)
                 PropertyChanged(this, e);
         }**/
    }
}
