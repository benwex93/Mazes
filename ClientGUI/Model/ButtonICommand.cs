using ClientGui.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ClientGui.Model
{
    public class ButtonICommand : ICommand
    {
        private Action ExecuteAction;

        public event EventHandler CanExecuteChanged;

        public ButtonICommand(Action executeAction) // Point 2
        {
            ExecuteAction = executeAction;
        }
        public bool CanExecute(object parameter)
        {
            return true; // Point 3
        }
        public void Execute(object parameter)
        {
            ExecuteAction(); // Point 4
        }
    }
}
