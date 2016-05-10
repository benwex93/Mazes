using ClientGui.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ClientGui.Model
{
    public static class AppModel
    {
        public static MainWindowViewModel mainWindowViewModel;
        public static void SwitchCurrentView(UserControl controlToChange)
        {
            mainWindowViewModel.currentView = controlToChange;
            mainWindowViewModel.MainWindow_PropertyChanged(new PropertyChangedEventArgs("CurrentView"));
        }
    }
}
